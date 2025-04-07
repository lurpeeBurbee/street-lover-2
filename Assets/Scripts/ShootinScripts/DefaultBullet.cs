using UnityEngine;

// Ensures the GameObject has these components when DefaultBullet is added.
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))] // Assuming CircleCollider2D, but Collider2D is more general
public class DefaultBullet : MonoBehaviour
{
    private float lifetimeRemaining;
    private bool destroyOnHit;
    private bool disableOnExpire;
    private bool isActive = true; // To prevent multiple hits/expiry actions


    /// Initializes the bullet's parameters.

    /// <param name="life">How long the bullet should exist.</param>
    /// <param name="shouldDestroyOnHit">Should the bullet be destroyed immediately on hit?</param>
    /// <param name="shouldDisableOnExpire">Should the bullet be disabled (for pooling) on expiry?</param>
    public void Initialize(float life, bool shouldDestroyOnHit, bool shouldDisableOnExpire)
    {
        lifetimeRemaining = life;
        destroyOnHit = shouldDestroyOnHit;
        disableOnExpire = shouldDisableOnExpire;
        isActive = true; // Reset active state if reused from a pool
    }

    void Update()
    {
        if (!isActive) return; // Don't process if already handled

        lifetimeRemaining -= Time.deltaTime;
        if (lifetimeRemaining <= 0f)
        {
            HandleExpiry();
        }
    }

    // This method runs IF the Collider2D is set to be a Trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isActive) return; // Prevent duplicate triggers
        // No need to check isTrigger here, this method only runs for triggers.
        HandleHit(other.gameObject);
    }

    // This method runs IF the Collider2D is NOT set to be a Trigger
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isActive) return; // Prevent duplicate collisions
        // No need to check !isTrigger here, this method only runs for non-triggers.
        HandleHit(collision.gameObject);
    }

    void HandleHit(GameObject hitObject)
    {
        // Optional: Check if the hit object is something we care about (e.g., an enemy)
        // if (!hitObject.CompareTag("Enemy") && !hitObject.CompareTag("Shootable"))
        // {
        //     return; // Ignore collision with objects not tagged correctly
        // }

        Debug.Log($"Bullet has touched a gameobject named: {hitObject.name}");

        isActive = false; // Deactivate to prevent further actions

        if (destroyOnHit)
        {
            Destroy(gameObject);
        }
        else
        {
            // Disable for potential object pooling
            gameObject.SetActive(false);
            // Note: If you implement pooling, return the object to the pool here.
        }
    }

    void HandleExpiry()
    {
        isActive = false; // Deactivate to prevent further actions

        if (disableOnExpire)
        {
            // Disable for potential object pooling
            gameObject.SetActive(false);
            // Note: If you implement pooling, return the object to the pool here.
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Optional: If using pooling, you might add a method like this
    // void OnDisable()
    // {
    //    // Reset state if needed when disabled (e.g., cancel invokes, reset velocity)
    //    Rigidbody2D rb = GetComponent<Rigidbody2D>();
    //    if (rb != null) rb.velocity = Vector2.zero;
    //    // Return to pool would happen here or in HandleHit/HandleExpiry
    // }

    // void OnEnable()
    // {
    //     // If object pooling is used, re-initialize or reset state when enabled.
    //     // Note: Initialization often happens externally when getting from the pool.
    // }
}