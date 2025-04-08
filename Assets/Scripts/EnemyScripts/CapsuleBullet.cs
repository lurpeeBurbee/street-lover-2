using UnityEngine;

public class CapsuleBullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float speed = 10f; // Speed of the bullet
    public float lifetime = 5f; // Lifetime of the bullet
    public LayerMask collisionLayers; // Layers the bullet reacts to

    private void Start()
    {
        // Destroy the bullet after its lifetime
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        // Move the bullet forward
        transform.Translate(speed * Time.deltaTime * Vector3.right);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the bullet collided with the specified layers
        if (((1 << collision.gameObject.layer) & collisionLayers) != 0)
        {
            // Log the collision
            Debug.Log("Bullet collided with: " + collision.gameObject.name);

            // Destroy the bullet on collision
            Destroy(gameObject);
        }
    }
}