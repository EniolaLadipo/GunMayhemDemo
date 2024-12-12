using UnityEngine;

public class Rifle : Gun
{
    public GameObject bulletPrefab;

    void Start()
    {
        gunName = "Rifle";
        magazineCount = 30;
        knockbackForce = 15;
        range = 50;
        bulletSpeed = 5;
        fireRate = 0.1f;
    }

    void Update()
    {

    }

    public override void Shoot(Vector2 direction)
    {
        if (bulletPrefab != null && muzzlePoint != null)
        {
            GameObject movingBullet = Instantiate(bulletPrefab, muzzlePoint.position, muzzlePoint.rotation);
            movingBullet.SetActive(true);

            Rigidbody2D rb = movingBullet.GetComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            rb.linearVelocity = direction.normalized * bulletSpeed;
        }
    }
}
