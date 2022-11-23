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
      //  for (CurrentHealth = MaxHealthNumber; CurrentHealth >= MinHealth; CurrentHealth--) 

        if(CurrentHealth > MinHealth)
        {

            Debug.Log(CurrentHealth);
   
        }
        return 0;
    }

    void Update()
    {
        HealthManager();
    }
}

