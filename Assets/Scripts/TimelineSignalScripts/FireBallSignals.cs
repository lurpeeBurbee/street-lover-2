using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FireBallSignals : MonoBehaviour
{
    Renderer rend;
    public void FireballMovement()
    {
        Debug.Log("Works");
    }

    void Start()
    {
        rend = GetComponent<Renderer>();    
    }

    void Update()
    {

    }
}
