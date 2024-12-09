using UnityEngine;

public class GunPickup : MonoBehaviour
{
    public GameObject gunPrefab;
    void Start()
    {
        if(gunPrefab == null)
        {
            Debug.LogError("Gun object is not assigned");
        }
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            PlayerInventory playerInventory = collision.GetComponent<PlayerInventory>();
            if(playerInventory != null)
            {
                playerInventory.equipGun(gunPrefab);
            }
            Destroy(gameObject);
        }
    }
}
