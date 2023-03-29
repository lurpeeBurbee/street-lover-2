using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemSound : MonoBehaviour
{
  //  public string scene;
    public AudioSource itemSound;
    public AudioClip itemsoundclip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            itemSound.pitch = 0.4f;
            itemSound.PlayOneShot(itemsoundclip);
            gameObject.SetActive(false);    
            //SceneManager.LoadScene(scene);
        }
    }
    private void Start()
    {
       
    }
}
