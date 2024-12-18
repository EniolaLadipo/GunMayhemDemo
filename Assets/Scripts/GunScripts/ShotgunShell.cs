using UnityEngine;

public class ShotgunShell : MonoBehaviour
{
    public float maxLifetime = 1.5f;    // Maximum time bullet exists
    public float maxRange = 5f;     // Maximum range the bullet can travel
    public float knockback;          // Knockback force applied to enemies
    public float speed;              // Bullet speed
    public Vector2 direction;        // Direction the bullet travels

    private Vector2 startPosition;   // Initial position of the bullet

    void Start()
    {
        // Store the starting position when the bullet is instantiated
        startPosition = transform.position;

        // Destroy the bullet after maxLifetime as a fallback
        Destroy(gameObject, maxLifetime);
    }

    void Update()
    {
        MoveBullet();
        CheckRange();
    }

    void MoveBullet()
    {
        Vector2 newPosition = (Vector2)transform.position + direction * speed * Time.deltaTime;
        transform.position = newPosition;
    }

    void CheckRange()
    {
        // Calculate the distance traveled from the starting position
        float distanceTraveled = Vector2.Distance(startPosition, transform.position);

        // Destroy the bullet if it exceeds its maximum range
        if (distanceTraveled >= maxRange)
        {
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        // Destroy the bullet if it goes off-screen
        Destroy(gameObject);
    }

    public void setBulletVelocity(Vector2 direction, float speed, float knockback, float maxRange)
    {
        this.direction = direction;
        this.speed = speed;
        this.knockback = knockback;
        this.maxRange = maxRange; // Pass the range dynamically for different weapons
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.ApplyKnockback(direction, knockback);
            }

            Destroy(gameObject);
        }
    }
}
