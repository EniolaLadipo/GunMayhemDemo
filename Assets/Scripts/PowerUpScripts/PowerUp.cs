using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpType { Speed, Jetpack, DoubleJump }
    public PowerUpType powerUpType; // Type of this power-up

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player has collided with the power-up
        if (other.CompareTag("Player"))
        {
            // Get the PlayerInventory component from the player
            PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();

            if (playerInventory != null)
            {
                // Call the method to activate the power-up on the player
                playerInventory.ActivatePowerUp(this);

                // Destroy the power-up after activation
                Destroy(gameObject);
            }
        }
    }
}