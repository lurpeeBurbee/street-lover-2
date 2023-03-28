using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSound : MonoBehaviour
{

    public AudioSource itemSound;
    public AudioClip itemsoundclip;

    void Start()
    {
        itemSound.Play();
    }


    void Update()
    {
        
    }
}
