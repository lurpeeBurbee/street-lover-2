using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [Header("References")]
    public Transform player; // Reference to the player
    public Transform nozzle; // Reference to the nozzle
    public GameObject bulletPrefab; // Reference to the bullet prefab

    [Header("Shooting Settings")]
    public float shootingInterval = 2f; // Time between shots
    public float bulletLifetime = 5f; // Lifetime of the bullet

    private float shootingTimer; // Timer to track shooting interval

    void Update()
    {
        // Calculate the direction from the enemy to the player
        Vector3 direction = player.position - transform.position;

        // Calculate the angle to rotate the enemy
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotate the enemy to face the player
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Ensure the nozzle points towards the player
        if (nozzle != null)
        {
            nozzle.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        // Handle shooting
        shootingTimer += Time.deltaTime;
        if (shootingTimer >= shootingInterval)
        {
            Shoot();
            shootingTimer = 0f;
        }
    }

    void Shoot()
    {
        // Instantiate the bullet at the nozzle's position and rotation
        GameObject bullet = Instantiate(bulletPrefab, nozzle.position, nozzle.rotation);

        // Set the bullet to be destroyed after its lifetime
        Destroy(bullet, bulletLifetime);
    }
}