using UnityEngine;

public class Effects : MonoBehaviour
{
    public GameObject barrellEffect;

    private void OnCollisionEnter2D(Collision2D barrell)
    {
        if (barrell.gameObject.CompareTag("PaleAleHealth"))
        {
            barrellEffect.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D barrell)
    {
        if (barrell.gameObject.CompareTag("PaleAleHealth"))
        {
            barrellEffect.SetActive(false);
        }
    }


    void Start()
    {

    }

    void Update()
    {

    }
}
