using UnityEngine;

public class Collector : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<ICollectable>(out var collidedObject))
        {
            collidedObject.Collect();
        }
    }
}
