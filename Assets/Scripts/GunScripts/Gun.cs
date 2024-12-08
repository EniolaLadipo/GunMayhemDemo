using System.Data;
using UnityEngine;

public class Gun : MonoBehaviour
{

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

    public virtual void Shoot()
    {
        Debug.Log("Shots fired!");
    }
    
}
