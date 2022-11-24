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

        if (HealthBarListIndex < 3)
        {
            HealthBarListIndex += DamagePoint;
        }
    }

    void GainHealth(int AnyGlobalVariable)
    {

        if (HealthBarListIndex > 0)
        {
            HealthBarListIndex -= AnyGlobalVariable; // Edelleenkin sama kuin: HealthIndexLocal = HealthIndexLocal - 1
        }

        HealthBarList[HealthBarListIndex].SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            ReduceHealth(DamagePoint); //<-- ajetaan täällä
        }
        if (collision.gameObject.CompareTag("PaleAleHealth"))
        {
            //Debug.Log("Index is: " + HealthBarListIndex);
            GainHealth(HealthPoint); //<-- ajetaan täällä
        }
        if (collision.gameObject.CompareTag("MegaHealthBarrell"))
        {

            for (HealthBarListIndex = 0; HealthBarListIndex < 4; HealthBarListIndex++)
            {
                HealthBarList[HealthBarListIndex].SetActive(true);

                // GainHealth(HealthPoint); //<-- ajetaan täällä
            }

            HealthBarListIndex = 0; // <-- Nollataan indexi, jotta voidaan alkaa käsittelemään
            // HealthBaria jatkossakin alusta, eli indexistä 0, mikä on siis viimeinen palkki.
        }


    }
    void Start()
    {

    }

    void Update()
    {

    }
}
