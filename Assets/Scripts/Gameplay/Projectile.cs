using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Level boundary")]
    public float xRange = 30f;
    public float yRange = 20f;

    // if projectile hits anything but level boundary or other projectile, it gets destroyed
    private void OnCollisionEnter2D(Collision2D col) 
    {
        if (col.gameObject.tag == "Boundary" || col.gameObject.tag == "Projectile") 
        {
        
		}
        else
        {
            Destroy(gameObject);
        }
    }

    // if projectile leaves level boundaries, it gets destroyed
    private void Update() 
    {
        if (transform.position.x < -xRange)
        {
            Destroy(gameObject);
        }
        if (transform.position.x > xRange)
        {
            Destroy(gameObject);
        }
        if (transform.position.y < -yRange)
        {
            Destroy(gameObject);
        }
        if (transform.position.y > yRange)
        {
            Destroy(gameObject);
        }   
    }
}
