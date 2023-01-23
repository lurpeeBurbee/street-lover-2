using UnityEngine;

public class PlayerMovement:MonoBehaviour
{
    // Hidden in the inspector (Private variable). We don't need to see these, since we are only giving these a value by pressing a button.
    float leftmove = 0.0f;
    float rightmove = 0.0f;

    public float moveSpeed;

    public float jumpForce;

    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public LayerMask jumpableObject;

    //--- AUDIO
  //  public AudioSource jumpsound;


    private void Awake()
    {
      //  jumpsound  = GetComponent<AudioSource>();
    }
    void Start()
    {
   
    }




    bool IsGrounded()
    {
        // groundCheck.position antaa groundCheckin sijainnin. 4f on ympyr‰mitta, kuinka laajalta alueelta tarkistetaan
        // ground-alue ja ollaanko siihen kosketuksissa. groundLayer = lattiaksi asetettu gameObject, jolle on m‰‰ritelty
        // Layeriksi groundLayer

        // t‰nne jotain toimintoa, kun painetaan Space. Kopioi valmis if tuolta alhaalta.

        return Physics2D.OverlapCircle(groundCheck.position, 4f, groundLayer);

    }
    bool IsOnJumpableObject()
    {
        // groundCheck.position antaa groundCheckin sijainnin. 4f on ympyr‰mitta, kuinka laajalta alueelta tarkistetaan
        // ground-alue ja ollaanko siihen kosketuksissa. groundLayer = lattiaksi asetettu gameObject, jolle on m‰‰ritelty
        // Layeriksi groundLayer

        // t‰nne jotain toimintoa, kun painetaan Space. Kopioi valmis if tuolta alhaalta.

        return Physics2D.OverlapCircle(groundCheck.position, 4f, jumpableObject);

    }




    // T‰h‰n funktio, mik‰ liikuttaa pelaajaa. Aseta se Updateen.
    // Muista laittaa bracketit kiinni

    void MovePlayer()
    {
        if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
        {
            // move left:

            leftmove = -moveSpeed; 
            transform.localScale= new Vector3(-1, 1,1); // k‰‰nt‰‰ Spriten toiseen suuntaan

            transform.Translate(leftmove, 0, 0);
        }
        // move right:
        if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
        {

            rightmove = moveSpeed;
            transform.localScale = new Vector3(1, 1, 1);
            transform.Translate(rightmove, 0, 0);
        }
    }
    void JumpPlayer()
    {
        if (IsGrounded() || IsOnJumpableObject())
        {
            // ei saa hypp‰‰ ilmassa

            if (Input.GetKeyDown("space")) // tai KeyCode.Space
            {
             // jumpsound.Play();   
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);


            } 


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
