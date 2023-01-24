using UnityEngine;
public class Fireballmove : MonoBehaviour
{
    public float speed = 20;

    void Update()
    {
        transform.Translate(transform.position.x * speed * Time.deltaTime, 0, 0);
    }
}