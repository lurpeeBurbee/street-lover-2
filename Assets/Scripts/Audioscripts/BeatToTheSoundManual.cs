using UnityEngine;

public class BeatToTheSoundManual : MonoBehaviour
{
    // The interval between beats in seconds
    public float beatInterval = 1.0f;

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

    // Timer to track the beat interval
    private float timer = 0f;

    private void Start()
    {
        // Store the original scale and position of the sprite
        originalScale = transform.localScale;
        originalPosition = transform.localPosition;
    }

    private void Update()
    {
        // Increment the timer
        timer += Time.deltaTime;

        // Check if the timer exceeds the beat interval
        if (timer >= beatInterval)
        {
            // Reset the timer
            timer = 0f;

            // Scale the sprite based on the beat interval
            Vector3 newScale = originalScale;
            Vector3 newPosition = originalPosition;

            // Apply scaling and positioning based on the selected lock side
            switch (lockSide)
            {
                case LockSide.Bottom:
                    newScale.y += beatScaleAmount;
                    newPosition.y += beatScaleAmount * yPositionAdditive; // Adjust position to keep the bottom in place
                    break;
                case LockSide.Top:
                    newScale.y -= beatScaleAmount;
                    newPosition.y -= beatScaleAmount * yPositionAdditive; // Adjust position to keep the top in place
                    break;
                case LockSide.Left:
                    newScale.x += beatScaleAmount;
                    break;
                case LockSide.Right:
                    newScale.x -= beatScaleAmount;
                    break;
                default:
                    newScale += beatScaleAmount * Vector3.one;
                    break;
            }

            // Smoothly interpolate the scale and position
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
}
