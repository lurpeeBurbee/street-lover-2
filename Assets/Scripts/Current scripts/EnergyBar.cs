using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public Image energyBar;
    public bool TimeReducer;
    public float waitTime = 20f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            energyBar.fillAmount -= 0.2f;
            // Sama kuin energyBar.fillAmount = energyBar.fillAmount (viimeisin arvo) jaettuna neljällä.
        }
        if (collision.CompareTag("PaleAleHealth"))
        {
            energyBar.fillAmount += 0.2f;
     }

    }

    void Update()
    {
        if(TimeReducer)
        {
            energyBar.fillAmount -= 1.0f /waitTime * Time.deltaTime;  
        }
    }
}
