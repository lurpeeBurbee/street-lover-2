using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    public GameObject head;
    public Animator anim;
    CapsuleCollider2D headCollider;
    [SerializeField]
    SimpleMovement simplemovement;
    float walkingSpeed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Cave"))
        {
           anim.SetBool("Crouch", true);

            var isCrouching = anim.GetBool("IsCrouching");

            if (isCrouching)
            {
                headCollider.enabled = !isCrouching;
                gameObject.SetActive(!isCrouching);
                //walkingSpeed = 0;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        anim.SetBool("Crouch", false);  
        headCollider.enabled = true;    

    }

    void Start()
    {
        // Vinkki:
            var someWord = new string("Joo");
        anim = head.GetComponent<Animator>();    
        headCollider = GetComponent<CapsuleCollider2D>();

    }
    void Update()
    {
        //walkingSpeed = GetComponent<SimpleMovement>().moveSpeed;
    }
}
