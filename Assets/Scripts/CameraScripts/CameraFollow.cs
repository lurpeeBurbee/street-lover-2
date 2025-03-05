using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float followDelay = 0.1f; // Delay in following the player
    public bool followYAxis = true; // Boolean to determine whether to follow the player on the y-axis

    private Vector3 offset; // Offset between the camera and the player
    public float fixedY; // Fixed y-axis position for the camera
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
        fixedY = transform.position.y;
    }
    void LateUpdate()
    {
        if (player == null) return;

        // Calculate the target position based on whether the camera follows the y-axis
        Vector3 targetPosition = followYAxis
            ? new Vector3(player.position.x, player.position.y, player.position.z) + offset
            : new Vector3(player.position.x, fixedY, player.position.z) + offset;

        // Smoothly interpolate between the camera's current position and the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, followDelay);
    }
}
