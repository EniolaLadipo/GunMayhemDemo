using UnityEngine;
using System.Collections;

public class PlayerInventory : MonoBehaviour
{
    public int gunCount = 0;
    public Transform gunHolder; // Reference to the GunHolder object
    private GameObject currentGun; // The currently equipped gun

    public float lastTimeShot = 0f;

    // Player-related fields
    public float speed = 3f;          // Default speed (slower)
    public float jumpForce = 5f;      // Default jump force (reduced for normal jumps)
    public bool canDoubleJump = false; // Double jump enabled
    private int doubleJumpCount = 0; // Counter for double jumps
    public bool isJetpackActive = false; // Jetpack state
    public float jetpackForce = 10f;  // Force applied during jetpack use

    private Rigidbody2D rb; // Reference to Rigidbody2D

    // Power-up related fields
    private float originalGravityScale; // To store the original gravity scale for restoring later
    private Coroutine currentPowerUpCoroutine;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D for movement
        originalGravityScale = rb.gravityScale; // Save the original gravity scale
    }

    void Update()
    {
        FlipGun();

        // Shooting logic
        if (Input.GetKey(KeyCode.E))
        {
            FireWeapon();
        }

        // Jetpack logic: If jetpack is active and spacebar is held down, keep applying upward force
        if (isJetpackActive && Input.GetKey(KeyCode.Space))
        {
            ActivateJetpack();
        }

        // Double jump logic
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded())
            {
                doubleJumpCount = 0; // Reset double jump when touching the ground
            }
            else if (canDoubleJump && doubleJumpCount < 1) // Allow one extra double jump
            {
                doubleJumpCount++;
                Jump(jumpForce); // Perform a double jump
            }
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
        if (currentGun != null)
        {
            Gun equippedGun = currentGun.GetComponent<Gun>();

            if (equippedGun != null)
            {
                float currentTime = Time.time;

                if (currentTime - lastTimeShot >= equippedGun.fireRate)
                {
                    Vector2 direction = (transform.localScale.x > 0) ? Vector2.right : Vector2.left; // Player facing right or left
                    equippedGun.Shoot(direction);

                    lastTimeShot = currentTime;

                    // Trigger recoil after firing
                    Recoil();
                }
            }
        }
    }

    // Recoil effect when the player shoots the gun
    public void Recoil()
    {
        // Apply recoil effect to the player when shooting
        Vector2 recoilDirection = transform.localScale.x > 0 ? Vector2.left : Vector2.right;
        rb.AddForce(recoilDirection * 2f, ForceMode2D.Impulse); // Recoil force is small
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player collides with a power-up or rifle
        if (other.CompareTag("powerup"))
        {
            PowerUpBehavior powerUp = other.GetComponent<PowerUpBehavior>();

            if (powerUp != null)
            {
                // If there's already an active power-up, stop it first
                if (currentPowerUpCoroutine != null)
                {
                    StopCoroutine(currentPowerUpCoroutine);
                }

                // Apply the new power-up
                switch (powerUp.powerUpType)
                {
                    case PowerUpBehavior.PowerUpType.Speed:
                        currentPowerUpCoroutine = StartCoroutine(SpeedBoost());
                        break;

                    case PowerUpBehavior.PowerUpType.DoubleJump:
                        currentPowerUpCoroutine = StartCoroutine(DoubleJumpBoost());
                        break;

                    case PowerUpBehavior.PowerUpType.Jetpack:
                        currentPowerUpCoroutine = StartCoroutine(JetpackBoost()); // Activate jetpack power-up
                        break;
                }

                Destroy(other.gameObject); // Remove the power-up from the scene
            }
        }
        // Check if the player collides with a rifle and picks it up
        if (other.CompareTag("Gun"))
        {
            // Equip the rifle and destroy the rifle object
            equipGun(other.gameObject); // Equip the rifle
            Destroy(other.gameObject);  // Remove the rifle from the scene
        }
    }

    private IEnumerator SpeedBoost()
    {
        float originalSpeed = speed;
        speed = 6f; // Increase speed with the power-up
        yield return new WaitForSeconds(15f); // Lasts for 15 seconds
        speed = originalSpeed; // Reset to original speed
    }

    private IEnumerator DoubleJumpBoost()
    {
        canDoubleJump = true; // Enable double jump
        yield return new WaitForSeconds(15f); // Lasts for 15 seconds
        canDoubleJump = false; // Disable double jump
    }

    private IEnumerator JetpackBoost()
    {
        isJetpackActive = true; // Enable jetpack
        rb.gravityScale = originalGravityScale * 0.5f; // Reduce gravity to make jetpack more effective
        yield return new WaitForSeconds(15f); // Lasts for 15 seconds
        isJetpackActive = false; // Disable jetpack
        rb.gravityScale = originalGravityScale; // Reset gravity back to original
    }

    // Function to activate the jetpack while pressing space
    public void ActivateJetpack()
    {
        if (isJetpackActive)
        {
            // Apply an upward force while the space key is held down
            rb.AddForce(Vector2.up * jetpackForce, ForceMode2D.Force);
        }
    }

    // Helper function to check if the player is grounded
    private bool isGrounded()
    {
        // Check if the player is on the ground (adjust this logic based on your game)
        return Physics2D.OverlapCircle(transform.position, 0.1f, LayerMask.GetMask("Ground")) != null;
    }

    // Function to handle normal jumping
    private void Jump(float force)
    {
        rb.velocity = new Vector2(rb.velocity.x, 0); // Reset vertical velocity before jumping
        rb.AddForce(Vector2.up * force, ForceMode2D.Impulse); // Apply the jump force
    }
}
