using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemSound : MonoBehaviour
{

    public AudioSource itemSound;
    public AudioClip itemsoundclip;
    public float pitchValue;

    public Transform groundCheck;
    public LayerMask groundLayer;

    bool DiamondHit()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 1f, groundLayer);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Trigger")) 
        {
            itemSound.pitch = pitchValue;
            itemSound.PlayOneShot(itemsoundclip);
            gameObject.SetActive(false);    
        
        }
    }

    private void Update()
    {
        if(DiamondHit())
        {
            itemSound.pitch = pitchValue;
            itemSound.PlayOneShot(itemsoundclip);
        }
    }

}
