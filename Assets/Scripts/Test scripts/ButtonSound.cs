using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public AudioSource buttonClickSound;
    public AudioSource backgroundSound;


    //public float volume;
    public void PlayButtonClickSound()
    {

        buttonClickSound.Play();  
    }
    public void StopAudio()
    {
        backgroundSound.Stop(); 
    }

    public void ToggleAudio()
    {
        if(backgroundSound.isPlaying)
        {
            backgroundSound.Stop();
        }
        else
        {
            backgroundSound.Play(); 
        }

    }

    public void ReduceAudioVolume(float vol)
    {
        backgroundSound.volume -= vol;
    }
    public void IncreaseAudioVolume(float vol)
    {
        backgroundSound.volume += vol;
    }
}
