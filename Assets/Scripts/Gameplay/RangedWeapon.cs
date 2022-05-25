using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : MonoBehaviour
{
    [Header("Projectile")]
    public GameObject projectilePrefab; // projectile GameObject

    [Header("Fire point")]
    public Transform firePoint; // point where projectile is created

    [Header("Fire mode")]
    public bool isSingleShotFire; // bool for single shot fire mode
    public bool isBurstFire; // bool for burst fire mode
    public bool isAutomaticFire; // bool for automatic fire mode

    [Header("Weapon stats")]
    public float fireForce; // projectile speed
    public bool isShotReady = true; // bool for shot cooldown
    public float fireCooldown; // time between shots for single shot/bursts for burst mode
    public float fireRate; // time between individual projectiles in a burst or automatic fire
    public int burstAmount; // amount of projectiles in a burts

    [Header("Scripts")]
    private PlayerController playerControllerScript;

    public void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    public void Update()
    {
        PlayerShot();
    }

    public void PlayerShot()
    {
        
        // triggers action on a button press/hold
        if (Input.GetKey(playerControllerScript.FireKey))
        {
            if (isSingleShotFire && isShotReady) // single shot
            {
                StartCoroutine(FireSingleShot());
                Debug.Log("shoot 1");
            }
            else if (isBurstFire && isShotReady) // burst fire
            {
                StartCoroutine(FireBurst());
                Debug.Log("shoot 2");
            }
            else if (isAutomaticFire && isShotReady) // automatic fire
            {
                StartCoroutine(FireAutomatic());
                Debug.Log("shoot 3");
            }
        } 
    }

    // fire a single projectile, general method
    public void Fire()
    {
        GameObject bullet = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
    }

    // fire a single projectile, particular
    IEnumerator FireSingleShot()
    {
        isShotReady = false; // disables ability to fire
        Fire();
        yield return new WaitForSeconds(fireCooldown); // cooldown before enabling next shot
        isShotReady = true; // enables ability to fire again
    }

    // burst fire mode, applies both amount of projectiles and wait time
    IEnumerator FireBurst()
    {
        isShotReady = false; // disables ability to fire

        for(int i = 0; i < burstAmount; i++) // fires burstAmount of projectiles 
        {
            Fire();
            yield return new WaitForSeconds(fireRate); // with fireRate delay between them
        }

        yield return new WaitForSeconds(fireCooldown); // then starts cooldown for next burst
        isShotReady = true; // enables ability to fire again
    }

    IEnumerator FireAutomatic()
    {
        isShotReady = false; // disables ability to fire
        Fire();
        yield return new WaitForSeconds(fireRate); // delay between them shots
        isShotReady = true; // enables ability to fire again
    }
}
