using UnityEngine;
using UnityEngine.InputSystem;

[ExecuteInEditMode]
public class PlayerMovement2D_pickle : MonoBehaviour
{
    public float moveSpeed = 5f;
    [Header("Enable jump")]
    [SerializeField] bool canJump;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float wallSlideSpeed = 2f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private bool isGrounded;
    private bool isTouchingWall;
    private float wallFriction = 1f;
    private bool facingRight = false;
    public AudioSource jumpSound;

    // Reference your existing Input Action Asset in the Inspector
    [SerializeField] private InputActionAsset inputActions;
    private InputActionMap actionMap;
    private InputAction moveAction;
    private InputAction jumpAction;
    private float moveInput;

    private void Awake()
    {
        AddComponents();
        SetupInputSystem();
    }

    private void SetupInputSystem()
    {
        if (inputActions == null)
        {
            Debug.LogError("Input Action Asset not assigned! Please assign your 'Player' Input Action Asset in the Inspector.");
            return;
        }

        // Find your action map (replace "Player" with your actual map name if different)
        actionMap = inputActions.FindActionMap("Player");

        if (actionMap == null)
        {
            Debug.LogError("Could not find Action Map named 'Player' in the Input Action Asset.");
            return;
        }

        // Get the actions
        moveAction = actionMap.FindAction("Move");
        jumpAction = actionMap.FindAction("Jump");

        // Subscribe to jump action
        if (jumpAction != null)
        {
            jumpAction.performed += OnJumpPerformed;
            jumpAction.Enable();
        }

        if (moveAction != null)
        {
            moveAction.Enable();
        }
    }

    private void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        if (jumpSound != null)
        {
            jumpSound.time = 0;
            jumpSound.Play();
            jumpSound.Pause();
        }

        facingRight = false;
    }

    private void Update()
    {
        if (moveAction != null)
        {
            moveInput = moveAction.ReadValue<float>();
        }

        rb.linearVelocity = new Vector2(moveInput * moveSpeed * wallFriction, rb.linearVelocity.y);

        HandleFriction();
        FlipSprite(moveInput);
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        ControlJump();
    }

    // ... (rest of the methods remain the same as above)

    private void AddComponents()
    {
        if (GetComponent<Rigidbody2D>() == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            rb = GetComponent<Rigidbody2D>();
        }

        if (GetComponent<CapsuleCollider2D>() == null)
        {
            gameObject.AddComponent<CapsuleCollider2D>();
        }
    }

    private void HandleFriction()
    {
        if (!isGrounded && isTouchingWall)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -wallSlideSpeed);
            wallFriction = 0.5f;
        }
        else
        {
            wallFriction = 1f;
        }
    }

    private void ControlJump()
    {
        if (canJump && isGrounded)
        {
            if (jumpSound != null)
            {
                jumpSound.PlayOneShot(jumpSound.clip, 1f);
            }
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void FlipSprite(float moveInput)
    {
        if (moveInput > 0 && !facingRight)
        {
            facingRight = true;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else if (moveInput < 0 && facingRight)
        {
            facingRight = false;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            isTouchingWall = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false;
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            isTouchingWall = false;
        }
    }

    private void OnDisable()
    {
        if (jumpAction != null)
        {
            jumpAction.performed -= OnJumpPerformed;
            jumpAction.Disable();
        }

        if (moveAction != null)
        {
            moveAction.Disable();
        }
    }

    public bool IsFacingRight()
    {
        return facingRight;
    }
}