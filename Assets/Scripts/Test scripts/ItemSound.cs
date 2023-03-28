using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemSound : MonoBehaviour
{
    public string scene;
    public AudioSource itemSound;
    public AudioClip itemsoundclip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            Debug.Log("Collided!");
            itemSound.PlayOneShot(itemsoundclip);
            SceneManager.LoadScene(scene);
        }
    }
    private void Start()
    {
        Debug.Log("Start works");
    }
}
