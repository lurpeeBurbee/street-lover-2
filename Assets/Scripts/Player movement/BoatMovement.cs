using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    [Header("Boat Movement Settings")]
    [SerializeField] private float maxSpeed = 5f; // Maximum speed of the boat
    [SerializeField] private float acceleration = 2f; // Accumulation power (how quickly the boat accelerates)
    [SerializeField] private float deceleration = 1f; // How quickly the boat slows down when no input is given

    private float currentSpeed = 0f; // Current speed of the boat
    private float moveInput = 0f; // Input value for movement (-1 for left, 1 for right)
    private Rigidbody2D rb; // Reference to the Rigidbody2D component

    void Start()
    {
        // Get the Rigidbody2D component attached to the boat
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component is missing from the boat!");
        }
    }

    // This method can be called from other scripts to set the move input directly
    public void SetMoveInput(float input)
    {
        moveInput = input;
        Debug.Log("Move Input Set: " + moveInput);
    }

    void Update()
    {
        // Simulate acceleration and deceleration
        if (moveInput != 0)
        {
            // Accelerate towards the target speed
            currentSpeed = Mathf.MoveTowards(currentSpeed, moveInput * maxSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            // Decelerate to a stop when no input is given
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, deceleration * Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        // Apply the movement to the Rigidbody2D
        if (rb != null)
        {
            rb.linearVelocity = new Vector2(currentSpeed, rb.linearVelocity.y);
        }
    }
}
