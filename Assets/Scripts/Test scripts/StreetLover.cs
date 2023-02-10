
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StreetLover : MonoBehaviour
{
    float MoveSpeed;

    int PlayerHealth;
    int MinHealth;
    float RunSpeed;
    bool IsOnAir;

   bool IsOnAirChecker()
    {
        return IsOnAir;

    }

    void HealthManager()
    {
        if (PlayerHealth > MinHealth) // TRUE
        {
            PlayerHealth -= 1;
            Debug.Log(PlayerHealth);
        }
        if (PlayerHealth == 0) // FALSE
        {
            Debug.Log("Game over");
            PlayerHealth -= 1;
        }


    } // -Funktio loppuu

    void Start()
    {
        MoveSpeed = 0;
        PlayerHealth = 100;
        MinHealth = 0;
        RunSpeed = 2;
        IsOnAir = false;


    }
    void Update()
    {
        HealthManager();
        Debug.Log(IsOnAirChecker());



        //Debug.Log("MoveSpeed is " + MoveSpeed);
        //print(PlayerHealth);
    }
}
