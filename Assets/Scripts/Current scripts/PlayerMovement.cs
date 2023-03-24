using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Hidden in the inspector (Private variable). We don't need to see these, since we are only giving these a value by pressing a button.
    float leftmove = 0.0f;
    float rightmove = 0.0f;
    public float moveSpeed;
   [SerializeField] float superSpeedMoveControl; // T‰t‰ k‰ytet‰‰n argumenttina PlayerIsMovingSuperSpeed-funktiossa.
    public float jumpForce;

    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public LayerMask jumpableObject;

    float superSpeed = 0f;

 

    //--- AUDIO
    public AudioSource jumpsound;
    public AudioClip jumpClip;


    private void Awake()
    {
        //  jumpsound  = GetComponent<AudioSource>();
    }
    void Start()
    {

        
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

    float PlayerIsMovingSuperSpeed(float speed) //<-- Lis‰‰ t‰h‰n mik‰ vain muuttuja tyyppi‰ float.Voit myˆs antaa arvon t‰ss‰
    {
        superSpeed -=  speed * Time.deltaTime;
        return superSpeed;
    }

    void MovePlayer()
    {
        if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
        {
            // move left:

            leftmove =-moveSpeed;
            gameObject.transform.Translate(PlayerIsMovingSuperSpeed(superSpeedMoveControl), 0, 0);
            transform.localScale = new Vector3(-1, 1, 1); // k‰‰nt‰‰ Spriten toiseen suuntaan

        }
        // move right:
        if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
        {

            if (1 + 1 == 3)
            {
                rightmove = +moveSpeed;
                transform.Translate(rightmove, 0, 0);
            }
            if(1+1 == 2 || 1 +2 == 3)
            {
                rightmove = +moveSpeed;
                transform.Translate(rightmove, 0, 0);
            }

            transform.localScale = new Vector3(1, 1, 1);

        }
    }
    void JumpPlayer()
    {
        if (IsGrounded() || IsOnJumpableObject())
        {
            // ei saa hypp‰‰ ilmassa

            if (Input.GetKeyDown("space")) // tai KeyCode.Space
            {
                jumpsound.PlayOneShot(jumpClip);

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
