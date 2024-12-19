using UnityEngine;

public class Shotgun : Gun
{
    public GameObject bulletPrefab;

    void Start()
    {
        gunName = "Shotgun";
        magazineCount = 8;
        knockbackForce = 3.0f;
        range = 10; // Shorter range for shotgun
        bulletSpeed = 15;
        fireRate = 1f;
    }

    public override void Shoot(Vector2 direction)
    {
        if (magazineCount <= 0)
        {
            return;
        }

        if (bulletPrefab != null && muzzlePoint != null)
        {
            Debug.Log("Shotgun fired!");

            // Create multiple bullets for a shotgun spread
            for (int i = -2; i <= 2; i++) // Creates 5 bullets in a cone pattern
            {
                float spreadAngle = i * 10f; // Spread arc
                Vector2 spreadDirection = Quaternion.Euler(0, 0, spreadAngle) * direction;

                GameObject movingBullet = Instantiate(bulletPrefab, muzzlePoint.position, muzzlePoint.rotation);
                movingBullet.SetActive(true);

                ShotgunShell bulletScript = movingBullet.GetComponent<ShotgunShell>();
                if (bulletScript != null)
                {
                    bulletScript.setBulletVelocity(spreadDirection, bulletSpeed, knockbackForce, range);
                }

                Rigidbody2D rb = movingBullet.GetComponent<Rigidbody2D>();
                rb.gravityScale = 0;
            }

            magazineCount--;
        }
    }
}
