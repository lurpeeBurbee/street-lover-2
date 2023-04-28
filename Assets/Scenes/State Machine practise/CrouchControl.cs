using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchControl : MonoBehaviour
{
    public GameObject head;
    [SerializeField] Animator anim;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Cave"))
        {
            anim.SetBool("Crouch", true);
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
