using UnityEngine;

public class ItemSound : MonoBehaviour
{
    public AudioSource itemSound;
    public AudioClip itemsoundclip;

    public float maxPitchValue;
    public float minPitchValue; 

    public Transform groundCheck;
    public LayerMask groundLayer;

    bool DiamondHit()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 1f, groundLayer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Trigger"))
        {


            itemSound.PlayOneShot(itemsoundclip);
            gameObject.SetActive(false);

        }
    }

    private void Update()
    {
        if (DiamondHit())
        {
            if (!itemSound.isPlaying)
            {
                // Vaihtaa väriä
                var diamondRenderer = gameObject.GetComponent<Renderer>();
                diamondRenderer.material.SetColor("_Color", Color.red);

                // Soittaa soundin random pitch-taajuuksilla
                itemSound.pitch = Random.Range(minPitchValue, maxPitchValue);
                Debug.Log("Sound is playing with a pitch value of: " + itemSound.pitch);
                
                itemSound.PlayOneShot(itemsoundclip);
            }
        }
        else
        {
            itemSound.Stop();   
        }
    }

}
