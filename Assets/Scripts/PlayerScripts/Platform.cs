using UnityEngine;
using System.Collections;

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
        if (Input.GetKeyDown(KeyCode.DownArrow) && currentPlatform != null && !dropping)
        {
            StartCoroutine(DropThroughPlatform());
        }
    }

    private IEnumerator DropThroughPlatform()
    {
        dropping = true;

        // Temporarily disable collision between player and current platform
        Collider2D platformCollider = currentPlatform.GetComponent<Collider2D>();
        if (platformCollider != null)
        {
            Physics2D.IgnoreCollision(playerCollider, platformCollider, true);
        }

        yield return new WaitForSeconds(dropCooldown);

        // Re-enable collision
        if (platformCollider != null)
        {
            Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
        }

        dropping = false;
    }

    // Detect when the player is on a platform
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            // If the player is on top of the platform, record it
            currentPlatform = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == currentPlatform)
        {
            // Reset the current platform when leaving it
            currentPlatform = null;
        }
    }
}
