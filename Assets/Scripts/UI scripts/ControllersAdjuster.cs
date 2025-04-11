using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode] // Allows the script to run in the editor
public class ControllersAdjuster : MonoBehaviour
{
    [Header("Button References")]
    [Tooltip("The left button to position on the bottom left of the screen.")]
    public RectTransform leftButton;

    [Tooltip("The right button to position on the bottom right of the screen.")]
    public RectTransform rightButton;

    [Header("Margin Settings")]
    [Tooltip("The margin from the side of the screen (applies to both buttons).")]
    public float sideMargin = 50f;

    [Tooltip("The margin from the bottom of the screen (applies to both buttons).")]
    public float bottomMargin = 50f;

    [Header("Runtime Settings")]
    [Tooltip("Apply adjustments in the editor mode (useful for testing screen sizes).")]
    public bool applyInEditor = true;

    void Awake()
    {
        // Adjust button positions at the start
        if (Application.isPlaying)
        {
            AdjustButtonPositions();
        }
    }

    void Update()
    {
        // Adjust button positions in editor mode if enabled
        if (!Application.isPlaying && applyInEditor)
        {
            AdjustButtonPositions();
        }
    }

    private void AdjustButtonPositions()
    {
        // Get the screen dimensions
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // Adjust the left button position
        if (leftButton != null)
        {
            leftButton.anchorMin = new Vector2(0, 0); // Bottom-left corner
            leftButton.anchorMax = new Vector2(0, 0); // Bottom-left corner
            leftButton.pivot = new Vector2(0, 0); // Pivot at the bottom-left
            leftButton.anchoredPosition = new Vector2(sideMargin, bottomMargin);
        }

        // Adjust the right button position
        if (rightButton != null)
        {
            rightButton.anchorMin = new Vector2(1, 0); // Bottom-right corner
            rightButton.anchorMax = new Vector2(1, 0); // Bottom-right corner
            rightButton.pivot = new Vector2(1, 0); // Pivot at the bottom-right
            rightButton.anchoredPosition = new Vector2(-sideMargin, bottomMargin);
        }
    }
}
