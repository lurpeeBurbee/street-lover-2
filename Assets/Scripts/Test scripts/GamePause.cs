using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePause : MonoBehaviour
{

    void Update()
    {
        if(Input.GetKey(KeyCode.LeftControl))
        {
            Debug.Log("Game paused");
            Time.timeScale = 0f;
        }
    }
}
