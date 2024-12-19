using UnityEngine;

public class ShotgunShell : MonoBehaviour
{
    public float maxLifetime = 1.5f;
    public float maxRange = 5f;
    public float knockback;
    public float speed;
    public Vector2 direction;

    private Vector2 startPosition;

    void Start()
    {
        // Store the starting position when the bullet is instantiated
        startPosition = transform.position;

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
        Destroy(gameObject);
    }

    public void setBulletVelocity(Vector2 direction, float speed, float knockback, float maxRange)
    {
        this.direction = direction;
        this.speed = speed;
        this.knockback = knockback;
        this.maxRange = maxRange;
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
