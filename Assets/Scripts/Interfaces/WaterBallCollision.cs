using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBallCollision : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
            if (collision.gameObject.TryGetComponent<IWaterable>(out var water))
            {
                water.WaterHit(1);
            }
    }
}
