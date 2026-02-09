using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveKinematic : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;

    void Start() => rb = GetComponent<Rigidbody2D>();

    void FixedUpdate()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        Vector2 movement = speed * Time.fixedDeltaTime * new Vector2(inputX, 0);
        rb.MovePosition(rb.position + movement);
    }
}