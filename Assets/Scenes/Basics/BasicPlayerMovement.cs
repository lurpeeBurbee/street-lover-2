using UnityEngine;

public class BasicPlayerMovement : MonoBehaviour
{
    // yleisimm�t muuttujat (variables) ja niille annettu arvo:
    public string playername = "PlayerHeroBlue";
    public int minHealth;
    public int playerHealth;
    public float playerMovement; // koska public, arvon voi antaa my�s inspect
    public bool playerIsMoving;
    int numberi1, numberi2;  // samat tyypit voi laittaa samalle riville alustuksessa.
    // Ei kuitenkaan funktion parametrina (sulkujen sis�ll� olevat muuttujat)


    int PrintNumber(int number1, int number2) // type on kokonaisluku, jolloin palautettava arvo
                                              // pit�� olla my�s kokonaisluku
    {
        int sumOfNumber = number1 + number2;
        return sumOfNumber;
    }

    void PlayerHealthManager()
    {
        playerHealth += 1;

    }

    void Start()
    {
        playerHealth = minHealth; // kummankin pit�� olla samaa type� (int)

        if (10 + 10 == 20)
        {
            playerIsMoving = true;
        }

    }

    void Update()
    {
        Debug.Log("PrintNumber value is " + PrintNumber(10, 10));
       PlayerHealthManager();

    }
}
