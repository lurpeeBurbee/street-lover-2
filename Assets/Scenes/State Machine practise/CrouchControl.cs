using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchControl : MonoBehaviour
{
    public GameObject head;
    [SerializeField] Animator anim;

    CapsuleCollider2D playerCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Cave"))
        {
            anim.SetBool("Crouch", true);

            var isCrouching = anim.GetBool("IsCrouching");
            if(isCrouching)
            {
                playerCollider = GetComponent<CapsuleCollider2D>();
                playerCollider.enabled = !isCrouching; 
                Debug.Log("Is on crouching state");
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("CaveExit"))
        {
            anim.SetBool("Crouch", false);

            var isCrouching = anim.GetBool("IsCrouching");
            if (isCrouching == false)
            {
                playerCollider = GetComponent<CapsuleCollider2D>();
                playerCollider.enabled = true;
                Debug.Log("Exiting crouching state");
            }
        }
    }

    void Start()
    {
        anim = head.GetComponent<Animator>();    
    }

    void Update()
    {
        
    }
}
