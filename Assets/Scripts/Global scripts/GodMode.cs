
using UnityEngine;

public class GodMode : MonoBehaviour
{
    [Header("Enable isGodModeOn to unleash immortality. There can be only one!")]
    [Space(6)]
    public bool isGodModeOn = false;


    void Update()
    {
        if (isGodModeOn)
        {
            // Implement god mode functionality here
            Debug.Log("God Mode is ON: You are immortal!");
        }
        else
        {
            // Implement normal functionality here
            Debug.Log("God Mode is OFF: You are mortal.");
        }
    }

}