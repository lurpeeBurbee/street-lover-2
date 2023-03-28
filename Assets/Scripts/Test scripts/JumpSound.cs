using UnityEngine;

public class JumpSound : MonoBehaviour
{

    // Audio
    public AudioSource jumpsound;

    public AudioClip jumpsoundclip;
    Animator animator;

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            jumpsound.PlayOneShot(jumpsoundclip);

            animator.SetBool("ChestOpen", true);
        }
    }
}
