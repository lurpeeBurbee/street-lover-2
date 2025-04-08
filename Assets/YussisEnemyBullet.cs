
using UnityEngine;

public class YussisEnemyBullet : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D rb;
    public float force;
    private float timer;
    public PlayerHealth playerhealth;

    [Header("Rotation Adjustment")]
    public float rotationOffset = 90f; // Adjustable rotation offset

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        // Calculate the direction from the bullet to the player
        Vector3 direction = player.transform.position - transform.position;

        // Normalize the direction and apply force
        rb.linearVelocity = direction.normalized * force;

        // Calculate the rotation angle to face the player
        float rot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + rotationOffset);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > 10)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player hit!");

            // other.gameObject.GetComponent<PlayerHealth>().health -= 2;
            Destroy(gameObject);
        }
    }
}
    