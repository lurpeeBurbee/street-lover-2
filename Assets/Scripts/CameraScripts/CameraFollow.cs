using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float followDelay = 0.1f; // Delay in following the player
    public bool followYAxis = true; // Boolean to determine whether to follow the player on the y-axis
    public float yThreshold = 1.0f; // Threshold for y-axis adjustment
    public float minYPosition = 0.0f; // Minimum y-position for the camera

    private Vector3 offset; // Offset between the camera and the player
    private float initialY; // Initial y-axis position of the camera

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player transform not assigned!");
            return;
        }

        // Calculate and store the offset value by getting the distance between the player's position and camera's position
        offset = transform.position - player.position;

        // Store the initial y-axis position of the camera
        initialY = transform.position.y;
    }

    void LateUpdate()
    {
        if (player == null) return;

        // Calculate the target position based on whether the camera follows the y-axis
        Vector3 targetPosition = followYAxis
            ? new Vector3(player.position.x, player.position.y, player.position.z) + offset
            : new Vector3(player.position.x, initialY, player.position.z) + offset;

        // Apply minimum y-position restriction
        if (targetPosition.y < minYPosition)
        {
            targetPosition.y = minYPosition;
        }

        // Smoothly interpolate between the camera's current position and the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, followDelay);
    }
}
