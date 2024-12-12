using UnityEngine;
public class PlayerInventory : MonoBehaviour
{
    public int gunCount = 0;
    public Transform gunHolder; // Reference to the GunHolder object
    private GameObject currentGun; // The currently equipped gun

    public float lastTimeShot = 0f;
    void Start()
    {
        
    }

    void Update()
    {
        FlipGun();

        if(Input.GetKey(KeyCode.E))
        {
            FireWeapon();
        }
    }
    
    public void equipGun(GameObject gunPrefab)
    {
        // Remove the current gun if one exists
        if (currentGun != null)
        {
            Destroy(currentGun);
        }

        gunCount++;
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

    void FireWeapon()
    {
        if(currentGun != null)
        {
            Gun equippedGun = currentGun.GetComponent<Gun>();

            if(equippedGun != null) 
            {
                float currentTime = Time.time;

                if(currentTime - lastTimeShot >= equippedGun.fireRate)
                {
                    Vector2 direction = (transform.localScale.x > 0) ? Vector2.right : Vector2.left;  // Player facing right or left
                    equippedGun.Shoot(direction);

                    lastTimeShot = currentTime;
                }
            }
        }
    }
}
