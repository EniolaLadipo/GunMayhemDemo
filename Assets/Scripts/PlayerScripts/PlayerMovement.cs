using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    public int jumpForce;
    public float speed;
    public float gravityForce;
    private bool isGrounded;
    public Transform groundCheck; // Empty GameObject for checking the ground
    public float groundCheckRadius = 0.15f; // Size of the ground check
    public LayerMask groundLayer; // Layer representing ground objects
    public Transform gunHolder;

    public float defaultGravity;
    private float horizontalInput;
    private bool isFacingRight = false;
    public bool canDoubleJump = false;
    public int jumpCount = 0;

    void Start()
    {
        
    }

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        body.freezeRotation = true;
        defaultGravity = body.gravityScale;
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded)
        {
            jumpCount = 0;  // Reset the jump count when the player is grounded
        }

        // Handle jump input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                // Perform the first (normal) jump
                Jump();
                jumpCount = 1;  // Mark that the player has jumped once
            }
            else if (canDoubleJump && jumpCount == 1)
            {
                // Perform the second (double) jump if the player is in the air and double jump is available
                Jump();
                jumpCount = 2;  // Mark that the player has performed both jumps
            }
        }

        // Flip the player and gun depending on movement direction
        if (horizontalInput > 0 && isFacingRight)
        {
            Flip();
        }
        else if (horizontalInput < 0 && !isFacingRight)
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
        body.linearVelocity = new Vector2(moveInput * speed, body.linearVelocity.y);
    }

    void Jump()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
    }

    void ApplyGravity()
    {
        if(body.linearVelocity.y > 0)
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
        // Flip the player by changing its scale
        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;  // Invert the X scale to flip the player
        transform.localScale = playerScale;

        // Flip the gunholder along with the player (since the gunholder is a child of the player)
        Vector3 gunScale = gunHolder.localScale;
        gunScale.x *= -1;  // Invert the X scale to flip the gunholder
        gunHolder.localScale = gunScale;

    }

    private void OnDrawGizmos()
    {
        // Visualize the ground check in the editor
        Gizmos.color = Color.red;
        if (groundCheck != null)
        {
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }

    public void EnableDoubleJump(bool isEnabled)
    {
        canDoubleJump = isEnabled;
    }
}
