using UnityEngine;

public class AnalogDigital : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    private bool hasController;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Check if any joysticks/controllers are connected
        hasController = Input.GetJoystickNames().Length > 0;
    }

    void Update()
    {
        float h; // Horizontal input value

        if (hasController)
        {
            // Use GetAxis for smooth analog stick movement
            h = Input.GetAxis("Horizontal");
        }
        else
        {
            // Use GetAxisRaw for snappy, instant keyboard movement
            h = Input.GetAxisRaw("Horizontal");
        }

        rb.linearVelocity = new Vector2(h * speed, rb.linearVelocity.y);
    }
}