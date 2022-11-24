using System;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    int HealthIndexGlobal = 100;

    [SerializeField]
    List<GameObject> HealthBarList;

    void HealthBarManager(int AnyGlobalVariable)
    {
        AnyGlobalVariable /= 100; // Täysin sama kuin: AnyGlobalVariable = AnyGlobalVariable / 100
        Debug.Log("AnyGlobalVariable: " + AnyGlobalVariable);

        int HealthIndexLocal = 0;
          HealthBarList[HealthIndexLocal].SetActive(false);
          HealthIndexLocal += 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) 
        {
            HealthBarManager(100); //<-- ajetaan täällä
        }
    }
    void Start()
    {
   
    }

    void Update()
    {

    }
}
