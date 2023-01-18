using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trigger : MonoBehaviour
{
    public string scene;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Trigger"))

        {
            SceneManager.LoadScene(scene);
            Debug.Log("Loading");
        }
    }
}
