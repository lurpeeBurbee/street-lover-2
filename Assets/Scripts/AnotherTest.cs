using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherTest : MonoBehaviour
{

    int HealthNumber;
    int HealthPotion;
    float HealthExtra;
    string loppu = "Loppu";
    void Start()
    {
        HealthNumber = 100;
        HealthPotion = 1;
        HealthExtra = 0.02f;

       Debug.Log(HealthManager());

    }

    int HealthManager()
    {
        for (int number1 = 100; number1 >= -1; number1--)
        {
            Debug.Log(number1);
        }

        return 10000;
       
        // return number1 + number2;

    }


    void Update()
    {

    }


}

