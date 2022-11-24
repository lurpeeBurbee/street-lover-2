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
    int HealthPoint = 1;

    int HealthBarListIndex = 0;


    void ReduceHealth(int AnyGlobalVariable)
    {
        //---- Manageroivat Healthbaria ainoastaan:
          HealthBarList[HealthBarListIndex].SetActive(false);

        if(HealthBarListIndex < 3) {
          HealthBarListIndex += DamagePoint;
        }
    }

    void GainHealth()
    {
        HealthBarList[HealthBarListIndex].SetActive(true);

        if (HealthBarListIndex > 0)
        {
            HealthBarListIndex -= HealthPoint; // Edelleenkin sama kuin: HealthIndexLocal = HealthIndexLocal - 1
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) 
        {
            ReduceHealth(DamagePoint); //<-- ajetaan täällä
        }
        if (collision.gameObject.CompareTag("PaleAleHealth"))
        {
            Debug.Log("Gained health");
            GainHealth(); //<-- ajetaan täällä
        }
    }
    void Start()
    {

    }

    void Update()
    {

    }
}
