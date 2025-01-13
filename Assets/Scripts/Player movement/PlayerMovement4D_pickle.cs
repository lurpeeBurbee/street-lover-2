using UnityEngine;

[ExecuteInEditMode]
public class PlayerMovement4D_pickle : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // Speed of the player's movement
    [SerializeField] private Rigidbody2D rb;
    private bool facingRight = false;

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
        rb.gravityScale = 0; // Set gravity to zero
    }

    private void Update()
    {
        // Handle horizontal and vertical movement
        float moveInputX = Input.GetAxis("Horizontal");
        float moveInputY = Input.GetAxis("Vertical");
        rb.linearVelocity = new Vector2(moveInputX * moveSpeed, moveInputY * moveSpeed);

        FlipSprite(moveInputX);
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

    private void FlipSprite(float moveInputX)
    {
        // Flip the sprite based on horizontal movement direction
        if (moveInputX > 0 && !facingRight)
        {
            facingRight = true;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else if (moveInputX < 0 && facingRight)
        {
            facingRight = false;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }
}
