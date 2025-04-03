using UnityEngine;

public class PaperDudeMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // Speed of the player's movement
    public Rigidbody2D rb; // Reference to the Rigidbody2D component
    [SerializeField] private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component

    private bool facingRight = true; // Flag to check if the player is facing right

    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    void Update()
    {
        // Handle horizontal movement
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y * Time.deltaTime);

        // Flip the sprite based on horizontal movement direction
        if (moveInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && facingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        // Flip the sprite by inverting the X scale
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
