using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StartEnemyMove : MonoBehaviour
{
    private PlayableDirector director;
    public GameObject controlPanel;

    private void Awake()
    {
        director = GetComponent<PlayableDirector>();
        director.played += Director_Played;
        director.stopped += Director_Stopped;
    }

    private void Director_Stopped(PlayableDirector obj)
    {
       //controlPanel?.SetActive(true);

    }

    private void Director_Played(PlayableDirector obj)
    {
        //  controlPanel?.SetActive(false);
        StartTrain();
    }

    public void StartTrain()
    {
        director.Play();
        Debug.Log("Train start");
    }
}