using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioControl : MonoBehaviour {

    public AudioMixer audioMixer;

    public void MuteSelectedAudio(AudioSource source) { // valitse inspectorista, minkä Audio Sourcen halua mutettaa
    if(audioMixer != null)
        {
            if(source.isPlaying)
            {
                source.Stop();
            }
            else
            {
                source.Play();
            }
        }
    }

    public void MuteAudio(float audioVolume) {  
        if(audioMixer != null)
        {
            if(audioMixer.GetFloat("volume", out audioVolume) && audioVolume > -80) 
            {
                audioMixer.SetFloat("volume", -80);
            }
        }
    }



}
