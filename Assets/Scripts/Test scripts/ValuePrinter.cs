using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class ValuePrinter : MonoBehaviour
{
    // string shout = "JEE!!";

    readonly int maxHealth = 10;
    readonly int minHealth = 0;
    //float movespeed = 10; <-- will be used later

    void PrintValue(int number1, string word)
    {

        Debug.Log(number1);
        Debug.Log(word);
      
    }

    void Start()
    {

        if (SceneManager.GetActiveScene().name == "StartScene")
        {
            Debug.Log("Playing start music");

        }

    }


    void Update()
    {
        PrintValue(maxHealth, "");

        if (maxHealth == minHealth)
        {
            Debug.Log("GAME OVER");
        }
        if (maxHealth <= 5)
        {
            Debug.Log("Player is wounded");
        }
        else
        {
            Debug.Log("Game is playing and the player is healthy & alive");
        }
    }

}