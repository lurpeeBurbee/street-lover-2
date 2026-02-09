using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveForce : MonoBehaviour
{
    public float thrustForce = 20f;
    private Rigidbody2D rb;

    void Start() => rb = GetComponent<Rigidbody2D>();

    void FixedUpdate()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        rb.AddForce(inputX * thrustForce * Vector2.right, ForceMode2D.Force);
    }
}