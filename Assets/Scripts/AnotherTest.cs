using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherTest : MonoBehaviour 
{

   int HealthNumber;
    int HealthPotion;
    float HealthExtra;

    void Start()
    {
        HealthNumber = 9;
        HealthPotion = 1;
        HealthExtra = 0.02f;

       Debug.Log(HealthManager(HealthNumber, HealthPotion, HealthExtra));

    }

    
    int HealthManager(int number1, int number2, float extra)
    {
        return number1 + number2;
    }

  
    void Update()
    {
        
    }

 
}

