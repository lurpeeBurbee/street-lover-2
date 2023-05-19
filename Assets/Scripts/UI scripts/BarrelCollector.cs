using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelCollector : MonoBehaviour
{
    [SerializeField] UItoolkitControl uitoolkitcontrol;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PaleAleHealth"))
        {
            uitoolkitcontrol.BarrelControl();
            collision.gameObject.SetActive(false);  
        }
    }
    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
