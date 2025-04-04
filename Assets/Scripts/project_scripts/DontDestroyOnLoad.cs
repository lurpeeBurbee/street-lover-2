using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{

    void Start()
    {
        Debug.Log(gameObject.name + " is not destroyed on load.");  
        DontDestroyOnLoad(this);    
    }


}
