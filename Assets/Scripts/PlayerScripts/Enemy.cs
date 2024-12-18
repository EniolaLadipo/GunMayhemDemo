using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float knockbackResistance = 0.1f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    public void ApplyKnockback(Vector2 knockbackDirection, float knockbackForce)
    {
       if(rb != null)
       {
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
       }
    }
}
