using UnityEngine;

public class Test : MonoBehaviour
{

    private Rigidbody2D rb;
    public float bulletSpeed = 5f;  // Speed of the bullet's movement
    private Vector2 moveDirection;  // The direction in which the bullet will move

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveDirection = new Vector2(1, 0);  // Moving to the right
        gameObject.SetActive(true);
        Debug.Log("Bullet object was created and should start moving");

    }   

    void Update()
    {
        Debug.Log("Bullet is moving");
        MoveBullet();
        if (Time.time > 3f)
        {
            gameObject.SetActive(false);  // Disable the object
        }
    }

    void MoveBullet()
    {
        // Calculate new position by moving in the specified direction
        Vector2 newPosition = (Vector2)transform.position + moveDirection * bulletSpeed * Time.deltaTime;

        // Move the bullet to the new position
        transform.position = newPosition;
    }
}