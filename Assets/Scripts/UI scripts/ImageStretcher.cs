using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode] // Allows the script to run in the editor
public class ImageStretcher : MonoBehaviour
{
    [Header("Current Screen Sizes")]
    [SerializeField, Tooltip("Current screen width in pixels.")]
    float screenWidth;
    [SerializeField, Tooltip("Current screen height in pixels.")]
    float screenHeight;

    [Header("Stretch Settings")]
    [Tooltip("Stretch the width of the images to match the screen width.")]
    public bool stretchWidth = true;

    [Tooltip("Stretch the height of the images to match the screen height.")]
    public bool stretchHeight = false;

    [Header("Runtime Settings")]
    [Tooltip("Apply stretching in the editor mode (useful for testing screen sizes).")]
    public bool applyInEditor = true;

    [Header("Target Images")]
    [Space(5)]
    [TextArea(2, 5)]
    public string imageInfo = "The images must be UI elements with a RectTransform component.\n" +
        "Ensure they are part of a Canvas.";
    public List<RectTransform> targetImages = new();

    [Header("Snapping Settings")]
    [Tooltip("Enable snapping of images to each other.")]
    public bool enableSnapping = true;

    [Tooltip("Snap only top and bottom borders.")]
    public bool snapVertical = true;

    [Tooltip("Snap side borders as well.")]
    public bool snapHorizontal = false;

    void Awake()
    {
        // Apply stretching and snapping at the start
        if (Application.isPlaying)
        {
            StretchAndSnapImages();
        }
    }

    void Update()
    {
        // Apply stretching and snapping in editor mode if enabled
        if (!Application.isPlaying && applyInEditor)
        {
            StretchAndSnapImages();
        }
    }

    private void StretchAndSnapImages()
    {
        if (targetImages == null || targetImages.Count == 0) return; // No images to process

        // Get the screen dimensions
        screenWidth = Screen.width;
        screenHeight = Screen.height;

        // Calculate the total height of all images for proportional stretching
        float totalHeight = 0f;
        foreach (var rectTransform in targetImages)
        {
            if (rectTransform != null)
            {
                totalHeight += rectTransform.sizeDelta.y;
            }
        }

        // Stretch and snap images
        float currentYPosition = screenHeight / 2f; // Start from the top of the screen
        foreach (var rectTransform in targetImages)
        {
            if (rectTransform == null) continue;

            // Stretch the width and height proportionally
            Vector2 newSize = rectTransform.sizeDelta;

            if (stretchWidth)
            {
                newSize.x = screenWidth;
            }

            if (stretchHeight)
            {
                newSize.y = (rectTransform.sizeDelta.y / totalHeight) * screenHeight; // Proportional height
            }

            rectTransform.sizeDelta = newSize;

            // Snap vertical borders
            if (snapVertical)
            {
                float halfHeight = newSize.y / 2f;
                currentYPosition -= halfHeight; // Move down by half the height
                rectTransform.localPosition = new Vector3(0, currentYPosition, rectTransform.localPosition.z);
                currentYPosition -= halfHeight; // Move down by the other half
            }

            // Snap horizontal borders (optional)
            if (snapHorizontal)
            {
                rectTransform.localPosition = new Vector3(0, rectTransform.localPosition.y, rectTransform.localPosition.z);
            }
        }
    }
}
