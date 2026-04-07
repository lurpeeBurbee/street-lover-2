using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
[RequireComponent (typeof(SpriteRenderer))]
public class DraggableTimer : MonoBehaviour
{
    [Header("New Input System Actions")]
    // Pre-bound so you don't even have to set them in the Inspector
    public InputAction clickAction = new("Click", InputActionType.Button, "<Pointer>/press");
    public InputAction positionAction = new("Position", InputActionType.Value, "<Pointer>/position");

    [Header("Settings")]
    public float timeLimit = 3.0f;

    private bool isTimerRunning = false;
    private float countdown;
    private float absoluteStartTime; // Time.time when the timer starts
    private float sceneStartTime; // Time.timeSinceLevelLoad when the timer starts

    private Camera mainCam;
    private Vector3 offset;
    private Collider2D col;
    private SpriteRenderer sr; // NEW: Cached visual component
    private ParticleSystem childParticles; // NEW: The nested effect
    private bool particlesPlaying = false; // NEW: The trigger lock
    private bool isDragging = false;

    // IF WE IMPLEMENT THE GAME LOOP: We need to remember where we started
    private Vector3 originalPosition;

    [Header("Effects Settings")]
    [Tooltip("When should the particles ignite? (0.0 = Start, 1.0 = End)")]
    [Range(0f, 1f)]
    public float rageStartTime = 0.5f; // NEW: The designer's slider
    private void Awake()
    {
        col = GetComponent<Collider2D>(); // Is secure because of the RequireComponent attribute, so we can safely cache it here.
        sr = GetComponent<SpriteRenderer>(); // Lock it into memory
        // Let the script find the child particle system automatically
        childParticles = GetComponentInChildren<ParticleSystem>();
    }

    private void Start()
    {
        mainCam = Camera.main;
        countdown = timeLimit;

        // Cache the exact spawn coordinate before the user ever touches it
        originalPosition = transform.position;
        sr.color = Color.white; // Ensure we always start pure white

        // Failsafe: Ensure it doesn't emit immediately if "Play On Awake" was checked
        if (childParticles != null)
        {
            childParticles.Stop();
        }
    }

    private void OnEnable()
    {
        // 1. Enable and Subscribe
        clickAction.Enable();
        positionAction.Enable();
        clickAction.started += OnClickStarted;
        clickAction.canceled += OnClickCanceled;
    }

    private void OnDisable()
    {
        // Always clean up to prevent memory leaks
        clickAction.Disable();
        positionAction.Disable();
        clickAction.started -= OnClickStarted;
        clickAction.canceled -= OnClickCanceled;
    }

    // 2. THE TRIGGER: Replaces OnMouseDown
    private void OnClickStarted(InputAction.CallbackContext ctx)
    {
        Vector2 screenPos = positionAction.ReadValue<Vector2>(); // Get the pointer position at the moment of click
        Vector3 worldPos = mainCam.ScreenToWorldPoint(screenPos); // Convert to world space. World Space means the actual position in the game world, not just on the screen. This is crucial for accurate interaction with game objects.

        // Manual Raycast: Did the pointer click inside our 2D Collider?
        if (col.OverlapPoint(worldPos))
        {
            isDragging = true;
            offset = transform.position - worldPos; // Calculate the offset so the block doesn't snap to the pointer's center

            if (!isTimerRunning) // We avoid restarting the timer if it's already running, allowing for multiple drags within the same timer session
            {
                isTimerRunning = true;
                absoluteStartTime = Time.time;
                sceneStartTime = Time.timeSinceLevelLoad;
                StartCoroutine(DestructionSequence()); // Start the timer for destruction
            }
        }
    }

    // 3. THE RELEASE: Stops the drag logic
    private void OnClickCanceled(InputAction.CallbackContext ctx)
    {
        isDragging = false;
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            countdown -= Time.deltaTime; // Decrease the countdown based on real time passed since the last frame

            // --- THE NORMALIZED BLUSH EFFECT ---
            // 1. Calculate how much time has passed as a percentage (0.0 to 1.0)
            float timePassedPercentage = 1f - Mathf.Clamp01(countdown / timeLimit);

            // 2. Blend the colors based on that exact percentage
            sr.color = Color.Lerp(Color.white, Color.red, timePassedPercentage);

            // --- THE ADJUSTABLE PARTICLE TRIGGER ---
            // 3. Compare the timeline against the Inspector slider
            if (timePassedPercentage >= rageStartTime && !particlesPlaying)
            {
                if (childParticles != null) childParticles.Play();
                particlesPlaying = true;
            }

        }

        // 4. THE MOVEMENT: Replaces OnMouseDrag
        if (isDragging)
        {
            Vector2 screenPos = positionAction.ReadValue<Vector2>(); // Get the current pointer position every frame while dragging
            Vector3 newWorldPos = mainCam.ScreenToWorldPoint(screenPos) + offset; // Convert to world space and apply the initial offset
            newWorldPos.z = 0f; // Keep it flat on the 2D plane
            transform.position = newWorldPos; // Move the block to follow the pointer
        }
    }


    private void OnGUI()
    {
        GUIStyle style = new()
        {
            fontSize = 32, alignment = TextAnchor.UpperLeft
        };
        style.normal.textColor = Color.green;
        style.fontStyle = FontStyle.Bold;

        string timerText = isTimerRunning
            ? $"Time Remaining: {Mathf.Max(0, countdown):F1}s"
            : "Pick up the block to start the timer.";

        string globalText = $"Time.time (App Start): {Time.time:F1}s";
        string sceneText = $"Time.timeSinceLevelLoad (Scene Start): {Time.timeSinceLevelLoad:F1}s";

        GUI.Label(new Rect(20, 20, 400, 40), timerText, style);

        if (isTimerRunning)
        {
            GUI.Label(new Rect(20, 60, 400, 40), $"Started at (App): {absoluteStartTime:F1}s", style);
            GUI.Label(new Rect(20, 100, 400, 40), $"Started at (Scene): {sceneStartTime:F1}s", style);
        }

        GUI.Label(new Rect(20, Screen.height - 80, 400, 40), globalText, style);
        GUI.Label(new Rect(20, Screen.height - 40, 400, 40), sceneText, style);
    }

    // =========================================================================
    // COROUTINE BEHAVIORS: Comment out the one you don't want to use
    // =========================================================================

    // BEHAVIOR A: The Permanent Destruction (Standard)
    /*
    private IEnumerator DestructionSequence()
    {
        yield return new WaitForSeconds(timeLimit);
        Destroy(gameObject);
    }
    */

    // BEHAVIOR B: The State Reset (Object Pooling Foundation) - This is more complex but allows the block to be reused without needing to respawn it, which is ideal for performance in a real game loop.
    private IEnumerator DestructionSequence()
    {
        yield return new WaitForSeconds(timeLimit);

        // 1. Force the object to drop out of the player's "hand"
        isDragging = false;

        // 2. Snap physical coordinates back to the vault
        transform.position = originalPosition;

        // 3. Reset the internal memory map state
        isTimerRunning = false;
        countdown = timeLimit;

        // Reset the visual state so the trick can happen again
        sr.color = Color.white;

        // --- PARTICLE CLEANUP ---
        if (childParticles != null)
        {
            childParticles.Stop();
            childParticles.Clear(); // Erases any smoke/sparks instantly
        }
        particlesPlaying = false; // Unlock the trigger for the next cycle
    }
}