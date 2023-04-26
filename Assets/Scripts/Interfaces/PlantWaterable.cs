using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantWaterable : MonoBehaviour, IWaterable
{
    [SerializeField] GameObject vegetation;
    void Start()
    {
        vegetation.SetActive(false);
    }
    public void PlantAppear()
    {
        vegetation.SetActive(true);
    }
    public void WaterHit(float waterHitValue)
    {
      PlantAppear();
    }

}
