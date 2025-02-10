using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEars : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D objekti)
    {

        if (objekti.gameObject.CompareTag("Player"))
        {
            Debug.Log("Heard player");

        }
    }
}
