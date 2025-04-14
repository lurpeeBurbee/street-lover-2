using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode] // Allows the script to run in the editor
public class ElementRelocator : MonoBehaviour
{
    [Header("Target UI Element")]
    [Tooltip("The UI element to keep within screen bounds")]
    public RectTransform targetElement;

    [Header("Screen Margins")]
    [Tooltip("Minimum distance from the left edge of the screen (in pixels)")]
    public float leftMargin = 20f;

    [Tooltip("Minimum distance from the right edge of the screen (in pixels)")]
    public float rightMargin = 20f;

    [Tooltip("Minimum distance from the top edge of the screen (in pixels)")]
    public float topMargin = 20f;

    [Tooltip("Minimum distance from the bottom edge of the screen (in pixels)")]
    public float bottomMargin = 20f;

    [Header("Element Size")]
    [Tooltip("Enable to override the element's width")]
    public bool overrideWidth = false;

    [Tooltip("New width for the element (in pixels)")]
    public float elementWidth = 200f;

    [Tooltip("Enable to override the element's height")]
    public bool overrideHeight = false;

    [Tooltip("New height for the element (in pixels)")]
    public float elementHeight = 200f;

    [Header("Screen Adaptation")]
    [Tooltip("Enable to make the size relative to screen width")]
    public bool useRelativeSize = false;

    [Range(0.05f, 0.9f)]
    [Tooltip("Width as percentage of screen width")]
    public float widthPercentage = 0.2f;

    [Range(0.05f, 0.9f)]
    [Tooltip("Height as percentage of screen width (using width for consistency)")]
    public float heightPercentage = 0.2f;

    [Header("Settings")]
    [Tooltip("Enable relocator in editor mode")]
    public bool applyInEditor = true;

    [Tooltip("Update the element position and size every frame")]
    public bool continuousUpdate = true;

    [Header("Debug Info")]
    [SerializeField, Tooltip("Current screen dimensions")]
    private Vector2 screenSize;

    [SerializeField, Tooltip("Current element position")]
    private Vector2 elementPosition;

    [Space(10)]
    [TextArea(2, 5)]
    public string helpText = "This component keeps the target UI element within screen bounds with specified margins.\n" +
                           "It can also resize the element by absolute size or screen percentage.\n" +
                           "The element must be a UI element with a RectTransform component and part of a Canvas.";

    private Canvas parentCanvas;
    private RectTransform canvasRectTransform;

    private void Awake()
    {
        Initialize();

        // Apply once at the start in play mode
        if (Application.isPlaying)
        {
            ProcessElement();
        }
    }

    private void Update()
    {
        // Process in editor mode if enabled
        if (!Application.isPlaying && applyInEditor)
        {
            ProcessElement();
        }
        // Process every frame in play mode if continuous update is enabled
        else if (Application.isPlaying && continuousUpdate)
        {
            ProcessElement();
        }
    }


    // Finds the parent canvas for coordinate conversion

    private void Initialize()
    {
        if (targetElement != null)
        {
            // Find the parent canvas
            parentCanvas = GetComponentInParent<Canvas>();
            if (parentCanvas == null)
            {
                parentCanvas = targetElement.GetComponentInParent<Canvas>(); // Get the parent canvas of the target element
            }

            if (parentCanvas != null)
            {
                canvasRectTransform = parentCanvas.GetComponent<RectTransform>(); // Get the RectTransform of the canvas
            }
        }
    }


    // Main function that processes the element's size and position

    private void ProcessElement()
    {
        if (targetElement == null) return;

        // Initialize if needed
        if (parentCanvas == null)
        {
            Initialize();
            if (parentCanvas == null) return;
        }

        // Update current screen size
        screenSize = new Vector2(Screen.width, Screen.height);

        // Process element size if requested
        UpdateElementSize();

        // Keep the element within screen bounds
        KeepElementWithinBounds();

        // Update debug info
        elementPosition = targetElement.position;
    }

    // Updates the element's size based on settings

    private void UpdateElementSize()
    {
        // Get current size
        Vector2 size = targetElement.sizeDelta;

        // Apply relative sizing if enabled
        if (useRelativeSize)
        {
            size.x = screenSize.x * widthPercentage;
            size.y = screenSize.x * heightPercentage; // Using screen width for both for consistency
        }
        // Otherwise apply absolute sizing if enabled
        else
        {
            if (overrideWidth)
            {
                size.x = elementWidth;
            }

            if (overrideHeight)
            {
                size.y = elementHeight;
            }
        }

        // Apply new size
        targetElement.sizeDelta = size;
    }

    // Keeps the element within screen bounds based on margins
    private void KeepElementWithinBounds()
    {
        // Get element size and current position in canvas space
        float elementWidth = targetElement.rect.width;
        float elementHeight = targetElement.rect.height;
        Vector2 position = targetElement.anchoredPosition;

        // Adjust position based on anchor mode and pivot
        // This assumes the canvas uses "Screen Space - Overlay" or "Screen Space - Camera" render mode

        // Calculate screen bounds considering margins
        float minX = leftMargin + (elementWidth * targetElement.pivot.x);
        float maxX = screenSize.x - rightMargin - (elementWidth * (1 - targetElement.pivot.x));
        float minY = bottomMargin + (elementHeight * targetElement.pivot.y);
        float maxY = screenSize.y - topMargin - (elementHeight * (1 - targetElement.pivot.y));

        // Convert screen bounds to canvas-relative positions
        if (parentCanvas.renderMode == RenderMode.ScreenSpaceCamera ||
            parentCanvas.renderMode == RenderMode.WorldSpace)
        {
            // For camera and world space canvases, we need to convert
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasRectTransform,
                new Vector2(minX, minY),
                parentCanvas.renderMode == RenderMode.ScreenSpaceCamera ? parentCanvas.worldCamera : null,
                out Vector2 minPointLocal);

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasRectTransform,
                new Vector2(maxX, maxY),
                parentCanvas.renderMode == RenderMode.ScreenSpaceCamera ? parentCanvas.worldCamera : null,
                out Vector2 maxPointLocal);

            minX = minPointLocal.x;
            minY = minPointLocal.y;
            maxX = maxPointLocal.x;
            maxY = maxPointLocal.y;
        }

        // Apply bounds checking and correct position if needed
        Vector2 newPosition = position;

        // Handle different canvas scaling modes
        //if (parentCanvas.renderMode == RenderMode.ScreenSpaceOverlay)
        //{
        //    // Get anchor reference point in screen space
        //    Vector2 anchorRefPoint = new Vector2(
        //        screenSize.x * targetElement.anchorMin.x + screenSize.x * targetElement.anchorMax.x,
        //        screenSize.y * targetElement.anchorMin.y + screenSize.y * targetElement.anchorMax.y
        //    ) * 0.5f;

        //    // Calculate min/max positions considering anchors
        //    float leftBound = minX - anchorRefPoint.x;
        //    float rightBound = maxX - anchorRefPoint.x;
        //    float bottomBound = minY - anchorRefPoint.y;
        //    float topBound = maxY - anchorRefPoint.y;

        //    // Clamp position
        //    newPosition.x = Mathf.Clamp(position.x, leftBound, rightBound);
        //    newPosition.y = Mathf.Clamp(position.y, bottomBound, topBound);
        //}
        //else
        //{
            // Simpler approach for camera/world space
            newPosition.x = Mathf.Clamp(position.x, minX, maxX);
            newPosition.y = Mathf.Clamp(position.y, minY, maxY);
      //  }

        // Apply new position if it changed
        if (newPosition != position)
        {
            targetElement.anchoredPosition = newPosition;
        }
    }

    /// Apply the current settings immediately (can be called from a button)

    public void ApplyNow()
    {
        ProcessElement();
    }
}