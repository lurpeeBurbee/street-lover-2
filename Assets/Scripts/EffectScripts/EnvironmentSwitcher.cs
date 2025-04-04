using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D))]
public class EnvironmentSwitcher : MonoBehaviour
{
    [Header("BACKGROUND SETTINGS")]
    [Tooltip("Drag the Canvas Panel with the background Image component here")]
    public Image backgroundImage;
    [Tooltip("First background texture (fades out when transitioning)")]
    public Sprite backgroundA;
    [Tooltip("Second background texture (fades in when transitioning)")]
    public Sprite backgroundB;
    [Tooltip("Fade color (default is black)")]
    public Color fadeColor = Color.black;
    [Tooltip("Fade duration multiplier (higher = slower transition)")]
    public float fadeSpeed = 1f;

    [Header("AUDIO SETTINGS")]
    [Tooltip("Main background audio source (fades out)")]
    public AudioSource audioSourceA;
    [Tooltip("Secondary audio source (fades in)")]
    public AudioSource audioSourceB;
    [Tooltip("Check to enable audio crossfading")]
    public bool enableAudioFade = true;

    [Header("TRIGGER SETTINGS")]
    [Tooltip("Layer that triggers the transition (e.g., Player)")]
    public LayerMask triggerLayer;
    [Tooltip("Show debug visuals for the trigger area")]
    public bool debugVisuals = true;

    private bool isTransitioning;
    private Sprite currentBackground;
    private Sprite targetBackground;
    private AudioSource currentAudio;
    private AudioSource targetAudio;
    private Color originalColor;

    private void Start()
    {
        // Initialize audio sources
        if (enableAudioFade)
        {
            audioSourceA.volume = 1f;
            audioSourceA.Play();
            audioSourceB.volume = 0f;
            audioSourceB.Play();
        }

        // Set up initial state
        originalColor = backgroundImage.color;
        currentBackground = backgroundA;
        targetBackground = backgroundB;
        currentAudio = audioSourceA;
        targetAudio = audioSourceB;

        // Configure collider
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & triggerLayer) != 0)
        {
            StartTransition(other.transform);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & triggerLayer) != 0)
        {
            UpdateTransition(other.transform);
        }
    }

    private void StartTransition(Transform player)
    {
        if (isTransitioning) return;

        isTransitioning = true;

        // Swap target assets
        if (backgroundImage.sprite == backgroundA || backgroundImage.sprite == null)
        {
            targetBackground = backgroundB;
            targetAudio = audioSourceB;
            currentAudio = audioSourceA;
        }
        else
        {
            targetBackground = backgroundA;
            targetAudio = audioSourceA;
            currentAudio = audioSourceB;
        }

        // Ensure both audio sources are playing
        if (enableAudioFade)
        {
            if (!currentAudio.isPlaying) currentAudio.Play();
            if (!targetAudio.isPlaying) targetAudio.Play();
        }
    }

    private void UpdateTransition(Transform player)
    {
        if (!isTransitioning) return;

        // Calculate progress based on player position in trigger area
        float horizontalProgress = Mathf.InverseLerp(
            transform.position.x - transform.localScale.x / 2,
            transform.position.x + transform.localScale.x / 2,
            player.position.x
        );

        // Apply fade to background
        backgroundImage.sprite = targetBackground;
        backgroundImage.color = Color.Lerp(originalColor, fadeColor, horizontalProgress);

        // Crossfade audio if enabled
        if (enableAudioFade)
        {
            currentAudio.volume = 1 - horizontalProgress;
            targetAudio.volume = horizontalProgress;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & triggerLayer) != 0)
        {
            CompleteTransition();
        }
    }

    private void CompleteTransition()
    {
        // Finalize the swap
        currentBackground = targetBackground;
        backgroundImage.color = originalColor;

        if (enableAudioFade)
        {
            currentAudio.volume = 0f;
            targetAudio.volume = 1f;
        }

        isTransitioning = false;
    }

    private void OnDrawGizmos()
    {
        if (!debugVisuals) return;

        Gizmos.color = new Color(0, 1, 0, 0.3f);
        Gizmos.DrawCube(transform.position, transform.localScale);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}