using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float enemySpeed;

    public AudioSource enemyActive;
    public AudioSource backgroundmusic;

    void enemyMovement()
    {
        enemySpeed += 0.0001f;
        transform.Translate(enemySpeed, 0, 0);
    }

    void Start()
    {
        enemyActive.GetComponent<AudioSource>();
        backgroundmusic.GetComponent<AudioSource>();
        backgroundmusic.Stop(); 
        enemyActive.Play();
        enemySpeed= 0f;
    }

    void Update()
    {
        enemyMovement();
    }
}
