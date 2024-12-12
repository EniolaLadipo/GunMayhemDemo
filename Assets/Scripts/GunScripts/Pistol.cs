using UnityEngine;

public class Pistol : Gun
{
    public GameObject bulletPrefab;

    void Start()
    {
        gunName = "Pistol";
        magazineCount = 16;
        knockbackForce = 10;
        range = 25;
        bulletSpeed = 2;
        fireRate = 0.3f;
    }

    void Update()
    {

    }

    public override void Shoot(Vector2 direction)
    {
        if(bulletPrefab != null && muzzlePoint != null)
        {
            GameObject movingBullet = Instantiate(bulletPrefab, muzzlePoint.position, muzzlePoint.rotation);
            movingBullet.SetActive(true);

            Rigidbody2D rb = movingBullet.GetComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            rb.AddForce(direction.normalized * bulletSpeed, ForceMode2D.Impulse);
        }
    }
}
