using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectPlayerMove : MonoBehaviour
{
    public float moveSpeed = 0.1f;
    public float leftMove = 0f;
    public float rightMove = 0f;
    public float jumpForce;

    public Rigidbody2D rb;

    public Transform groundCheck;
    public LayerMask groundLayer;

    void MovePlayer()
    {
        if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
        {
            // move left:
            if (leftMove >= -0.1f)
            {
                leftMove -= moveSpeed * Time.deltaTime;
                transform.localScale = new Vector3(-1, 1, 1); // k‰‰nt‰‰ Spriten toiseen suuntaan
            }   
            
            transform.Translate(leftMove, 0, 0);
            
        }

        // move right:
        if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
        {

            if(rightMove <= 0.1f) {
            rightMove += moveSpeed;
            transform.localScale = new Vector3(1, 1, 1);
      
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

    bool IsGrounded() {
        return Physics2D.OverlapCircle(groundCheck.position, 4f, groundLayer);
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
