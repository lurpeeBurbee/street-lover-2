using UnityEngine;
using UnityEngine.UI;

public class UIMickeyOptions : MonoBehaviour
{
    [Header("UI Settings")]
    public Button closeButton; // Reference to the button that closes the canvas

    [HideInInspector]
    public UIMickeyControl mainMenuController; // Reference to the main menu controller

    private Canvas canvas; // Reference to the canvas this script is attached to

    void Start()
    {
        // Get the Canvas component attached to this GameObject or its parent
        canvas = GetComponent<Canvas>();
        if (canvas == null)
        {
            canvas = GetComponentInParent<Canvas>();
        }

        // If mainMenuController wasn't set, try to find it
        if (mainMenuController == null)
        {
            mainMenuController = Object.FindAnyObjectByType<UIMickeyControl>();
            if (mainMenuController != null)
            {
                Debug.Log("Found main menu controller");
            }
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
        Debug.Log("Close button clicked");

        if (canvas != null)
        {
            canvas.gameObject.SetActive(false);

            // Important: Notify the main menu controller
            if (mainMenuController != null)
            {
                mainMenuController.CloseOptions();
                Debug.Log("Notified main controller to re-enable buttons");
            }
            else
            {
                Debug.LogWarning("Main menu controller not found - buttons may remain disabled");
            }
        }
        else
        {
            Debug.LogError("Canvas not found!");
        }
    }
}
