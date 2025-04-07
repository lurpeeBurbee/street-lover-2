using UnityEngine;

public class EasyShooting : MonoBehaviour
{
    [Header("Bullet Settings")]
    [Tooltip("Prefab of the bullet to be spawned.")]
    public GameObject bulletPrefab;

    [Tooltip("Point from where the bullet will be spawned.")]
    public Transform bulletSpawnPoint;

    [Tooltip("Time in seconds between shots.")]
    public float fireRate = 0.5f;

    private float nextFireTime = 0f;

    // Reference to the PlayerMovement script to get the facing direction
    private PlayerMovement2D_pickle playerMovement;

    void Start()
    {
        // Try to find the PlayerMovement2D_pickle script on the same GameObject or its parents
        playerMovement = GetComponentInParent<PlayerMovement2D_pickle>();
        if (playerMovement == null)
        {
            Debug.LogError("EasyShooting script needs to be on the same GameObject or a child of the GameObject with the PlayerMovement2D_pickle script.");
            enabled = false; // Disable the script if the dependency is not met
        }
    }

    void Update()
    {
        // Check if the fire rate has passed and either Ctrl key is pressed
        if (Time.time >= nextFireTime && (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl)))
        {
            Shoot();
            // Update the next allowed fire time
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        // Check if a bullet prefab and spawn point are assigned
        if (bulletPrefab == null || bulletSpawnPoint == null)
        {
            Debug.LogError("Bullet Prefab or Spawn Point not assigned in the Inspector!");
            return;
        }

        // Instantiate the bullet at the spawn point's position and rotation
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        // Get the EasyBullet script component from the spawned bullet

        // If the bullet has the EasyBullet script, set its direction. First we check if the bullet prefab has the EasyBullet script attached
        if (bullet.TryGetComponent<EasyBullet>(out var bulletScript))
        {
            // Get the facing direction from the PlayerMovement script
            if (playerMovement != null)
            {
                if (playerMovement.IsFacingRight())
                {
                    bulletScript.direction = Vector2.right;
                }
                else
                {
                    bulletScript.direction = Vector2.left;
                }
            }
            else
            {
                Debug.LogError("PlayerMovement2D_pickle script reference is null in EasyShooting.");
                // Fallback to a default direction if the player movement script is not found
                bulletScript.direction = Vector2.left;
            }
        }
        else
        {
            Debug.LogWarning("Bullet prefab does not have the EasyBullet script attached.");
        }
    }
}