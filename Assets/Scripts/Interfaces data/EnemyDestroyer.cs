
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroyer : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<IWaterable>(out var enemyobject))
        {
            Debug.Log("Collided with enemy");
            enemyobject.WaterHit();
        }
    }
}
