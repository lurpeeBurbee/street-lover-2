using UnityEngine;

public class PlaySoundFromEvent : MonoBehaviour
{
    private AudioSource audioSource;

    [Header("Audio Clip")]
    public AudioClip soundClip;

    void Start()
    {
        // Initialize the AudioSource component
        audioSource = GetComponent<AudioSource>();

        // Ensure the AudioSource component exists
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
 
    public void PlaySoundClip()
    {
        // Check if the soundClip is assigned
        if (soundClip != null)
        {
            // Play the sound clip
            audioSource.PlayOneShot(soundClip);
        }
        else
        {
            Debug.LogWarning("SoundClip is not assigned.");
        }
    }
}
