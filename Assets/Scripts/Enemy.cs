using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float enemySpeed;
    int enemyPoints;

    void enemyMovement()
    {
        enemySpeed += 0.0001f;
        transform.Translate(enemySpeed, 0, 0);
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        Debug.Log("HIT!");
    //    }
    //}

    //void OnCollisionEnter2D(Collision2D col)
    //{
    //    EnemyHit();
    //    Debug.Log(enemyPoints);
    //}

    void EnemyHit() {
        enemyPoints++; // sama kuin: enemypoints += 1;
    }

    void Start()
    {

        enemySpeed= 0f;
        enemyPoints = 0;
    }

    void Update()
    {
        enemyMovement();
    }
}
