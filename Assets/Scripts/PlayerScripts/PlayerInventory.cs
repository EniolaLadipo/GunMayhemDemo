using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int gunCount = 0;
    public Transform gunHolder; // Reference to the GunHolder object
    private GameObject currentGun; // The currently equipped gun
    void Start()
    {
        
    }

    void Update()
    {
        FlipGun();
    }
    
    public void equipGun(GameObject gunPrefab)
    {
        // Remove the current gun if one exists
        if (currentGun != null)
        {
            Destroy(currentGun);
        }

        // Instantiate the new gun and parent it to the GunHolder
        currentGun = Instantiate(gunPrefab, gunHolder.position, gunHolder.rotation);
        currentGun.transform.SetParent(gunHolder, true); // Make it a child of GunHolder
    }

    public void FlipGun()
    {
        if (currentGun != null)
        {
            // Check player movement direction and flip the gun accordingly
            if (transform.localScale.x > 0)
            {
                currentGun.transform.localRotation = Quaternion.Euler(0, 0, 0); // Facing right
            }
            else
            {
                currentGun.transform.localRotation = Quaternion.Euler(0, 180, 0); // Facing left
            }
        }
    }
}
