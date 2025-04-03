using UnityEngine;

public class MusicPlay : MonoBehaviour
{
    // Header for the script description
    [Header("Music Play Script")]
    [Tooltip("This script plays background music on loop when the game starts. Attach this script to a GameObject with an AudioSource component.")]

    // Reference to the AudioSource component
    [SerializeField]
    AudioSource backgroundMusic;

    // Public variables to control volume and pitch
    [Header("Audio Settings")]
    [Range(0.0f, 1.0f)]
    [Tooltip("Volume of the background music.")]
    public float volume = 1.0f;

    [Range(0.5f, 1.5f)]
    [Tooltip("Pitch of the background music.")]
    public float pitch = 1.0f;


    private void Awake()
    {


        // Check if the AudioSource component is found
        if (backgroundMusic == null)
        {
            Debug.LogError("AudioSource component not found on the GameObject.");
            return;
        }

        // Ensure the audio clip is set
        if (backgroundMusic.clip == null)
        {
            Debug.LogError("Audio clip not set on the AudioSource component.");
            return;
        }

        // Set the audio to loop
        backgroundMusic.loop = true;

        // Set the volume and pitch
        backgroundMusic.volume = volume;
        backgroundMusic.pitch = pitch;

        // Play the audio
        backgroundMusic.Play();
    }
}
