using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaterable : MonoBehaviour, IWaterable
{
    public void EnemyDisappear()
    {
        gameObject.SetActive(false);
    }
    public void WaterHit()
    {
        EnemyDisappear();   
    }



}
