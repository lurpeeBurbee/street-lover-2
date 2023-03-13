using UnityEngine;

public class ProjectPlayerMove : MonoBehaviour
{
    public float moveSpeed = 0.1f;
    public float leftMove = 0f;
    public float rightMove = 0f;
    public float MoveConstraint;
    public float jumpForce;

    public Rigidbody2D rb;

    public Transform groundCheck;
    public LayerMask groundLayer;



    void MovePlayer()
    {
        if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.localScale = new Vector3(-5, 5, 5); // kääntää Spriten toiseen suuntaan
            // move left:
            if (leftMove >= -MoveConstraint)
            {
                leftMove -= moveSpeed * Time.deltaTime;

            }

            transform.Translate(leftMove, 0, 0);

        }

        // move right:
        if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
        {
            transform.localScale = new Vector3(5, 5, 5);

            if (rightMove <= MoveConstraint)
            {
                rightMove += moveSpeed * Time.deltaTime;
            }
            transform.Translate(rightMove, 0, 0);

        }
    }
    void JumpPlayer()
    {
        if (IsGrounded())
        {
            if (Input.GetKeyDown("space"))
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 1f, groundLayer);
    }

    void Start()
    {

    }

    void Update()
    {
        MovePlayer();
        JumpPlayer();
    }
}
