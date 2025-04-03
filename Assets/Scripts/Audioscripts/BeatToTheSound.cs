using UnityEngine;

public class BeatToTheSound : MonoBehaviour
{
    [Header("This script recognizes the beat of the sound and scales the sprite accordingly.")]
    // Reference to the AudioSource component
    [SerializeField] private AudioSource audioSource;

    // Array to hold the spectrum data
    private float[] spectrum;

    // The frequency band to analyze for bass (low frequencies)
    [Range(0, 1023)]
    public int bassBandIndex = 0;

    // The threshold for the bass intensity to trigger a beat
    [Range(0.0f, 1.0f)]
    public float beatThreshold = 0.038f;

    // The amount to scale the sprite when beating
    public float beatScaleAmount = 20.0f;

    // The speed of the beat effect
    public float beatSpeed = 5.0f;

    // The Y-position additive value to adjust the positioning of the sprite
    public float yPositionAdditive = 0.5f;

    // Enum to select which side to lock
    public enum LockSide
    {
        None,
        Bottom,
        Top,
        Left,
        Right
    }

    // Variable to select which side to lock
    public LockSide lockSide = LockSide.Bottom;

    // The original scale and position of the sprite
    private Vector3 originalScale;
    private Vector3 originalPosition;

    private void Start()
    {
        // Check if the AudioSource component is found
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found on the GameObject.");
            enabled = false; // Disable the script if no AudioSource is found
            return;
        }

        // Initialize the spectrum array
        spectrum = new float[1024];

        // Store the original scale and position of the sprite
        originalScale = transform.localScale;
        originalPosition = transform.localPosition;
    }

    private void Update()
    {
        // Get the spectrum data from the AudioSource
        audioSource.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

        // Get the intensity of the bass frequency band
        float bassIntensity = spectrum[bassBandIndex] * 100f;

        // Check if the bass intensity exceeds the threshold
        if (bassIntensity > beatThreshold)
        {
            // Scale the sprite based on the beat intensity
            Vector3 newScale = originalScale;
            Vector3 newPosition = originalPosition;

            // Apply scaling and positioning based on the selected lock side
            switch (lockSide)
            {
                case LockSide.Bottom:
                    newScale.y += bassIntensity * beatScaleAmount;
                    newPosition.y += bassIntensity * beatScaleAmount * yPositionAdditive; // Adjust position to keep the bottom in place
                    break;
                case LockSide.Top:
                    newScale.y -= bassIntensity * beatScaleAmount;
                    newPosition.y -= bassIntensity * beatScaleAmount * yPositionAdditive; // Adjust position to keep the top in place
                    break;
                case LockSide.Left:
                    newScale.x += bassIntensity * beatScaleAmount;
                    break;
                case LockSide.Right:
                    newScale.x -= bassIntensity * beatScaleAmount;
                    break;
                default:
                    newScale += bassIntensity * beatScaleAmount * Vector3.one;
                    break;
            }

            transform.localScale = Vector3.Lerp(transform.localScale, newScale, Time.deltaTime * beatSpeed);
            transform.localPosition = Vector3.Lerp(transform.localPosition, newPosition, Time.deltaTime * beatSpeed);
        }
        else
        {
            // Reset the scale and position to the original values
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, Time.deltaTime * beatSpeed);
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalPosition, Time.deltaTime * beatSpeed);
        }
    }

    private void OnValidate()
    {
        // Ensure the bassBandIndex is within the valid range
        bassBandIndex = Mathf.Clamp(bassBandIndex, 0, 1023);
    }
}
