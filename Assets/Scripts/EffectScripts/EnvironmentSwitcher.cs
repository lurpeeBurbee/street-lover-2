using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D))]
public class EnvironmentSwitcher : MonoBehaviour
{
    [Header("BACKGROUND SETTINGS")]
    public Image backgroundImage;
    public Sprite cityBackground;
    public Sprite forestBackground;
    public Color fadeColor = Color.black;

    [Header("AUDIO SETTINGS")]
    public AudioSource cityAudio;
    public AudioSource forestAudio;

    [Header("TRIGGER SETTINGS")]
    public string triggerTag = "Player"; // Much easier to read in the Inspector
    public bool debugVisuals = true;

    private bool isTransitioning;
    private float centerPosition;
    private float halfWidth;
    private Transform playerTransform;

    private void Start()
    {
        backgroundImage.color = Color.white;
        backgroundImage.sprite = cityBackground;

        GetComponent<Collider2D>().isTrigger = true; // Ensure the collider is set to trigger mode.

        centerPosition = transform.position.x; // Cache the center position for easy access during transitions
        halfWidth = transform.localScale.x / 2f; // Assuming the trigger's width is determined by its localScale.x

        cityAudio.volume = 1f;
        forestAudio.volume = 0f;
    }

    private void Update()
    {
        if (playerTransform != null)
        {
            if (isTransitioning)
            {
                UpdateTransition(); // This is where the magic happens every frame while the player is within the trigger area
            }
            else
            {
                ValidateBackgroundPosition(); // This ensures that if the player teleports or moves quickly, the background and audio will still update correctly
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Readable plain-English check
        if (other.CompareTag(triggerTag))
        {
            playerTransform = other.transform; // Cache the player's transform for use in Update()
            isTransitioning = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Readable plain-English check
        if (other.CompareTag(triggerTag))
        {
            CompleteTransition(); // Finalize the transition immediately when the player leaves the trigger area
            playerTransform = null; // We need to clear this so Update() doesn't try to access it after the player leaves
        }
    }

    private void UpdateTransition()
    {
        float playerX = playerTransform.position.x; // Get the player's current X position
        float distanceFromCenter = Mathf.Abs(playerX - centerPosition); // Calculate how far the player is from the center of the trigger area

        float fadeAmount = Mathf.Clamp01(1f - (distanceFromCenter / halfWidth)); // This gives us a value between 0 and 1 that represents how close the player is to the center of the trigger area.
                                                                                 // The closer they are, the higher the fadeAmount.
        backgroundImage.color = Color.Lerp(Color.white, fadeColor, fadeAmount); // Lerp the background color from white to the fadeColor based on the fadeAmount.
                                                                                // When fadeAmount is 1, the background will be fully faded; when it's 0, it will be fully white.

        float normalizedPos = Mathf.Clamp01((playerX - (centerPosition - halfWidth)) / (halfWidth * 2f)); // This gives us a value between 0 and 1 that represents the player's position within the trigger area.
                                                                                                          // When the player is at the left edge, normalizedPos will be 0; when they're at the right edge, it will be 1.

        cityAudio.volume = 1f - normalizedPos; // As the player moves from left to right, the city audio will fade out (1 to 0) and the forest audio will fade in (0 to 1).
        forestAudio.volume = normalizedPos; // This creates a smooth crossfade between the two audio sources as the player moves through the trigger area.

        bool shouldBeForest = playerX > centerPosition; // This is a simple check to determine which background sprite should be active based on whether the player is on the left or right side of the center position.
        Sprite targetSprite = shouldBeForest ? forestBackground : cityBackground; // This is a concise way to select the target sprite based on the player's position.
                                                                                  // If shouldBeForest is true, we use the forestBackground; otherwise, we use the cityBackground.

        if (backgroundImage.sprite != targetSprite)
        {
            backgroundImage.sprite = targetSprite;
        }
    }

    private void ValidateBackgroundPosition()
    {
        float playerX = playerTransform.position.x;
        bool shouldBeForest = playerX > centerPosition; // This is the same check we use in UpdateTransition to determine which background should be active based on the player's position.
        Sprite targetSprite = shouldBeForest ? forestBackground : cityBackground; 

        if (backgroundImage.sprite != targetSprite)
        {
            backgroundImage.sprite = targetSprite;
        }
        if (cityAudio != null) cityAudio.volume = shouldBeForest ? 0f : 1f; // This ensures that if the player teleports or moves quickly, the audio will still update correctly based on their position relative to the center of the trigger area.
        if (forestAudio != null) forestAudio.volume = shouldBeForest ? 1f : 0f;
    }

    private void CompleteTransition()
    {
        isTransitioning = false;
        backgroundImage.color = Color.white; // Reset the background color to white immediately when the player leaves the trigger area.

        if (playerTransform != null)
        {
            ValidateBackgroundPosition(); // This ensures that when the player leaves the trigger area, the background and audio will immediately snap to the correct state based on their final position relative to the center of the trigger area.
        }
    }


}