using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float enemySpeed;

    void enemyMovement()
    {
        enemySpeed += 0.0001f;
        transform.Translate(enemySpeed, 0, 0);
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player")) {
    //        Debug.Log("HIT!");
    //    }
    //}

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("HIT!");
    }

    void Start()
    {
        enemySpeed= 0f;
    }

    void Update()
    {
        enemyMovement();
    }
}
