using UnityEngine;

public class SmoothMovingPlatform : MonoBehaviour
{
    public Transform pointA;   // First point
    public Transform pointB;   // Second point
    public float speed = 0.5f;   // Speed of the platform
    private bool isMoving = true;

    void Update()
    {
        MovePlatform();
    }

    void MovePlatform()
    {
        if (!isMoving) return;

        // Smoothly interpolate between pointA and pointB using PingPong
        float t = Mathf.PingPong(Time.time * speed, 1f); // Time factor for interpolation

        // Get the current position of the platform and preserve its Z value
        Vector3 currentPosition = transform.position;

        // Move the platform, but keep the Z value the same
        transform.position = Vector3.Lerp(pointA.position, pointB.position, t);
        
        // Ensure the Z value stays the same as it was before
        transform.position = new Vector3(transform.position.x, transform.position.y, currentPosition.z);
    }
}
