using System.Collections;
using UnityEngine;

public class PatrollingEnemy : MonoBehaviour
{

    float EnemyMoveValue;
    public float EnemyMoveSpeed;

    public bool EnemyIsMovingRight;
    public bool EnemyIsMovingLeft;

    public int enemyWaitSeconds;

    IEnumerator EnemyWaitingOnRight()
    {
        while (true)
        {

            yield return new WaitForSeconds(enemyWaitSeconds);
            Debug.Log("Enemy has waited on right" + enemyWaitSeconds);
            EnemyIsMovingLeft = true;
        }
    }
    IEnumerator EnemyWaitingOnLeft()
    {
        while (true)
        {
            yield return new WaitForSeconds(enemyWaitSeconds);
            Debug.Log("Enemy has waited on left" + enemyWaitSeconds);
            EnemyIsMovingRight = true;
        }
    }
    void Start()
    {
        EnemyIsMovingRight = true;
        EnemyIsMovingLeft = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            Debug.Log("Collided with " + collision.gameObject.name);
            if (collision.gameObject.name == "Right wall")
            {
                EnemyIsMovingRight = false;
                StartCoroutine(EnemyWaitingOnRight());
            }
            if (collision.gameObject.name == "Left wall")
            {
                EnemyIsMovingLeft = false;
                StartCoroutine(EnemyWaitingOnLeft());
            }


        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        StopAllCoroutines();
    }

    void Update()
    {
        if (EnemyIsMovingRight)
        {
            EnemyMoveValue += EnemyMoveSpeed * Time.deltaTime;
        }
        if (EnemyIsMovingLeft)
        {
            EnemyMoveValue -= EnemyMoveSpeed * Time.deltaTime;
        }

        transform.position = new Vector2(EnemyMoveValue, transform.position.y);
    }
}
