using UnityEngine;

public class Rifle : Gun
{
    public GameObject bulletPrefab;

    void Start()
    {
        gunName = "Rifle";
        magazineCount = 30;
        knockbackForce = 0.8f;
        range = 25;
        bulletSpeed = 20;
        fireRate = 0.15f;
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
