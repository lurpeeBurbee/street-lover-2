using UnityEngine;
using UnityEngine.InputSystem;

// Automatically adds these components if they are missing. Safety first!
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class FullPlayerMovement : MonoBehaviour
{
    [Header("Input Actions")]
    [Tooltip("Bind a 1D Axis (A/D or Left/Right Arrows)")]
    public InputAction moveAction;
    [Tooltip("Bind a Button (Spacebar/South Gamepad Button)")]
    public InputAction jumpAction;
    [Tooltip("Bind a Button (Left Click/West Gamepad Button)")]
    public InputAction shootAction;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float jumpForce = 12f;

    [Header("Ground Detection")]
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Combat Settings")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    private Rigidbody2D rb;
    private float moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // Force the Z rotation to freeze so the player doesn't tip over like a bowling pin.
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        // This runs EVERY time you press Play
        if (groundLayer == 0)
        {
            groundLayer = LayerMask.GetMask("Ground");
            Debug.Log("Gameplay Start: Automatically found and set Ground Layer.");
        }
    }

    private void OnEnable()
    {
        // --- THE MAGAZINE SUBSCRIPTION ANALOGY ---
        // jumpAction.performed is the Publisher. 
        // += : "Subscribe me to this magazine." (If we just used '=', we would cancel everyone else's subscription!)
        // ctx : The actual magazine issue delivered to your door (Context/Data about the button press).
        // =>  : "Take this magazine AND THEN read it" (execute the Jump function).

        jumpAction.performed += ctx => Jump(); // Subscribe to the jump action
        shootAction.performed += ctx => Shoot(); // Subscribe to the shoot action

        // We must enable the actions to start listening
        moveAction.Enable(); // Enable the movement action
        jumpAction.Enable(); // Enable the jump action
        shootAction.Enable(); // Enable the shoot action
    }

    private void OnDisable()
    {
        // Always unsubscribe (-=) when the object is disabled to prevent memory leaks!
        jumpAction.performed -= ctx => Jump();
        shootAction.performed -= ctx => Shoot();

        moveAction.Disable();
        jumpAction.Disable();
        shootAction.Disable();
    }

    private void Update()
    {
        // Read the horizontal movement continuously (e.g., -1 for left, 1 for right)
        moveInput = moveAction.ReadValue<float>();
    }

    private void FixedUpdate()
    {
        // Apply movement in FixedUpdate for smooth, frame-rate independent physics
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    private void Jump()
    {
        // Only jump if we are touching the "Ground" layer
        if (IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); // Set the vertical velocity to the jump force (this allows for variable jump height if you want to implement it later)
        }
    }

    private void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            // Experiment here: instantiate the bullet and let the bullet's own script handle its speed
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Debug.Log("Pew pew!");
        }
        else
        {
            Debug.LogWarning("Missing Bullet Prefab or Fire Point!");
        }
    }

    private bool IsGrounded()
    {
        if (groundCheckPoint == null) return false;

        // Draws an invisible circle at the feet to check for the Ground layer
        return Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);
    }

    // Draws a helpful visual sphere in the Editor to show where the ground check is happening
    private void OnDrawGizmosSelected()
    {
        if (groundCheckPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheckPoint.position, groundCheckRadius);
        }
    }



}