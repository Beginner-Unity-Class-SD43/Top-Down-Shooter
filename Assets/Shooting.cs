using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint; // Where the bullet will fire from

    public GameObject bulletPrefab; // Bullet prefab gameobject

    public float bulletForce = 20f; // The bullet's speed

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) { // Get our left mouse button click
            Shoot(); // Shoot the bullet
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); // Creates a bullet
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>(); // Gets the bullet's rigidbody
        rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse); // Adds force to the bullet
    }
}
