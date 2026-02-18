using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AmmoMove : MonoBehaviour
{
    [SerializeField] private float ammoSpeed = 10f;

    // We make this public so the Player can set it when spawning the bullet
    public void Launch(bool isFacingRight)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 0; // Keeping it simple for the test

        // Manually set the direction based on the player's state
        float direction = isFacingRight ? 1f : -1f;
        rb.linearVelocity = new Vector2(direction * ammoSpeed, 0);

        // Optional: Flip the bullet sprite too so it looks correct
        Vector3 s = transform.localScale;
        s.x = direction;
        transform.localScale = s;

        Destroy(gameObject, 2f); // Destroy the bullet after 2 seconds to prevent clutter 
    }
}