using UnityEngine;

public class GunPickup : MonoBehaviour
{
    public GameObject gunPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(gunPrefab == null)
        {
            Debug.LogError("Gun object is not assigned");
        }
    }

    // Update is called once per frame
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
