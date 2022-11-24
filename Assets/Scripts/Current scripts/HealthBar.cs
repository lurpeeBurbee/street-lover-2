using System;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

   // int HealthIndexGlobal = 100;
    int SuperPowerUp = 120;

    [SerializeField]
    List<GameObject> HealthBarList;

    void HealthBarManager(int AnyGlobalVariable)
    {

        AnyGlobalVariable /= 100; // Täysin sama kuin: AnyGlobalVariable = AnyGlobalVariable / 100
        Debug.Log("AnyGlobalVariable: " + AnyGlobalVariable);

        //---- Manageroivat Healthbaria ainoastaan:
        int HealthIndexLocal = 0;
          HealthBarList[HealthIndexLocal].SetActive(false);
          HealthIndexLocal += 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) 
        {
            HealthBarManager(SuperPowerUp); //<-- ajetaan täällä
        }
    }
    void Start()
    {

    }

    void Update()
    {

    }
}
