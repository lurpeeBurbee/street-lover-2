using UnityEngine;
using TMPro; // Make sure to include TextMeshPro namespace
using System.Collections; // Include System.Collections for coroutines

public class DoorExit : MonoBehaviour
{
    public TextMeshProUGUI exitText; // UI Text component to display the message
    private bool playerInTrigger = false;
    private SceneControl sceneControl;
    public AudioSource doorOpenSound; // Reference to the AudioSource for the door opening sound

    private void Start()
    {
        // Check if the exitText is assigned
        if (exitText != null)
        {
            // Ensure the text is initially hidden
            exitText.enabled = false;
        }
        else
        {
            Debug.LogWarning("ExitText is not assigned.");
        }

        // Log all components on this GameObject
        Component[] components = GetComponents<Component>();
        foreach (var component in components)
        {
            Debug.Log("Component: " + component.GetType().Name);
        }

        // Get the SceneControl component on the same GameObject
        sceneControl = GetComponent<SceneControl>();
        if (sceneControl == null)
        {
            Debug.LogError("SceneControl component is not found on the same GameObject.");
        }
        else
        {
            Debug.Log("SceneControl component found successfully.");
        }
    }

    private void Update()
    {
        // Check if the player is in trigger and the Enter key is pressed
        if (playerInTrigger && Input.GetKeyDown(KeyCode.Return))
        {
            if (sceneControl != null)
            {
                if (doorOpenSound != null)
                {
                    StartCoroutine(PlaySoundAndLoadScene());
                }
                else
                {
                    sceneControl.LoadScene();
                }
            }
            else
            {
                Debug.LogError("SceneControl component is missing. Cannot load scene.");
            }
        }
    }

    private IEnumerator PlaySoundAndLoadScene()
    {
        doorOpenSound.Play(); // Play the door opening sound
        yield return new WaitForSeconds(doorOpenSound.clip.length); // Wait for the duration of the sound
        sceneControl.LoadScene(); // Load the scene after the sound has finished
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the triggering object has the tag "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInTrigger = true;
            // Show the text if it's assigned
            if (exitText != null)
            {
                exitText.enabled = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Check if the exiting object has the tag "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInTrigger = false;
            // Hide the text if it's assigned
            if (exitText != null)
            {
                exitText.enabled = false;
            }
        }
    }
}
