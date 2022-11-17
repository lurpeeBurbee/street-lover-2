using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    int playerHealth;


    void OnCollisionEnter2D(Collision2D objekti)
    {

        if (objekti.gameObject.CompareTag("Enemy"))
        {
            playerHealth--;
            Debug.Log(playerHealth);
        }
    }


    void Start()
    {
        playerHealth= 10;
    }



    void Update()
    {
        
    }
}
