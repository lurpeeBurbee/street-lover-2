using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public Image energyBar;
    public bool TimeReducer;
    public float waitTime = 20f;

    void Update()
    {
        if(TimeReducer)
        {
            energyBar.fillAmount -= 1.0f /waitTime * Time.deltaTime;  
        }
    }
}
