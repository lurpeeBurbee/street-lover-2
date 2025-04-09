using UnityEngine;
using UnityEngine.Playables;

public class TimelineController : MonoBehaviour
{
    [Header("Timeline Settings")]
    public PlayableDirector timeline; // Reference to the PlayableDirector controlling the timeline
    public float targetTimeInSeconds; // The time to jump to in the timeline

    [Header("Audio Source Settings")]
    public AudioSource sourceToEnable; // The AudioSource to enable
    public AudioSource sourceToDisable; // The AudioSource to disable

    private bool isTriggered = false; // Prevent multiple triggers

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isTriggered) return;

        if (other.CompareTag("Player")) // Ensure the player is the one triggering
        {
            isTriggered = true;

            // Jump the timeline to the specified time
            if (timeline != null)
            {
                timeline.time = targetTimeInSeconds;
                timeline.Play();
            }

            // Enable and disable the specified AudioSources
            if (sourceToEnable != null)
            {
                sourceToEnable.enabled = true;
                sourceToEnable.Play(); // Start playing the enabled AudioSource
            }

            if (sourceToDisable != null)
            {
                sourceToDisable.Stop(); // Stop the disabled AudioSource
                sourceToDisable.enabled = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Reset the trigger when the player exits
        if (other.CompareTag("Player"))
        {
            isTriggered = false;
        }
    }
}
 