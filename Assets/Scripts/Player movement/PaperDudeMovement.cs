using UnityEngine;
using UnityEngine.InputSystem;

public class PaperDudeMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;

    public Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private InputAction moveAction;
    private float moveInput;
    private bool facingRight = true;

    private void Awake()
    {
        // Cache references early to prevent OnEnable race conditions
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        if (moveAction == null)
        {
            moveAction = new InputAction("Move", InputActionType.Value);

            // These strings are recognized universally by the Input System Package
            moveAction.AddCompositeBinding("1DAxis")
                .With("Negative", "<Keyboard>/a")
                .With("Negative", "<Keyboard>/leftArrow")
                .With("Positive", "<Keyboard>/d")
                .With("Positive", "<Keyboard>/rightArrow");
        }

        moveAction.Enable();
    }

    private void OnDisable()
    {
        // Crucial for Mac/Linux: Stop movement if the script is disabled 
        // or the game loses focus to prevent "Runaway Dude" bugs.
        moveInput = 0;
        if (moveAction != null)
            moveAction.Disable();
    }

    void Update()
    {
        // ReadValue is safe across all desktop OS architectures
        moveInput = moveAction.ReadValue<float>();

        // Unity 6.4 uses linearVelocity. On Mac/Linux builds, 
        // this is highly optimized in the physics engine.
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (moveInput > 0 && !facingRight) Flip();
        else if (moveInput < 0 && facingRight) Flip();
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}