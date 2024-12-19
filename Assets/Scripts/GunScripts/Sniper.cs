using UnityEngine;

public class Sniper : Gun
{
    public GameObject bulletPrefab;

    void Start()
    {
        gunName = "Sniper";
        magazineCount = 7;
        knockbackForce = 5.0f;
        range = 25;
        bulletSpeed = 25;
        fireRate = 1.2f;
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
