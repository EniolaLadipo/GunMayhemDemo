using UnityEngine;

public class GunPickup : MonoBehaviour
{
    public GameObject gunPrefab;

    void Start()
    {
        if (gunPrefab == null)
        {
            Debug.LogError("Gun object is not assigned");
        }
    }

    void Update()
    {
        // You can add additional logic here if needed
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerInventory playerInventory = collision.GetComponent<PlayerInventory>();
            if (playerInventory != null)
            {
                playerInventory.equipGun(gunPrefab); // Equip the new gun, whether it's a pistol, rifle, etc.
            }
            Destroy(gameObject); // Destroy the gun pickup object
        }
    }
}
