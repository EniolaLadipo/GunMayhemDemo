using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform muzzlePoint;
    public int magazineCount;
    public int knockbackForce;
    public int range;
    public int bulletSpeed;
    public string gunName;
    public float fireRate;
    
    void Start()
    {
        
    }

    void Update()
    {

    }

    public virtual void Shoot(Vector2 direction)
    {

    }
    
}
