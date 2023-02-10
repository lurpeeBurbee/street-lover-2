using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class RollingPlayerMove : MonoBehaviour
{
    public float moveSpeed;
    float leftmove;
    float rightmove;
    float leftRotation;
    float rightRotation;
    public float rotationSpeed;
    public GameObject rotatingFace;
    public Rigidbody2D rb;
    public float jumpForce; 
    void Jump()
    {
        if (Input.GetKeyDown("space")) // tai KeyCode.Space
        {


            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);


        }
    }
    void MovePlayer()
    {
        if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
        {
            // move left:

           leftmove = -moveSpeed * Time.deltaTime;
           leftRotation = rotationSpeed * Time.deltaTime;   
           transform.Translate(leftmove, 0, 0, Space.World); // Only on Left
          rotatingFace.transform.Rotate(0, 0, leftRotation);
        }
        // move right:
        if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
        {

            rightmove = moveSpeed * Time.deltaTime;
           rightRotation= rotationSpeed * Time.deltaTime;  
            transform.Translate(rightmove, 0, 0);
       
       rotatingFace.transform.Rotate(0, 0, rightRotation * -1);
        }
    }
    void Start()
    {
        rotatingFace.GetComponent<GameObject>();  
    }

    void Update()
    {
        Jump();
        MovePlayer();
    }
}
