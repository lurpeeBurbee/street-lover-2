using UnityEngine;

public class PlayerMoveOnly : MonoBehaviour
{
    public float moveSpeed = 0.1f;
    public float leftMove = 0f;
    public float rightMove = 0f;
    public float MoveConstraint;

    void MovePlayer()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            // move left:
            if (leftMove >= -MoveConstraint)
            {
                leftMove -= moveSpeed * Time.deltaTime;
            }
            transform.Translate(leftMove, 0, 0);
        }
        // move right:
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (rightMove <= MoveConstraint)
            {
                rightMove += moveSpeed * Time.deltaTime;
            }
            transform.Translate(rightMove, 0, 0);

        }
    }
    void Update()
    {
        MovePlayer();
    }
}
