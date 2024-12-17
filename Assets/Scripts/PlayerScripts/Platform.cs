using UnityEngine;

public class Platform : MonoBehaviour
{
    private GameObject currentPlatform;      // The platform the player is standing on
    private BoxCollider2D playerCollider;    // The player's collider
    private float dropCooldown = 0.8f;       // Time before re-enabling collision
    private bool dropping = false;           // Tracks if the player is currently dropping

    void Start()
    {
        playerCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        // Check for "down key" input and ensure the player is standing on a platform
        if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && currentPlatform != null && !dropping)
        {
            StartCoroutine(DropThroughPlatform());
        }
    }

    private System.Collections.IEnumerator DropThroughPlatform()
    {
        dropping = true;

        // Temporarily disable collision between player and current platform
        Collider2D platformCollider = currentPlatform.GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(playerCollider, platformCollider, true);

        // Wait for cooldown before re-enabling collision
        yield return new WaitForSeconds(dropCooldown);

        // Re-enable collision
        Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
        dropping = false;
    }

    // Detect when the player is on a platform
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            currentPlatform = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == currentPlatform)
        {
            currentPlatform = null;
        }
    }
}