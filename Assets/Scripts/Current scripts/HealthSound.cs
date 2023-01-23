using UnityEngine;

public class HealthSound : MonoBehaviour
{
    public AudioSource healthaudio;
    public AudioClip healthClip;

    private void OnCollisionEnter2D(Collision2D player)
    {
        if (player.gameObject.CompareTag("Player"))
        {
           healthaudio.PlayOneShot(healthClip, 0.6f);  
        }
    }




    void Update()
    {

    }
}
