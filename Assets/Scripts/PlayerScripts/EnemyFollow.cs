using UnityEngine;


// This script does work but stops the enemy from taking knockback from the bullet, it overwrites the force and keeps moving towards the player
public class EnemyMovement : MonoBehaviour
{
    public Transform player;  // The player's position
    public float moveSpeed = 3f;  // Speed of the enemy
    private Rigidbody2D rb;  // Reference to the enemy's Rigidbody2D
    private bool isFacingRight = false;

    void Start()
    {
        // Get the Rigidbody2D component attached to the enemy
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Call the FollowPlayer function every frame
        FollowPlayer();
    }

    void FollowPlayer()
    {
        if (player != null)
        {
            // Calculate the direction to the player
            Vector2 direction = (player.position - transform.position).normalized;

            // Apply the movement
            rb.linearVelocity = new Vector2(direction.x * moveSpeed, rb.linearVelocity.y);

            // Flip the enemy depending on the direction of movement
            if (direction.x > 0 && !isFacingRight)
            {
                Flip();
            }
            else if (direction.x < 0 && isFacingRight)
            {
                Flip();
            }
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 enemyScale = transform.localScale;
        enemyScale.x *= -1; // Invert the X axis to flip the enemy
        transform.localScale = enemyScale;
    }
}
