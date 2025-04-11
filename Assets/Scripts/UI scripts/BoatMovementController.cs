using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BoatMovementController : MonoBehaviour
{
    [Header("Boat Movement Settings")]
    [Tooltip("Reference to the BoatMovement script.")]
    public BoatMovement boatMovement;

    [Header("UI Button Controls")]
    [Tooltip("Button to move the boat left.")]
    public Button leftButton;

    [Tooltip("Button to move the boat right.")]
    public Button rightButton;

    [Header("Sound Settings")]
    [Tooltip("Sound to play when a button is pressed.")]
    public AudioClip buttonPressSound;
    private AudioSource audioSource;

    [Header("Gameplay Settings")]

  public float buttonInput = 0f; // Input value from buttons (-1 for left, 1 for right)

    void Start()
    {
        // Ensure the BoatMovement script is assigned
        if (boatMovement == null)
        {
            Debug.LogError("BoatMovement script is not assigned!");
            return;
        }

        // Initialize the AudioSource for playing button press sounds
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        // Add event listeners for button press and release
        // If we are not using AddButtonListeners, we can add the events in the Inspector for both buttons.
        // Commenting this out to avoid confusion and errors

        if (leftButton != null)
        {
            AddButtonListeners(leftButton, -1);
        }

        if (rightButton != null)
        {
            AddButtonListeners(rightButton, 1);
        }
    }
    public void OnRightButtonPointerExit()    // This method is called when the pointer exits the right button
    {
        Debug.Log("Pointer exited the right button.");
    }


    void Update()
    {


        // Apply input to the BoatMovement script
        boatMovement.SetMoveInput(buttonInput);
    }

    // We don't need AddButtonListeners if we add the Events PointerUp and PointerDown in the Inspector
    // for both buttons. We just add the float -1 or 1 to the OnPointerDown event of the button in the Inspector.
    // The methods must also be public, so we can call them from the Inspector.


    private void AddButtonListeners(Button button, float inputValue)
    {
        // Add event listeners for pointer down and up
        EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();

        // Pointer Down
        EventTrigger.Entry pointerDownEntry = new()
        {
            eventID = EventTriggerType.PointerDown
        };
        pointerDownEntry.callback.AddListener((data) => OnButtonPress(inputValue));
        trigger.triggers.Add(pointerDownEntry);

        // Pointer Up
        EventTrigger.Entry pointerUpEntry = new()
        {
            eventID = EventTriggerType.PointerUp
        };
        pointerUpEntry.callback.AddListener((data) => OnButtonRelease());
        trigger.triggers.Add(pointerUpEntry);
    }

    public void OnButtonPress(float input)
    {
        // Set the button input value
        buttonInput = input;

        // Play the button press sound if assigned
        if (buttonPressSound != null && audioSource != null)
        {
            audioSource.clip = buttonPressSound;
            audioSource.Play();
        }
    }

    private void OnButtonRelease()
    {
        // Reset the button input value
        buttonInput = 0f;
    }
}
