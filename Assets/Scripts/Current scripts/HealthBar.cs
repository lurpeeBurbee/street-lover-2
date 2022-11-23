using System;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    int HealthIndex = 0;

    [SerializeField]
    List<GameObject> HealthBarList;
    void HealthBarManager()
    {
        HealthIndex += 1;

          HealthBarList[HealthIndex].SetActive(false);
        //Debug.Log(HealthBarList[i]);
        

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) 
        {
            HealthBarManager();
        }
    }
    void Start()
    {
   
    }

    void Update()
    {

    }
}
