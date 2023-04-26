using UnityEngine;

public class PlayerMovement : MonoBehaviour
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

    public bool isFacingRight;



    //--- AUDIO
    public AudioSource jumpsound;
    public AudioClip jumpClip;


    private void Awake()
    {
        //  jumpsound  = GetComponent<AudioSource>();
    }
    void Start()
    {
        isFacingRight = true;

        //  DontDestroyOnLoad(gameObject);
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

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            // move left:

            leftmove = -moveSpeed;
            transform.Translate(leftmove, 0, 0);
            transform.localScale = new Vector3(-1, 1, 1); // k‰‰nt‰‰ Spriten menosuuntaan
            isFacingRight = false;
        }
        // move right:
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            rightmove = +moveSpeed;
            transform.Translate(rightmove, 0, 0);
            transform.localScale = new Vector3(1, 1, 1); // k‰‰nt‰‰ Spriten menosuuntaan
            isFacingRight = true;
        }
    }
    void JumpPlayer()
    {
        if (IsGrounded() || IsOnJumpableObject())
        {
            // ei saa hypp‰‰ ilmassa

            if (Input.GetKeyDown("space")) // tai KeyCode.Space
            {
                //  jumpsound.PlayOneShot(jumpClip);

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


}
