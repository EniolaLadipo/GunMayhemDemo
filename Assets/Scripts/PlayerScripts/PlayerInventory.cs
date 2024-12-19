using UnityEngine;
using System.Collections;

public class PlayerInventory : MonoBehaviour
{
    public int gunCount = 0;
    public Transform gunHolder;         // Reference to the GunHolder object
    private GameObject currentGun;      // The currently equipped gun

    public float lastTimeShot = 0f;

    // Player-related fields
    public float speed = 3f;            // Default speed
    public float jumpForce = 6f;        // Default jump force
    public bool canDoubleJump = false; // Double jump enabled
    public int doubleJumpCount = 0;     // Counter for double jumps
    public bool isJetpackActive = false; // Jetpack state
    public float jetpackForce = 5f;    // Reduced force for jetpack (default adjusted)
    public float jetpackSpeed = 2f;    // Speed at which the jetpack is activated

    private Rigidbody2D rb;            // Reference to Rigidbody2D

    // Power-up related fields
    private float originalGravityScale; // To store the original gravity scale for restoring later
    private Coroutine currentPowerUpCoroutine;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

        // Jetpack logic
        if (isJetpackActive && Input.GetKey(KeyCode.Space))
        {
            ActivateJetpack();
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
                    Vector2 direction = (transform.localScale.x > 0) ? Vector2.right : Vector2.left;
                    equippedGun.Shoot(direction);

                    lastTimeShot = currentTime;
                    Recoil();
                }
            }
        }
    }

    public void Recoil()
    {
        Vector2 recoilDirection = transform.localScale.x > 0 ? Vector2.left : Vector2.right;
        rb.AddForce(recoilDirection * 2f, ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("powerup"))
        {
            PowerUpBehavior powerUp = other.GetComponent<PowerUpBehavior>();

            if (powerUp != null)
            {
                // If there is already an active power-up, stop the current coroutine (i.e., cancel the current power-up)
                if (currentPowerUpCoroutine != null)
                {
                    StopCoroutine(currentPowerUpCoroutine);
                }

                switch (powerUp.powerUpType)
                {
                    case PowerUpBehavior.PowerUpType.Speed:
                        currentPowerUpCoroutine = StartCoroutine(SpeedBoost());
                        break;

                    case PowerUpBehavior.PowerUpType.DoubleJump:
                        currentPowerUpCoroutine = StartCoroutine(DoubleJumpBoost());
                        break;

                    case PowerUpBehavior.PowerUpType.Jetpack:
                        currentPowerUpCoroutine = StartCoroutine(JetpackBoost());
                        break;
                }

                // Destroy the power-up object and pick it up
                Destroy(other.gameObject);
            }
        }
    }

    private IEnumerator SpeedBoost()
    {
        float originalSpeed = speed;
        speed = 15f;
        yield return new WaitForSeconds(6f);
        speed = originalSpeed;
    }

    private IEnumerator DoubleJumpBoost()
    {
        canDoubleJump = true;
        yield return new WaitForSeconds(8f);
        canDoubleJump = false;
    }

    private IEnumerator JetpackBoost()
    {
        isJetpackActive = true;
        rb.gravityScale = originalGravityScale * 0.5f;

        // After 15 seconds, the jetpack effect should expire
        yield return new WaitForSeconds(6f);

        // Deactivate the jetpack and reset gravity
        isJetpackActive = false;
        rb.gravityScale = originalGravityScale;
    }

    public void ActivateJetpack()
    {
        if (isJetpackActive)
        {
            rb.AddForce(Vector2.up * jetpackForce * jetpackSpeed, ForceMode2D.Force); // Apply jetpack force
        }
    }
}
