using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickleBullet : MonoBehaviour
{
    public float speed = 5f;
    public float lifetime = 3f;
    public Vector2 direction = Vector2.right;
    public float shrinkSpeed = 1f;
    private Rigidbody2D rb;
    private Vector3 initialScale;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }
        rb.gravityScale = 1; // Set gravity scale to simulate throwing arc
        initialScale = transform.localScale;
    }

    void Start()
    {
        rb.AddForce(direction * speed, ForceMode2D.Impulse);
        StartCoroutine(DestroyBullet());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyNinja"))
        {
            // Handle collision with enemy ninja
        }
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(lifetime);

        while (transform.localScale.x > 0.01f)
        {
            transform.localScale -= shrinkSpeed * Time.deltaTime * initialScale;
            yield return null;
        }

        Destroy(gameObject);
    }
}
