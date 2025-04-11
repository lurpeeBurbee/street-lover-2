using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways] // Allows the script to run in both play and editor modes
public class InfoCanvasAdjuster : MonoBehaviour
{
    [Header("Canvas Reference")]
    [Tooltip("The Canvas to adjust. Ensure this is assigned in the Inspector.")]
    public Canvas targetCanvas;

    [Header("Canvas Margin Settings")]
    [Tooltip("Margin on the left side of the screen.")]
    public float leftMargin = 10f;

    [Tooltip("Margin on the right side of the screen.")]
    public float rightMargin = 10f;

    [Tooltip("Margin from the top of the screen.")]
    public float topMargin = 10f;

    [Tooltip("Height of the Canvas area.")]
    public float canvasHeight = 100f;

    [Header("Runtime Settings")]
    [Tooltip("Apply adjustments in editor mode (useful for testing).")]
    public bool applyInEditor = true;

    private RectTransform rectTransform;
    private HorizontalLayoutGroup layoutGroup;

    void Awake()
    {
        InitializeCanvas();
        AdjustCanvas();
    }

    void Update()
    {
        // Adjust the Canvas in both play and editor modes
        if (!Application.isPlaying && applyInEditor)
        {
            AdjustCanvas();
        }
    }

    private void InitializeCanvas()
    {
        if (targetCanvas == null)
        {
            Debug.LogError("Target Canvas is not assigned in InfoCanvasAdjuster!");
            return;
        }

        rectTransform = targetCanvas.GetComponent<RectTransform>();
        if (rectTransform == null)
        {
            Debug.LogError("The assigned Canvas does not have a RectTransform component!");
        }

        layoutGroup = targetCanvas.GetComponentInChildren<HorizontalLayoutGroup>();
        if (layoutGroup == null)
        {
            Debug.LogWarning("No Horizontal Layout Group found in the Canvas hierarchy.");
        }
    }

    private void AdjustCanvas()
    {
        if (rectTransform == null)
        {
            InitializeCanvas();
            if (rectTransform == null) return; // Exit if initialization failed
        }

        // Temporarily disable the Horizontal Layout Group to allow manual adjustments
        if (layoutGroup != null)
        {
            layoutGroup.enabled = false;
        }

        // Get the screen dimensions
        float screenWidth = Screen.width;

        // Calculate the width and position based on margins
        float canvasWidth = screenWidth - leftMargin - rightMargin;
        float canvasPosX = leftMargin;

        // Set the RectTransform properties
        rectTransform.anchorMin = new Vector2(0, 1); // Top-left corner
        rectTransform.anchorMax = new Vector2(0, 1); // Top-left corner
        rectTransform.pivot = new Vector2(0, 1);     // Pivot at the top-left
        rectTransform.sizeDelta = new Vector2(canvasWidth, canvasHeight);
        rectTransform.anchoredPosition = new Vector2(canvasPosX, -topMargin);

        // Re-enable the Horizontal Layout Group after adjustments
        if (layoutGroup != null)
        {
            layoutGroup.enabled = true;
        }
    }
}
