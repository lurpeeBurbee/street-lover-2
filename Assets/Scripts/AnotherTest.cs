using UnityEngine;

public class AnotherTest : MonoBehaviour
{

    int MaxHealthNumber;
    int MinHealth;
    int CurrentHealth;

    int damage;
    int HealthPotion;

  

    void Start()
    {
        MaxHealthNumber = 100;
        MinHealth = 0;
        CurrentHealth = MaxHealthNumber;

        damage = 5;
        HealthPotion = 10;

    }

    int HealthManager()
    {
        for (int number1 = 100; number1 >= -1; number1--)
        {
            Debug.Log(number1);
   
        }
        return 0;
    }

    void Update()
    {
        HealthManager();
    }
}

