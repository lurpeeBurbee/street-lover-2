using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DiamondSound : MonoBehaviour
{
    private AudioSource audioSource;

    [Header("Collision Clips")]
    public AudioClip concreteClip;
    public AudioClip grassClip;

    [Header("Audio Settings")]
    [Range(0f, 0.3f)] public float pitchRandomness = 0.1f;
    public float minVelocityForSound = 0.5f; // Prevent sound when just rolling

    private int concreteLayer;
    private int grassLayer;
    private Rigidbody2D rb;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();

        concreteLayer = LayerMask.NameToLayer("Concrete");
        grassLayer = LayerMask.NameToLayer("Grass");
    }

    // This catches the 'Prewarmer' child trigger events automatically
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Only play if we are falling with some speed
        if (rb.linearVelocity.magnitude < minVelocityForSound) return;

        int hitLayer = other.gameObject.layer;

        if (hitLayer == concreteLayer)
        {
            PlayImpact(concreteClip);
        }
        else if (hitLayer == grassLayer && grassClip != null)
        {
            PlayImpact(grassClip);
        }
    }

    private void PlayImpact(AudioClip clip)
    {
        audioSource.pitch = 1.0f + Random.Range(-pitchRandomness, pitchRandomness);
        audioSource.PlayOneShot(clip);
    }
}