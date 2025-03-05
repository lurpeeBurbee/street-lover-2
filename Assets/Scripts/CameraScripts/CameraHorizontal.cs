using UnityEngine;

public class CameraHorizontal : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float followDelay = 0.1f; // Delay in camera movement
    public float cameraMoveDistance = 10f; // Distance the camera moves to the next area
    public bool followYAxis = false; // Boolean to determine whether to follow the player on the y-axis

    private bool isMoving = false; // Boolean to check if the camera is currently moving
    private bool hasTriggeredLeft = false; // Boolean to check if the player has triggered the left side
    private bool hasTriggeredRight = false; // Boolean to check if the player has triggered the right side
    private Vector3 targetPosition; // Target position for the camera to move to

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player transform not assigned!");
            return;
        }

        // Initialize the target position as the current camera position
        targetPosition = transform.position;
    }

    void LateUpdate()
    {
        if (!isMoving)
        {
            if (hasTriggeredLeft)
            {
                Debug.Log("Triggered left side - moving camera left");
                MoveCamera(-cameraMoveDistance);
                hasTriggeredLeft = false;
            }
            else if (hasTriggeredRight)
            {
                Debug.Log("Triggered right side - moving camera right");
                MoveCamera(cameraMoveDistance);
                hasTriggeredRight = false;
            }
        }

        // Smoothly interpolate between the camera's current position and the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, followDelay);

        // Check if the camera has reached the target position
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            isMoving = false;
            Debug.Log("Camera reached target position");
        }
    }

    private void MoveCamera(float moveDistance)
    {
        isMoving = true;
        Debug.Log($"Camera moving to new target position with move distance: {moveDistance}");

        // Calculate the new target position based on the move distance
        if (followYAxis)
        {
            targetPosition = new Vector3(transform.position.x + moveDistance, player.position.y, transform.position.z);
        }
        else
        {
            targetPosition = new Vector3(transform.position.x + moveDistance, transform.position.y, transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Trigger entered by: {other.gameObject.name} with tag: {other.tag}");

        if (other.CompareTag("LeftTrigger") && !isMoving)
        {
            hasTriggeredLeft = true;
        }
        else if (other.CompareTag("RightTrigger") && !isMoving)
        {
            hasTriggeredRight = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log($"Trigger exited by: {other.gameObject.name} with tag: {other.tag}");

        if (other.CompareTag("LeftTrigger") || other.CompareTag("RightTrigger"))
        {
            // Prevent immediate re-trigger by requiring the player to exit the trigger first
            if (other.CompareTag("LeftTrigger"))
            {
                hasTriggeredLeft = false;
            }
            else if (other.CompareTag("RightTrigger"))
            {
                hasTriggeredRight = false;
            }
        }
    }
}
