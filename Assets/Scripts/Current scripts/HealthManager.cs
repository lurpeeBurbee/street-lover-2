using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    int playerHealth;
    int minHealth;

    public AudioSource healthMinus;

    string gameOverText;


    string GameOverText()
    {
        if (playerHealth <= minHealth)
        {
            // text = "Game over";
            // sceneGameOver = "GameOver";
            gameOverText = "Game over";
            SceneManager.LoadScene("GameOver");
        }
        return gameOverText;
    }

    void OnGUI()
    {
        GUI.contentColor = Color.green;
        GUI.backgroundColor = Color.black;
        GUI.skin.label.fontSize = 20;

        GUI.Label(new Rect(500, 20, 350, 100), "PlayerHealth value is: " + playerHealth + "%");
        GUI.Label(new Rect(500, 40, 350, 100), "" + gameOverText);

        //GUI.Label(new Rect(50, 40, 350, 100), GameOverText());
        // rightmove
        // GUI.Label(new Rect(50, 100, 350, 100), "isInAir value is: "); // ollaanko ilmassa? Luo itse uusi muuttuja
        // GUI.Label(new Rect(50, 120, 350, 100), "isGrounded value is: " + IsGrounded()); // ollaanko maassa?
    }


    void OnCollisionEnter2D(Collision2D objekti)
    {

        if (objekti.gameObject.CompareTag("Enemy") && playerHealth > minHealth) // || <- jompikumpi, && kumpikin
        {
            healthMinus.Play();
            playerHealth -= 25;
            //  HealthBarManager();
            Debug.Log("PlayerHealt is " + playerHealth);
        }


        else
        {

        }

    }


    void Start()
    {
        healthMinus = GetComponent<AudioSource>();
        gameOverText = "";
        playerHealth = 100;
        minHealth = 0;
    }



    void Update()
    {
        GameOverText();
    }
}
