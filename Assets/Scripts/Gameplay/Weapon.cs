using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Projectile")]
    public GameObject bulletPrefab;

    [Header("Fire point")]
    public Transform firePoint;

    [Header("Fire force")]
    public float fireForce = 20f;

    [Header("Fire mode")]
    public bool singleShotFire;
    public bool burstFire;
    public int burstAmount = 1;
    public bool automaticFire;

    public void Update()
    {
        if (singleShotFire)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Fire();
            }
        }
        else if (burstFire)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Fire();
            }
        }
        else if (automaticFire)
        {
            if (Input.GetMouseButton(0))
            {
                Fire();
            }
        }
        
    }

    public void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
    }
}
