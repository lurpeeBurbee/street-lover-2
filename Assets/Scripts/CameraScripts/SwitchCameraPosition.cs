using System.Collections.Generic;
using UnityEngine;

public class SwitchCameraPosition : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public PlayerMovement2D_pickle playerMovement; // Reference to the player's movement script
    public List<Transform> cameraPositions; // List of camera positions
    public float moveSpeed = 2f; // Speed at which the camera moves to the new position
    public Transform pointLeft; // Reference to the left point
    public Transform pointRight; // Reference to the right point

    private float originalMoveSpeed; // Store the original move speed of the player
    private bool isMoving = false; // Flag to check if the camera is moving

    void Start()
    {
        if (player == null || playerMovement == null || pointLeft == null || pointRight == null)
        {
            Debug.LogError("Required components not assigned!");
            return;
        }

        originalMoveSpeed = playerMovement.moveSpeed;
    }

    void Update()
    {
        if (isMoving) return;

        if (IsTouchingPoint(pointLeft))
        {
            StartCoroutine(MoveCameraToPosition(cameraPositions[0].position));
        }
        else if (IsTouchingPoint(pointRight))
        {
            StartCoroutine(MoveCameraToPosition(cameraPositions[1].position));
        }
    }

    private bool IsTouchingPoint(Transform point)
    {
        return Vector2.Distance(player.position, point.position) < 0.1f;
    }

    private System.Collections.IEnumerator MoveCameraToPosition(Vector3 targetPosition)
    {
        isMoving = true;
        playerMovement.moveSpeed = 0f;

        while (Vector3.Distance(Camera.main.transform.position, targetPosition) > 0.1f)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        Camera.main.transform.position = targetPosition;
        playerMovement.moveSpeed = originalMoveSpeed;
        isMoving = false;
    }
}
