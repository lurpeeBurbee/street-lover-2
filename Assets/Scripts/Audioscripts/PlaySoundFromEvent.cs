using UnityEngine;

// 1. SACRED PATH: Enforce the AudioSource at edit-time so you can mix the volume in the Inspector.
[RequireComponent(typeof(AudioSource))]
public class PlaySoundFromEvent : MonoBehaviour
{
    private AudioSource audioSource;

    [Header("Audio Settings")]
    public AudioClip soundClip;

    [Tooltip("Randomize pitch slightly to prevent the machine-gun effect on footsteps.")]
    [Range(0f, 0.5f)] public float pitchVariation = 0.1f;

    private float basePitch;

    void Start()
    {
        // 2. Guaranteed to exist because of [RequireComponent]
        audioSource = GetComponent<AudioSource>();

        // Store whatever pitch you set in the Inspector as the baseline
        basePitch = audioSource.pitch;
    }

    // This is the method your Animation Event calls
    public void PlaySoundClip()
    {
        if (soundClip != null)
        {
            // 3. Modulate the pitch slightly (e.g., between 0.9 and 1.1) before playing
            audioSource.pitch = basePitch + Random.Range(-pitchVariation, pitchVariation);

            // 4. Play the clip
            audioSource.PlayOneShot(soundClip);
        }
        else
        {
            Debug.LogWarning("SoundClip is not assigned on " + gameObject.name);
        }
    }
}