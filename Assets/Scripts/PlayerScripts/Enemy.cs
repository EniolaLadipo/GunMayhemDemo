using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float knockbackResistance = 0.1f;
    public float moveSpeed = 2f;
    public Transform player;
    public float followRange = 10f;
    public float knockbackRecoveryTime = 1f;

    private Rigidbody2D rb;
    private bool isKnockedBack = false;      // Tracks if the enemy is currently knocked back
    private float knockbackTimer = 0f;
    private bool isFacingRight = false;       // Tracks the enemy's facing direction

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;

        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
        }
    }

    void Update()
    {
        if (isKnockedBack)
        {
            knockbackTimer -= Time.deltaTime;
            if (knockbackTimer <= 0f)
            {
                isKnockedBack = false; // End knockback state
            }
        }
        else
        {
            FollowPlayer();
        }
    }

    public void ApplyKnockback(Vector2 knockbackDirection, float knockbackForce)
    {
        if (rb != null)
        {
            isKnockedBack = true;
            knockbackTimer = knockbackRecoveryTime; // Reset the knockback timer
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
        }
    }

    void FollowPlayer()
    {
        if (player == null)
        {
            return; // No player to follow
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Only follow if within range
        if (distanceToPlayer <= followRange)
        {
            // Determine direction towards the player
            Vector2 direction = (player.position - transform.position).normalized;

            // Flip the enemy if necessary
            if (direction.x > 0 && !isFacingRight)
            {
                Flip();
            }
            else if (direction.x < 0 && isFacingRight)
            {
                Flip();
            }

            rb.linearVelocity = new Vector2(direction.x * moveSpeed, rb.linearVelocity.y);
        }
        else
        {
            // Stop moving when out of range
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 enemyScale = transform.localScale;
        enemyScale.x *= -1;
        transform.localScale = enemyScale;
    }
}
