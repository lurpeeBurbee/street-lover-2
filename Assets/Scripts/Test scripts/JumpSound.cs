using UnityEngine;

public class JumpSound : MonoBehaviour
{

 
    public AudioSource jumpsound;
    public AudioClip jumpsoundclip;
    private readonly Animator animator;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpsound.PlayOneShot(jumpsoundclip);

        }
    }
}
