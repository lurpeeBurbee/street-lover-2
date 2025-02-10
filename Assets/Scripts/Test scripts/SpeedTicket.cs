using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedTicket : MonoBehaviour
{

    float MoveSpeed =0.15f;
    // Hidden in the inspector (Private variable). We don't need to see these,
    // since we are only giving these a value by pressing a button.
    public float leftmove = 0.0f;
    public float rightmove = 0.0f;
    float maxSpeedLeft = -0.1f;
    float maxSpeedRight = 0.1f;

    string stopword;

    void StopPrinter(string Shout)
    {
        Debug.Log(Shout);
    }

    void MovePlayer()
    {

        // Moving left
        if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
        {
            stopword = "STOP!!!";
            if (leftmove > maxSpeedLeft) { 
            leftmove -= MoveSpeed * Time.deltaTime;
         }
         transform.Translate(leftmove , 0, 0);
           
        }
        // Moving right
        if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
        {
            if (rightmove < maxSpeedRight)
            {
                rightmove += MoveSpeed * Time.deltaTime;
                 transform.Translate(rightmove, 0, 0);
            }
            else if(rightmove >= maxSpeedRight)
            {
                transform.Translate(0, 0, rightmove); // Saavuttaa maximiSpeed Right:in
             
                StopPrinter(stopword);
            }

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
