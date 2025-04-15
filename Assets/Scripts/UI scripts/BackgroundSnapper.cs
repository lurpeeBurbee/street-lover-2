
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode] // Allows the script to run in the editor
public class BackgroundSnapper : MonoBehaviour
{
    [Header("Target Image")]
    [Tooltip("The UI image that will snap to the edges of the screen")]
    public RectTransform targetImage;

    [Header("Settings")]
    [Tooltip("Enable to match the width of the image to the screen width")]
    public bool matchScreenWidth = true;

    [Tooltip("Enable to stretch the image vertically to fill the screen height")]
    public bool stretchToScreenHeight = true;

    [Tooltip("Preserve the image's proportions when stretching")]
    public bool preserveProportions = true;

    [Tooltip("Apply snapping in the editor mode (useful for testing)")]
    public bool applyInEditor = true;

    [Header("Current Screen Info")]
    [SerializeField, Tooltip("Current screen width in pixels")]
    private float screenWidth;

    [SerializeField, Tooltip("Current screen height in pixels")]
    private float screenHeight;

    [Space(10)]
    [TextArea(2, 4)]
    public string helpText = "This component snaps a UI image to the edges of the screen.\n" +
                             "The image must be a UI element with a RectTransform component and part of a Canvas.";

    private void Awake()
    {
        // Apply snapping at the start in play mode
        if (Application.isPlaying)
        {
            SnapElement();
        }
    }

    private void Update()
    {
        // Apply snapping in editor mode if enabled
        if (!Application.isPlaying && applyInEditor)
        {
            SnapElement();
        }
    }

    // Snaps the image to the screen edges and optionally matches its width to the screen width
    private void SnapElement()
    {
        // Check if the image is assigned
        if (targetImage == null)
        {
            return;
        }

        // Get the current screen dimensions
        screenWidth = Screen.width;
        screenHeight = Screen.height;

        // Match width to screen width if enabled
        if (matchScreenWidth)
        {
            // Adjust the width of the image to match the screen width
            Vector2 size = targetImage.sizeDelta;
            size.x = screenWidth;
            targetImage.sizeDelta = size;
        }

        // Stretch to screen height if enabled
        if (stretchToScreenHeight)
        {
            // Adjust the height of the image to match the screen height
            Vector2 size = targetImage.sizeDelta;
            size.y = screenHeight;

            // Preserve proportions if enabled
            if (preserveProportions)
            {
                float aspectRatio = targetImage.rect.width / targetImage.rect.height;
                size.x = screenHeight * aspectRatio;
            }

            targetImage.sizeDelta = size;
        }

        // Position the image to fill the screen
        targetImage.localPosition = Vector3.zero;
    }
}