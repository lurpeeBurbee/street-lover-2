using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedTicket : MonoBehaviour
{
    float MoveSpeed =0.1f;
    // Hidden in the inspector (Private variable). We don't need to see these, since we are only giving these a value by pressing a button.
    float leftmove = 0.0f;
    float rightmove = 0.0f;

    void MovePlayer()
    {
        // Moving left
        if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
        {
            leftmove -= MoveSpeed * Time.deltaTime;
         
         transform.Translate(leftmove , 0, 0);
           
        }
        // Moving right
        if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
        {
            rightmove += MoveSpeed * Time.deltaTime;

            transform.Translate(rightmove, 0, 0);

        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        MovePlayer();   
    }
}
