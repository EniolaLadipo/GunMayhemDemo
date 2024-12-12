using System.Collections;
using UnityEngine;

public class AmmoPickUp : PowerUp {

    void Start()
    {

    }

    void Update()
    {

    }
    IEnumerator Spawn()
    {
        // suspend execution for 5 seconds
        yield return new WaitForSeconds(5);
        Debug.Log("Creating ammo box...");
    }

}