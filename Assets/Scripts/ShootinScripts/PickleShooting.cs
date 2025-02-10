using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickleShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletStartPoint;
    public float canShoot = 0.5f;
    private float shootTimer;

    void Update()
    {
        shootTimer += Time.deltaTime;

        if ((Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl)) && shootTimer >= canShoot)
        {
            shootTimer = 0f;
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletStartPoint.position, bulletStartPoint.rotation);
        PickleBullet bulletScript = bullet.GetComponent<PickleBullet>();

        // Adjust the direction based on localScale and the initial left-facing direction
        if (transform.localScale.x > 0)
        {
            bulletScript.direction = Vector2.left;
        }
        else
        {
            bulletScript.direction = Vector2.right;
        }
    }
}
