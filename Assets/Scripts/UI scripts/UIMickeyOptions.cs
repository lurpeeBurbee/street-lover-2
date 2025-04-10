using UnityEngine;
using UnityEngine.UI;

public class UIMickeyOptions : MonoBehaviour
{
    [Header("UI Settings")]
    public Button closeButton; // Reference to the button that closes the canvas
    private Canvas canvas; // Reference to the canvas this script is attached to

    void Start()
    {
        // Get the Canvas component attached to this GameObject or its parent
        canvas = GetComponent<Canvas>();
        if (canvas == null)
        {
            canvas = GetComponentInParent<Canvas>();
        }

        // Ensure the closeButton is assigned and add a listener to it
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(CloseCanvas);
        }
        else
        {
            Debug.LogError("Close Button is not assigned in the Inspector!");
        }
    }

    // Method to close the canvas
    private void CloseCanvas()
    {
        if (canvas != null)
        {
            canvas.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Canvas not found!");
        }
    }
}
