using UnityEngine;

public class CharacterMovement1 : MonoBehaviour
{
    public float leftmove = 0.0f;
    public float rightmove = 0.0f;
    public float jump = 0.0f;
    public float jumpForce;

    int private_numberi;

    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;





    void Start()
    {

        //  Looper(); // <-- Ilmoitetaan Start-funktiolle, ett� aja Looper l�pi. Start tekee sen vain kerran, 
        // eik� looppaa ikuisesti, kuten Update-funktio. Raskas loop loopin sis�ll� voi kaataa koko pelin.
        jumpForce = 0f;
    }


    public void Looper()
    {
        for (int i = 101; i > 100; i++)
        {
            //    Debug.Log(i);
        }
    }


    bool IsGrounded()
    {
        // groundCheck.position antaa groundCheckin sijainnin. 4f on ympyr�mitta, kuinka laajalta alueelta tarkistetaan
        // ground-alue ja ollaanko siihen kosketuksissa. groundLayer = lattiaksi asetettu gameObject, jolle on m��ritelty
        // Layeriksi groundLayer

        // t�nne jotain toimintoa, kun painetaan Space. Kopioi valmis if tuolta alhaalta.

        return Physics2D.OverlapCircle(groundCheck.position, 4f, groundLayer);

    }

    //void OnGUI()
    //{
    //    GUI.contentColor = Color.green;
    //    GUI.backgroundColor = Color.black;
    //    GUI.skin.label.fontSize = 20;

    //    GUI.Label(new Rect(50, 20, 350, 100), "Leftmove value is: " + leftmove);
    //    // rightmove
    //   // GUI.Label(new Rect(50, 100, 350, 100), "isInAir value is: "); // ollaanko ilmassa? Luo itse uusi muuttuja
    //   // GUI.Label(new Rect(50, 120, 350, 100), "isGrounded value is: " + IsGrounded()); // ollaanko maassa?
    //}

    // T�h�n funktio, mik� liikuttaa pelaajaa. Aseta se Updateen.
    // Muista bracketit kiinni my�s!

    // (funktion Type) + (Funktion nimi esim. AngelinanFunktio) + () <- sulkujen sis��n tulee parametrit, nyt ei viel� tarvita.
    // { <- bracket auki

    // Kopioi if-ehdot t�nne

    // } < - bracket kiinni

    void MovePlayer()
    {
        if (Input.GetKey("a"))
        {
            // move left:
            //leftmove = -0.05f;
            leftmove -= 0.0005f;

            // transform.position = new Vector3(leftmove, 0, 0);

            // transform.localScale = new Vector3(4f, 8f, 0);

            transform.Translate(leftmove, 0, 0);
        }
        // move right:
        if (Input.GetKey("d"))
        {
            // rightmove = 0.05f;
            rightmove += 0.0005f;
            //  transform.localScale = new Vector3(1f, 2f, 0);

            transform.Translate(rightmove, 0, 0);
        }
    }
    void JumpPlayer()
    {
        if (IsGrounded())
        {
            // ei saa hypp�� ilmassa

            if (Input.GetKeyDown("space")) // tai KeyCode.Space
            {

                //   rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

                jumpForce += 0.84f;
                transform.Translate(0, jumpForce, 0);

            }  // If input... etc. loppuu


        }  // IsGrounded loppuu

    }

    void Update()
    {
        // Jonkun niminen move-script esim. CharacterMove();
        // isGrounded(); <-- for testing purposes, delete when jumping works
        MovePlayer();
        JumpPlayer();
    }
    void FixedUpdate()
    {
        
    }

}
