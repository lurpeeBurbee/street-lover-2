using System;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

   // int HealthIndexGlobal = 100;
    float SuperPowerUp = 120f;

    [SerializeField]
    List<GameObject> HealthBarList;

    int DamagePoint = 1;
    int HealthIndexLocal = 0;


    void ReduceHealth(int AnyGlobalVariable)
    {
        //---- Manageroivat Healthbaria ainoastaan:
          HealthBarList[HealthIndexLocal].SetActive(false);

        if(HealthIndexLocal < 3) {
          HealthIndexLocal += DamagePoint;
        }
    }

    void GainHealth()
    {
        HealthBarList[HealthIndexLocal].SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) 
        {
            ReduceHealth(DamagePoint); //<-- ajetaan t��ll�
        }
        if (collision.gameObject.CompareTag("PaleAleHealth"))
        {
            GainHealth(); //<-- ajetaan t��ll�
        }
    }
    void Start()
    {

    }

    void Update()
    {

    }
}
