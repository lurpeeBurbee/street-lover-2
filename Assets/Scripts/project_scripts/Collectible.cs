using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    //--- AUDIO
    public AudioSource itemsound;
    public AudioClip itemClip;
    public Renderer rend;
    private void Start()
    {
        rend = GetComponent<Renderer>();    
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            itemsound.PlayOneShot(itemClip);
            rend.enabled = false;
           // gameObject.SetActive(false);
        }
    }




}
