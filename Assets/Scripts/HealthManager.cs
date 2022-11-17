using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    int playerHealth;
    int minHealth;
    string text;
    string sceneGameOver;

    [SerializeField]
    GameObject spikes;

   string GameOverText()
    {

        if (playerHealth <= minHealth)
        {
           // text = "Game over";
           // sceneGameOver = "GameOver";

            spikes.SetActive(false);

            // SceneManager.LoadScene(sceneGameOver);
        }

        return text;

    }

    void OnGUI()
    {
        GUI.contentColor = Color.green;
        GUI.backgroundColor = Color.black;
        GUI.skin.label.fontSize = 20;

        GUI.Label(new Rect(50, 20, 350, 100), "PlayerHealth value is: " + playerHealth);
      //  GUI.Label(new Rect(50, 40, 350, 100), GameOverText());
        // rightmove
        // GUI.Label(new Rect(50, 100, 350, 100), "isInAir value is: "); // ollaanko ilmassa? Luo itse uusi muuttuja
        // GUI.Label(new Rect(50, 120, 350, 100), "isGrounded value is: " + IsGrounded()); // ollaanko maassa?
    }


    void OnCollisionEnter2D(Collision2D objekti)
    {

        if (objekti.gameObject.CompareTag("Enemy") &&  playerHealth > minHealth ) // || <- jompikumpi, && kumpikin
        {

            playerHealth--;
            Debug.Log("PlayerHealt is " + playerHealth);
        }

        else
        {
            //Debug.Log("Game over");
        }

    }


    void Start()
    {



        playerHealth= 4;
        minHealth = 0;
    }



    void Update()
    {
        GameOverText();
    }
}
