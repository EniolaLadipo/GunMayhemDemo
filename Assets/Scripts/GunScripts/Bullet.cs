using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float maxLifetime = 2f;
    public float knockback;
    public float speed;
    public Vector2 direction;

    void Start()
    {
        Destroy(gameObject, maxLifetime);
    }

    void Update()
    {
        MoveBullet();
    }

    void MoveBullet()
    {
        Vector2 newPosition = (Vector2)transform.position + direction * speed * Time.deltaTime;
        transform.position = newPosition;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void setBulletVelocity(Vector2 direction, float speed, float knockback)
    {
        this.direction = direction;
        this.speed = speed;
        this.knockback = knockback;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if(enemy != null)
            {
                enemy.ApplyKnockback(direction, knockback);
            }

            Destroy(gameObject);
        }
    }
}