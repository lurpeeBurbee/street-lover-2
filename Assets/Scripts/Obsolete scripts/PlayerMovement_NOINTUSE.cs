//using UnityEngine;
//using UnityEngine.InputSystem;

//public class PlayerMovement : MonoBehaviour
//{
//    public Rigidbody2D rb;
//    public Transform groundCheck;
//    public LayerMask groundLayer;

//    private float horizontal;
//    private float vertical;
//    private float speed = 8f;
//    private float jumpingPower = 20f;
//    private bool isFacingRight = true;

//    private void Start()
//    {
//        Debug.Log("Started Start()");
//        ShowHealth();
//    }

//    public void ShowHealth()
//    {
//        if (rb != null)
//        {
//           // print(PlayerHealth.health);
//        }

//    }
//    void Update()
//    {

//        if (!isFacingRight && horizontal > 0f)
//        {
//            Flip();
//        }
//        else if (isFacingRight && horizontal < 0f)
//        {
//            Flip();
//        }
//    }


//    // Displays text on screen. An alternative to Debug.Log or print.
//    void OnGUI()
//    {
//        GUI.contentColor = Color.green;
//        GUI.backgroundColor = Color.black;
//        GUI.skin.label.fontSize = 30;

//        GUI.Label(new Rect(50, 50, 350, 100), "Horizontal value is: " + horizontal.ToString()); // everything must be of string type
//        // right value
//        GUI.Label(new Rect(50, 100, 350, 100), "Vertical value is: " + vertical.ToString());
//        // 
//    }

//    private void FixedUpdate()
//    {
//        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
//        vertical = transform.position.y;
//        vertical = Mathf.Ceil(vertical) * 1;


//    }

//    //public void Jump(InputAction.CallbackContext context)
//    //{
//    //    if (context.performed && IsGrounded())
//    //    {
//    //        rb.velocity = new Vector2(rb.velocity.x, jumpingPower);

//    //        print("Jumped");
//    //    }

//    //    if (context.canceled && rb.velocity.y > 0f)
//    //    {
//    //        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
//    //    }
//    //}

//    private bool IsGrounded()
//    {
//        return Physics2D.OverlapCircle(groundCheck.position, 4.2f, groundLayer);
//    }

//    private void Flip()
//    {
//        isFacingRight = !isFacingRight;
//        Vector3 localScale = transform.localScale;
//        localScale.x *= -1f;
//        transform.localScale = localScale;
//    }

//    //public void Move(InputAction.CallbackContext context)
//    //{
//    //    horizontal = context.ReadValue<Vector2>().x;
//    //}
//}