using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSliderController : MonoBehaviour
{
    public AudioMixer audiomix;
    public void ControlAudio(float audioVolume)
    {
        audiomix.SetFloat("volume", audioVolume);
    }
}
