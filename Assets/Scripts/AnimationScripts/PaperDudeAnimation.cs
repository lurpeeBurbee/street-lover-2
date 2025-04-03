using UnityEngine;

public class PaperDudeAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator; // Reference to the Animator component
    [SerializeField] private PaperDudeMovement paperDudeMovement; // Reference to the PaperDudeMovement script

    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if (paperDudeMovement == null)
        {
            paperDudeMovement = GetComponent<PaperDudeMovement>();
        }
    }

    void Update()
    {
        // Check if the player is moving
        bool isMoving = Mathf.Abs(paperDudeMovement.rb.linearVelocity.x) > 0.1f;

        // Set the "walk" parameter in the Animator
        animator.SetBool("walk", isMoving);
    }
}
