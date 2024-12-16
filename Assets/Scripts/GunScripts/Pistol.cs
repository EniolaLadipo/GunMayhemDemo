using UnityEngine;

public class Pistol : Gun
{
    public GameObject bulletPrefab;

    void Start()
    {
        gunName = "Pistol";
        magazineCount = 16;
        knockbackForce = 2;
        range = 25;
        bulletSpeed = 20;
        fireRate = 0.3f;
    }

    void Update()
    {

    }

    public override void Shoot(Vector2 direction)
    {
        if(bulletPrefab != null && muzzlePoint != null)
        {
            Debug.Log("Gun was shot");
            GameObject movingBullet = Instantiate(bulletPrefab, muzzlePoint.position, muzzlePoint.rotation);
            movingBullet.SetActive(true);


            Bullet bulletScript = movingBullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.setBulletVelocity(direction, bulletSpeed, knockbackForce);
            }

            Rigidbody2D rb = movingBullet.GetComponent<Rigidbody2D>();
            rb.gravityScale = 0;
        }
    }
}
