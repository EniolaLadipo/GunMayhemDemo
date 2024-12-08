using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    public int speed;

    void Start()
    {
        
    }

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

    }
    
    void FixedUpdate()
    {
        body.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.linearVelocity.y);

        if(Input.GetKey(KeyCode.Space)){
            body.linearVelocity = new Vector2(body.linearVelocity.x, speed);
        }
    }
}
