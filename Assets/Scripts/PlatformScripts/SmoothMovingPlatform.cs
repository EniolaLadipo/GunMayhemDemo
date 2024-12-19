using UnityEngine;

public class SmoothMovingPlatform : MonoBehaviour
{
    public Transform pointA;   // First point
    public Transform pointB;   // Second point
    public float speed = 2f;   // Speed of the platform
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

        // Move the platform
        transform.position = Vector3.Lerp(pointA.position, pointB.position, t);
    }

}
