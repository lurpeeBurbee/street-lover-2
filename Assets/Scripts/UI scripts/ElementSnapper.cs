using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode] // Allows the script to run in the editor
public class ElementSnapper : MonoBehaviour
{
    [Header("Target Images")]
    [Tooltip("The upper UI image that will snap to the top of the screen")]
    public RectTransform upperImage;

    [Tooltip("The lower UI image that will snap to the bottom of the screen")]
    public RectTransform lowerImage;

    [Header("Settings")]
    [Tooltip("Enable to match the width of both images to the screen width")]
    public bool matchScreenWidth = true;

    [Tooltip("Enable to stretch images vertically to fill the screen height")]
    public bool stretchToScreenHeight = true;

    [Tooltip("Apply snapping in the editor mode (useful for testing)")]
    public bool applyInEditor = true;

    [Header("Current Screen Info")]
    [SerializeField, Tooltip("Current screen width in pixels")]
    private float screenWidth;

    [SerializeField, Tooltip("Current screen height in pixels")]
    private float screenHeight;

    [Space(10)]
    [TextArea(2, 4)]
    public string helpText = "This component snaps two UI images together vertically to create a seamless background.\n" +
                           "The upper image snaps to screen top, lower image snaps to screen bottom.\n" +
                           "Both images must be UI elements with RectTransform components and part of a Canvas.";

    private void Awake()
    {
        // Apply snapping at the start in play mode
        if (Application.isPlaying)
        {
            SnapElements();
        }
    }

    private void Update()
    {
        // Apply snapping in editor mode if enabled
        if (!Application.isPlaying && applyInEditor)
        {
            SnapElements();
        }
    }


    // Snaps the two images together and to the screen edges, and optionally matches their width to the screen width

    private void SnapElements()
    {
        // Check if both images are assigned
        if (upperImage == null || lowerImage == null)
        {
            return;
        }

        // Get the current screen dimensions
        screenWidth = Screen.width;
        screenHeight = Screen.height;

        // Match width to screen width if enabled
        if (matchScreenWidth)
        {
            // Adjust the width of both images to match the screen width
            Vector2 upperSize = upperImage.sizeDelta;
            upperSize.x = screenWidth;
            upperImage.sizeDelta = upperSize;

            Vector2 lowerSize = lowerImage.sizeDelta;
            lowerSize.x = screenWidth;
            lowerImage.sizeDelta = lowerSize;
        }

        // If we're stretching to screen height
        if (stretchToScreenHeight)
        {
            // Calculate how much vertical space we have
            float totalHeight = screenHeight;

            // Maintain the aspect ratio between the two images
            float upperToLowerRatio = upperImage.rect.height / (upperImage.rect.height + lowerImage.rect.height);

            // Resize the images to fill the screen height while maintaining their proportions
            Vector2 upperSize = upperImage.sizeDelta;
            upperSize.y = totalHeight * upperToLowerRatio;
            upperImage.sizeDelta = upperSize;

            Vector2 lowerSize = lowerImage.sizeDelta;
            lowerSize.y = totalHeight * (1 - upperToLowerRatio);
            lowerImage.sizeDelta = lowerSize;
        }

        // Calculate positions to snap to screen edges
        float upperImageHalfHeight = upperImage.rect.height / 2f;
        float lowerImageHalfHeight = lowerImage.rect.height / 2f;

        // Position the upper image with its top at the top of the screen
        upperImage.localPosition = new Vector3(
            0,  // Centered horizontally
            (screenHeight / 2) - upperImageHalfHeight,  // Top aligned with screen top
            upperImage.localPosition.z
        );

        // Position the lower image with its bottom at the bottom of the screen
        lowerImage.localPosition = new Vector3(
            0,  // Centered horizontally
            -(screenHeight / 2) + lowerImageHalfHeight,  // Bottom aligned with screen bottom
            lowerImage.localPosition.z
        );

        // Ensure the images meet in the middle without gaps or overlaps
        // This can be enabled if you want perfect alignment regardless of image sizes
        // For now, we rely on the stretchToScreenHeight option to handle this properly
    }
}