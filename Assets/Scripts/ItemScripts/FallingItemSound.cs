
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FallingItemSound : MonoBehaviour
{
    [Header("Sound Settings")]
    [Tooltip("The sound to play when the item hits the floor.")]
    public AudioClip hitSound;
    [Tooltip("The volume of the sound.")]
    [Range(0f, 1f)]
    public float volume = 1f;
    [Tooltip("The pitch of the sound.")]
    [Range(0.1f, 3f)]
    public float pitch = 1f;

    [Header("Layer Settings")]
    [Tooltip("The layer of the floor.")]
    public LayerMask groundLayer;

    private AudioSource audioSource;
    private bool canPlaySound = true;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = hitSound;
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.playOnAwake = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0 && canPlaySound)
        {
            if (audioSource != null)
            {
                audioSource.PlayOneShot(hitSound, volume);
            }
            else
            {
                Debug.LogError("AudioSource not found on the GameObject.");
            }
  
            canPlaySound = false;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            canPlaySound = true;
        }
    }
}
