using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class StartEnemyMove : MonoBehaviour
{
    public GameObject enemy;
    public Enemy enemyscript;

    public void EnableEnemy()
    {
        enemyscript.enabled = true;
    }

    private void Start()
    {

        enemy.GetComponent<GameObject>();

        enemyscript = enemy.GetComponent<Enemy>();
      enemyscript.enabled = false;    
    }

}
