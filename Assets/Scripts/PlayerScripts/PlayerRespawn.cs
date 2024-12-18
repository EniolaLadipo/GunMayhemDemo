using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public Transform respawnPoint;  // Reference to the respawn point
    public float fallThreshold = -10f;  // Y position where the player falls off the map

    void Update()
    {
        // Check if the player falls below the fallThreshold
        if (transform.position.y < fallThreshold)
        {
            Respawn();
        }
    }

    // Respawn the player at the respawn point
    void Respawn()
    {
        if (respawnPoint == null)
        {
            Debug.LogError("Respawn Point is not assigned in the Inspector!");
            return;
        }

        // Debug log for testing
        Debug.Log("Respawning player at: " + respawnPoint.position);

        // Reset player's position to the respawn point
        transform.position = respawnPoint.position;

        // Reset Rigidbody2D velocity
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }
}
