using UnityEngine;

public class DefaultShooting : MonoBehaviour
{
    [Header("Ammo Settings")]
    [Tooltip("Prefab to use for bullets (leave empty to create runtime objects)")]
    public GameObject bulletPrefab;
    [Tooltip("Where bullets will spawn")]
    public Transform shootPoint;
    [Tooltip("Time between shots")]
    public float fireRate = 0.5f;
    [Tooltip("How long bullets exist before being destroyed/disabled")]
    public float bulletLifetime = 2f;
    [Tooltip("Size of the bullet")]
    public float bulletSize = 0.5f;
    [Tooltip("Color of the bullet")]
    public Color bulletColor = Color.white;

    [Header("Collision Mode")]
    [Tooltip("Use trigger instead of collider")]
    public bool useTrigger = true;
    [Tooltip("Destroy bullet on hit (false to disable)")]
    public bool destroyOnHit = true;
    [Tooltip("Disable bullet on expire (false to destroy)")]
    public bool disableOnExpire = true;

    private float shootTimer;

    void Update()
    {
        shootTimer += Time.deltaTime;

        if ((Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl)) && shootTimer >= fireRate)
        {
            shootTimer = 0f;
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet;

        // Create bullet from prefab or runtime
        if (bulletPrefab != null)
        {
            bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        }
        else
        {
            bullet = new GameObject("Bullet");
            bullet.transform.SetPositionAndRotation(shootPoint.position, shootPoint.rotation);

            // Add visual components
            var renderer = bullet.AddComponent<SpriteRenderer>();
            renderer.sprite = Sprite.Create(
                Texture2D.whiteTexture,
                new Rect(0, 0, 1, 1),
                new Vector2(0.5f, 0.5f)
            );
            renderer.color = bulletColor;

            // Add physics
            var collider = bullet.AddComponent<CircleCollider2D>();
            collider.isTrigger = useTrigger;
        }

        // Scale bullet
        bullet.transform.localScale = Vector3.one * bulletSize;

        // Add bullet behavior
        var bulletScript = bullet.AddComponent<DefaultBullet>();
        bulletScript.Initialize(bulletLifetime, destroyOnHit, disableOnExpire);
    }
}
