using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    public int jumpForce;
    public int speed;
    public float gravityForce;
    private bool isGrounded;
    public Transform groundCheck; // Empty GameObject for checking the ground
    public float groundCheckRadius = 0.15f; // Size of the ground check
    public LayerMask groundLayer; // Layer representing ground objects
    public Transform gunHolder;

    public float defaultGravity;
    private float horizontalInput;
    private bool isFacingRight = true; // Default to facing right

    private PlayerInventory inventory; // Reference to the PlayerInventory script

    private float recoilForce = 2f; // Amount of recoil applied when shooting

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        body.freezeRotation = true;
        defaultGravity = body.gravityScale;

        inventory = GetComponent<PlayerInventory>(); // Link to the inventory script
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        // Check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                Jump(); // Normal jump
            }
            else if (inventory.canDoubleJump) // Double jump if power-up is active
            {
                Jump();
                inventory.canDoubleJump = false; // Disable double jump after use
            }
        }

        if (horizontalInput > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (horizontalInput < 0 && isFacingRight)
        {
            Flip();
        }
    }

    void FixedUpdate()
    {
        Move(horizontalInput);
        ApplyGravity();
    }

    void Move(float moveInput)
    {
        float movementSpeed = inventory != null ? inventory.speed : speed;
        body.velocity = new Vector2(moveInput * movementSpeed, body.velocity.y);
    }

    void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, inventory != null ? inventory.jumpForce : jumpForce);
    }

    void ApplyGravity()
    {
        if (body.velocity.y > 0)
        {
            body.gravityScale = defaultGravity * gravityForce;
        }
        else
        {
            body.gravityScale = defaultGravity;
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;

        Vector3 gunScale = gunHolder.localScale;
        gunScale.x *= -1;
        gunHolder.localScale = gunScale;
    }

    // Apply recoil effect to the player movement
    public void ApplyRecoil(Vector2 recoilDirection)
    {
        // Apply recoil force to the player's movement to push them back
        body.velocity = new Vector2(recoilDirection.x * recoilForce, body.velocity.y);
    }
}
