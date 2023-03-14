using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemy : MonoBehaviour
{

    float EnemyMoveValue;
    public float EnemyMoveSpeed;
    public bool EnemyIsMoving;

    void Start()
    {
        EnemyIsMoving = true;   
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Wall"))
        {
            EnemyIsMoving = false;
        }
    }

    void Update()
    {
        if (EnemyIsMoving)
        {
            EnemyMoveValue += EnemyMoveSpeed * Time.deltaTime;
        }
       
        transform.position = new Vector2(EnemyMoveValue, transform.position.y);
    }
}
