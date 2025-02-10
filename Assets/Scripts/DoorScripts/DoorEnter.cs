using System.Collections;
using TMPro; // Make sure to include TextMeshPro namespace
using UnityEngine;

public class DoorEnter : MonoBehaviour
{
    public TextMeshProUGUI enterText; // UI Text component to display the message
    private bool playerInTrigger = false;
    private SceneControl sceneControl;
    public AudioSource doorOpenSound; // Reference to the AudioSource for the door opening sound


    private void Start()
    {
        // Check if the enterText is assigned
        if (enterText != null)
        {
            // Ensure the text is initially hidden
            enterText.enabled = false;
        }
        else
        {
            Debug.LogWarning("EnterText is not assigned.");
        }

        // Get the SceneControl component on the same gameobject
        sceneControl = GetComponent<SceneControl>();
    }

    private void Update()
    {
        // Check if the player is in trigger and the Enter key is pressed
        if (playerInTrigger && Input.GetKeyDown(KeyCode.Return))
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
    }
    private IEnumerator PlaySoundAndLoadScene()
    {
        doorOpenSound.Play(); // Play the door opening sound
        yield return new WaitForSeconds(doorOpenSound.clip.length);
        // Wait for the duration of the sound
        sceneControl.LoadScene(); // Load the scene after the sound has finished
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the triggering object has the tag "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInTrigger = true;
            // Show the text if it's assigned
            if (enterText != null)
            {
                enterText.enabled = true;
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
            if (enterText != null)
            {
                enterText.enabled = false;
            }
        }
    }
}
