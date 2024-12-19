using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public Transform respawnPoint;
    public float fallThreshold = -6f;  // Y position where the player falls off the map

    void Update()
    {
        if (transform.position.y < fallThreshold)
        {
            Respawn();
        }
    }

    void Respawn()
    {
        if (respawnPoint == null)
        {
            Debug.LogError("Respawn Point is not assigned in the Inspector");
            return;
        }

        Debug.Log("Respawning player at: " + respawnPoint.position);

        // Reset player's position to the respawn point
        transform.position = respawnPoint.position;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }
}
