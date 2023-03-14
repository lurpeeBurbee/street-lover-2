using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeHandler : MonoBehaviour
{
    public float seconds;

    IEnumerator IncreaseNumber(int coroutineNumber)
    {
        while (true)
        {
            coroutineNumber++;
            yield return new WaitForSeconds(seconds);

            transform.position = new Vector2(coroutineNumber/10, transform.position.y);

            Debug.Log("coroutineNumber is " +  coroutineNumber);
        }
    }


    void Start()
    {
        StartCoroutine(IncreaseNumber(0));
    }



}
