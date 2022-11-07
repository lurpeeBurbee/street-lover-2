using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterMovement1 : MonoBehaviour
{
    public float leftmove = 0.0f;
    public float rightmove = 0.0f;
    public float jump = 0.0f;
    public float jumpForce = 9.0f;

    public Rigidbody2D rb;

    public Transform groundCheck;
    public LayerMask groundLayer;

    void Start()
    {
        Looper(); // <-- Ilmoitetaan Start-funktiolle, että aja Looper läpi. Start tekee sen vain kerran, 
        // eikä looppaa ikuisesti, kuten Update-funktio. Raskas loop loopin sisällä voi kaataa koko pelin.
    }


    public void Looper()
    {
        for (int i=0; i<100;i++)
        {
        //    Debug.Log(i);
        }
       

    }


   public bool IsGrounded()
    {
        // groundCheck.position antaa groundCheckin sijainnin. 4f on ympyrämitta, kuinka laajalta alueelta tarkistetaan
        // ground-alue ja ollaanko siihen kosketuksissa. groundLayer = lattiaksi asetettu gameObject, jolle on määritelty
        // Layeriksi groundLayer
        return Physics2D.OverlapCircle(groundCheck.position, 4f, groundLayer);
    }
    void OnGUI()
    {
        GUI.contentColor = Color.green;
        GUI.backgroundColor = Color.black;
        GUI.skin.label.fontSize = 20;

        GUI.Label(new Rect(50, 20, 350, 100), "Leftmove value is: " + leftmove);
        GUI.Label(new Rect(50, 100, 350, 100), "jumpForee value is: " + jumpForce);
        GUI.Label(new Rect(50, 120, 350, 100), "isGrounded value is: ");

    }
    void Update()
    {
   

        if (Input.GetKey("a"))
        {
            // move left:
            leftmove -= 0.001f; 
           // transform.position = new Vector3(leftmove, 0, 0);
            transform.Translate(leftmove, 0, 0);
        }
        // move right:
        if (Input.GetKey("d"))
        {
            rightmove += 0.001f;
            transform.Translate(rightmove, 0, 0);
        }
        // move up:
        if (IsGrounded())
        {
            if (Input.GetKeyDown("space")) // tai KeyCode.Space
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

                //jump += 10f;
                //transform.Translate(0, jump, 0);
            }
        }
    }
}
