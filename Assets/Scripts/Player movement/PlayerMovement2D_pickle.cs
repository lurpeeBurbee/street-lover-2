using UnityEngine;

[ExecuteInEditMode]
public class PlayerMovement2D_pickle : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the player's movement
    [Header("Enable jump")]
    [SerializeField] bool canJump;
    [SerializeField] private float jumpForce = 10f; // Force applied to the player for jumping
    [SerializeField] private float wallSlideSpeed = 2f; // Speed at which player slides down the wall
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private bool isGrounded;
    private bool isTouchingWall;
    private float wallFriction = 1f;
    private bool facingRight = false;
    public AudioSource jumpSound; // Reference to the AudioSource for the jump sound


    private void Awake()
    {
        AddComponents();
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

        // Initialize facing direction based on the initial sprite orientation (facing left)
        facingRight = false;
    }

    private void Update()
    {
        // Handle horizontal movement
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed * wallFriction, rb.linearVelocity.y);

        HandleFriction();
        ControlJump();
        FlipSprite(moveInput);
    }

    private void AddComponents()
    {
        // Check if Rigidbody2D component is already attached
        if (GetComponent<Rigidbody2D>() == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            rb = GetComponent<Rigidbody2D>();
        }

        // Check if CapsuleCollider2D component is already attached
        if (GetComponent<CapsuleCollider2D>() == null)
        {
            gameObject.AddComponent<CapsuleCollider2D>();
        }
    }

    private void HandleFriction()
    {
        // Reduce wall friction if player is in air and touching wall
        if (!isGrounded && isTouchingWall)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -wallSlideSpeed); // Slide down the wall
            wallFriction = 0.5f;
        }
        else
        {
            wallFriction = 1f;
        }
    }
    private void ControlJump()
    {
        if (canJump && isGrounded && (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            // Play sound FIRST with forced immediate playback
            if (jumpSound != null)
            {
                jumpSound.PlayOneShot(jumpSound.clip, 1f); // Better for rapid playback
            }

            // Then apply force
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }
    private void FlipSprite(float moveInput)
    {
        // Flip the sprite based on horizontal movement direction
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
        // Check if the player is grounded or touching a wall
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
        // Check if the player is not grounded or not touching a wall
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false;
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            isTouchingWall = false;
        }
    }

    // Public method to get the current facing direction
    public bool IsFacingRight()
    {
        return facingRight;
    }
}