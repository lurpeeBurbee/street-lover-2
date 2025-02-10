using UnityEngine;

public class Player2Movement : MonoBehaviour
{

    float Player2Xposition;
    float Player2Yposition; 

    void MovePlayer2()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Player2Xposition -= 0.04f;
            transform.position = new Vector2(Player2Xposition, transform.position.y);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Player2Xposition += 0.04f;
            transform.position = new Vector2(Player2Xposition, transform.position.y);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Player2Yposition += 0.04f;
            transform.position = new Vector2(transform.position.x, Player2Yposition);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Player2Yposition -= 0.04f;
            transform.position = new Vector2(transform.position.x, Player2Yposition);
        }
    }

    void Start()
    {

    }


    void Update()
    {
        MovePlayer2();
    }
}
