using UnityEngine;

public class Pistol : Gun
{
    public GameObject bulletPrefab;

    void Start()
    {
        gunName = "Pistol";
        magazineCount = 16;
        knockbackForce = 2.0f;
        range = 25;
        bulletSpeed = 20;
        fireRate = 0.3f;
    }

    void Update()
    {

    }

    public override void Shoot(Vector2 direction)
    {

        if(magazineCount <= 0) //Don't shoot
        {
            return;
        }

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

            magazineCount--;
            Rigidbody2D rb = movingBullet.GetComponent<Rigidbody2D>();
            rb.gravityScale = 0;
        }
    }
}
