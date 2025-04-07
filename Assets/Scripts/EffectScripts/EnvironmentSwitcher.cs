using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D))]
public class EnvironmentSwitcher : MonoBehaviour
{
    [Header("BACKGROUND SETTINGS")]
    public Image backgroundImage;
    public Sprite cityBackground;  // Left side
    public Sprite forestBackground; // Right side
    public Color fadeColor = Color.black;
    public float fadeSpeed = 1f;

    [Header("AUDIO SETTINGS")]
    public AudioSource cityAudio;
    public AudioSource forestAudio;

    [Header("TRIGGER SETTINGS")]
    public LayerMask triggerLayer;
    public bool debugVisuals = true;

    private bool isTransitioning;
    private float centerPosition;
    private float halfWidth;
    private Transform playerTransform;

    private void Start()
    {
        // Set initial state
        backgroundImage.color = Color.white;
        backgroundImage.sprite = cityBackground; // Default to city

        // Configure collider
        GetComponent<Collider2D>().isTrigger = true;

        // Calculate trigger area properties
        centerPosition = transform.position.x;
        halfWidth = transform.localScale.x / 2;

        // Initialize audio
        cityAudio.volume = 1f;
        forestAudio.volume = 0f;
    }

    private void Update()
    {
        // Constant validation when player is in trigger area
        if (playerTransform != null)
        {
            ValidateBackgroundPosition();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & triggerLayer) != 0)
        {
            playerTransform = other.transform;
            StartTransition();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & triggerLayer) != 0)
        {
            UpdateTransition();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & triggerLayer) != 0)
        {
            CompleteTransition();
            playerTransform = null;
        }
    }

    private void ValidateBackgroundPosition()
    {
        float playerX = playerTransform.position.x;
        bool shouldBeForest = playerX > centerPosition;

        // Immediate correction if wrong background is shown
        if (shouldBeForest && backgroundImage.sprite != forestBackground && !isTransitioning)
        {
            backgroundImage.sprite = forestBackground;
            cityAudio.volume = 0f;
            forestAudio.volume = 1f;
        }
        else if (!shouldBeForest && backgroundImage.sprite != cityBackground && !isTransitioning)
        {
            backgroundImage.sprite = cityBackground;
            cityAudio.volume = 1f;
            forestAudio.volume = 0f;
        }
    }

    private void StartTransition()
    {
        if (isTransitioning) return;
        isTransitioning = true;
    }

    private void UpdateTransition()
    {
        if (!isTransitioning || playerTransform == null) return;

        float playerX = playerTransform.position.x;
        float distanceFromCenter = Mathf.Abs(playerX - centerPosition);
        float fadeAmount = 1 - (distanceFromCenter / halfWidth);

        // Apply fade to black at center
        backgroundImage.color = Color.Lerp(Color.white, fadeColor, fadeAmount);

        // Switch background at full black (center point)
        if (fadeAmount >= 0.99f)
        {
            bool shouldBeForest = playerX > centerPosition;
            backgroundImage.sprite = shouldBeForest ? forestBackground : cityBackground;

            // Audio cut (as requested)
            cityAudio.volume = shouldBeForest ? 0f : 1f;
            forestAudio.volume = shouldBeForest ? 1f : 0f;
        }
    }

    private void CompleteTransition()
    {
        if (!isTransitioning) return;

        backgroundImage.color = Color.white;
        isTransitioning = false;

        // Final validation
        if (playerTransform != null)
        {
            ValidateBackgroundPosition();
        }
    }

    private void OnDrawGizmos()
    {
        if (!debugVisuals) return;

        Gizmos.color = new Color(0, 1, 0, 0.3f);
        Gizmos.DrawCube(transform.position, transform.localScale);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, transform.localScale);

        // Draw center line
        Gizmos.color = Color.red;
        Vector3 centerLineStart = transform.position + Vector3.up * transform.localScale.y / 2;
        Vector3 centerLineEnd = transform.position + Vector3.down * transform.localScale.y / 2;
        Gizmos.DrawLine(centerLineStart, centerLineEnd);
    }
}