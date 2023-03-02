using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Project_PlayerMove : MonoBehaviour
{
    public float moveSpeed = 0.1f;
    public float leftMove = 0f;
    public float rightMove = 0f;
    void MovePlayer()
    {
        if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
        {
            // move left:
            if (leftMove > -0.2f)
            {
                leftMove -= moveSpeed * Time.deltaTime;

                transform.localScale = new Vector3(-1, 1, 1); // k‰‰nt‰‰ Spriten toiseen suuntaan

                transform.Translate(leftMove, 0, 0, 0);
            }
        }
        // move right:
        if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
        {

            rightMove += moveSpeed;
            transform.localScale = new Vector3(1, 1, 1);
            transform.Translate(rightMove, 0, 0); ;
        }
    }
    void Start()
    {
        Debug.Log("Works");
    }

    void Update()
    {
        MovePlayer();
    }
}
