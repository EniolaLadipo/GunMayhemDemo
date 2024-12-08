using System.Data;
using UnityEngine;

public class Pistol : Gun
{

    void Start()
    {
        gunName = "Pistol";
        magazineCount = 16;
        knockbackForce = 10;
        range = 20;
        bulletSpeed = 30;
        fireRate = 0.1f;
    }

    void Update()
    {

    }

    public override void Shoot()
    {
        base.Shoot();
    }

}
