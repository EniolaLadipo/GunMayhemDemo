using UnityEngine;

public class PowerUp : MonoBehaviour 
{
    public enum PowerUpType { Speed, Jetpack, DoubleJump }
    public PowerUpType powerUpType;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();

            if (playerInventory != null)
            {
                playerInventory.ActivatePowerUp(this);

                Destroy(gameObject);
            }
        }
    }
}