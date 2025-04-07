using UnityEngine;

public class EasyBullet : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("Speed at which the bullet travels.")]
    public float speed = 5f;

    [Tooltip("How long the bullet will exist in seconds before being destroyed.")]
    public float lifetime = 2f;

    [Header("Trigger Settings")]
    [Tooltip("Tag of the GameObjects this bullet can trigger with. Leave empty for any GameObject with a collider.")]
    public string targetTag = "";

    // Internal variable to store the bullet's movement direction
    [HideInInspector] public Vector2 direction = Vector2.right;

    private float startTime;

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        // Move the bullet based on its direction and speed
        transform.Translate(speed * Time.deltaTime * direction);

        // Destroy the bullet after its lifetime expires
        if (Time.time - startTime >= lifetime)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the targetTag is set and if the other GameObject has that tag
        if (targetTag == "" || other.CompareTag(targetTag))
        {
            // Log the name of the GameObject this bullet collided with
            Debug.Log("Bullet collided with: " + other.gameObject.name);
            other.gameObject.SetActive(false); // Deactivate the target GameObject  
                                               // You can add more actions here, like dealing damage, playing effects, etc.

            // Destroy the bullet upon collision (optional, you might want it to pass through or just disable the bullet game object)
            // Destroy(gameObject);
            gameObject.SetActive(false); // Deactivate the bullet instead of destroying it  
        }
    }
}