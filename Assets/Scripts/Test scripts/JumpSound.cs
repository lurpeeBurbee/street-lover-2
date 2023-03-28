using UnityEngine;

public class JumpSound : MonoBehaviour
{

    // Audio
    public AudioSource jumpsound;

    public AudioClip jumpsoundclip;


    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            jumpsound.PlayOneShot(jumpsoundclip);
        }
    }
}
