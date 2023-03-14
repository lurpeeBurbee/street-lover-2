using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemy : MonoBehaviour
{

    float EnemyMoveValue;
    public float EnemyMoveSpeed;
    public bool EnemyIsMovingRight;
    public bool EnemyIsMovingLeft;

    public int enemyWaitSeconds;

    IEnumerator EnemyWaiting()
    {
        while (true)
        {
            yield return new WaitForSeconds(enemyWaitSeconds);
            Debug.Log("Enemy has waited " + enemyWaitSeconds);
        }
    }

    void Start()
    {
        EnemyIsMovingRight = true;   
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Wall"))
        {
            EnemyIsMovingRight = false;
            StartCoroutine(EnemyWaiting());
        }
    }

    void Update()
    {
        if (EnemyIsMovingRight)
        {
            EnemyMoveValue += EnemyMoveSpeed * Time.deltaTime;
        }
       
        transform.position = new Vector2(EnemyMoveValue, transform.position.y);
    }
}
