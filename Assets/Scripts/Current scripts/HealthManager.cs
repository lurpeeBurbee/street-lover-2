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

    string gameOverText;

    //[SerializeField]
    //GameObject HealthBar4;
    //[SerializeField]
    //GameObject HealthBar3;
    //[SerializeField]
    //GameObject HealthBar2;
    //[SerializeField]
    //GameObject HealthBar1;


    int numberOne=1;
    int numberTwo=2;

    string UpdateTest()
    {
       // Debug.Log(HealthBarList[0]);

       // Debug.Log("UpdateTest() works!");
        return "Text!";
    
    }

    int CalculateTwoNumbers(int firstNumber, string successText) { //<-- parametrit

        //Debug.Log(successText);

        return firstNumber;

        
    }


    void HealthBarManager()
    {
        for (int i = 4; i>=0; i--)
        {
            Debug.Log(i);
        }
       //Debug.Log(HealthBarList[0]);



        //if (playerHealth == 3)
        //{
        //    HealthBar4.SetActive(false);
        //}
        //if (playerHealth == 2) 
        //{
        //HealthBar3.SetActive(false);
        //}


    }

   string GameOverText()
    {
        if (playerHealth <= minHealth)
        {
            // text = "Game over";
            // sceneGameOver = "GameOver";
            gameOverText = "Game over";
            // SceneManager.LoadScene(sceneGameOver);
        }
        return gameOverText;
    }

    void OnGUI()
    {
        GUI.contentColor = Color.green;
        GUI.backgroundColor = Color.black;
        GUI.skin.label.fontSize = 20;

        GUI.Label(new Rect(50, 20, 350, 100), "PlayerHealth value is: " + playerHealth + "%");
        GUI.Label(new Rect(50, 40, 350, 100), "" + gameOverText);
        GUI.Label(new Rect(50, 60, 350, 100), "" + CalculateTwoNumbers(100, ""));

        //GUI.Label(new Rect(50, 40, 350, 100), GameOverText());
        // rightmove
        // GUI.Label(new Rect(50, 100, 350, 100), "isInAir value is: "); // ollaanko ilmassa? Luo itse uusi muuttuja
        // GUI.Label(new Rect(50, 120, 350, 100), "isGrounded value is: " + IsGrounded()); // ollaanko maassa?
    }


    void OnCollisionEnter2D(Collision2D objekti)
    {

        if (objekti.gameObject.CompareTag("Enemy") &&  playerHealth > minHealth ) // || <- jompikumpi, && kumpikin
        {

            playerHealth--;
          //  HealthBarManager();
            Debug.Log("PlayerHealt is " + playerHealth);
        }

     else
        {

        }

    }


    void Start()
    {
        gameOverText = "";
        playerHealth= 4;
        minHealth = 0;
    }



    void Update()
    {
        GameOverText();
        UpdateTest();
    }
}
