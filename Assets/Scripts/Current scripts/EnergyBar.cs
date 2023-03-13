using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
            // Sama kuin energyBar.fillAmount = energyBar.fillAmount (viimeisin arvo) vähennettynä 0.2:lla.

            if (energyBar.fillAmount <= 0)
            {
                SceneManager.LoadScene("GameOver");
            }

        }
        if (collision.CompareTag("PaleAleHealth"))
        {
            energyBar.fillAmount += 0.2f;
     }

    }

    void Update()
    {
        if(TimeReducer) // Lähtee tiputtamaan energyBarin arvoa
        {
            energyBar.fillAmount -= 1.0f /waitTime * Time.deltaTime;  
        }
    }
}
