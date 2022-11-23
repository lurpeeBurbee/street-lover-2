using UnityEngine;

public class TestScript : MonoBehaviour
{

    string word = "Working";
    float energy = 100;
    float position = 10.5f;
    bool canSpeak = false;


    void Start()
    {
        if (canSpeak == true)
        { // !canSpeak = canSpeak == false

            Debug.Log(word);
            print(energy);
            print(position);
            print(canSpeak);
        }

       
    }



    void Update()
    {
        if (energy == 0)
        {
            energy -= 0.001f;
            print(energy);
        }
    }
}
