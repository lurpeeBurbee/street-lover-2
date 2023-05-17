using UnityEngine;
using UnityEngine.Audio;

public class AudioMute : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void MuteSelectedAudio(AudioSource source) // valitse inspectorista, minkä Audio Sourcen halua mutettaa
    {
        if (source.isPlaying)
        {
            source.Stop();
        }
        else
        {
            source.Play();
        }
    }

    public void MuteAllAudio()
    {
        float vol;

        if (audioMixer != null)

        {
            if (audioMixer.GetFloat("volume", out vol) && vol > -80) audioMixer.SetFloat("volume", -80);
            else if(audioMixer.GetFloat("volume", out vol) && vol <0) audioMixer.SetFloat("volume", 0f);
            
        }
    }
}
