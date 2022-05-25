using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player attributes")]
    public float moveSpeed;
    public Rigidbody2D rb;
    Vector2 moveDirection;
    Vector2 mousePosition;
    public GameObject activeWeapon;
    public GameObject rangedWeapon1;
    public GameObject rangedWeapon2;
    public GameObject rangedWeapon3;

    [Header("Player controls")]
    public KeyCode FireKey = KeyCode.Mouse0;
    public KeyCode SwitchToWeapon1Key = KeyCode.Alpha1;
    public KeyCode SwitchToWeapon2Key = KeyCode.Alpha2;
    public KeyCode SwitchToWeapon3Key = KeyCode.Alpha3;

    [Header("Scripts")]
    private GameManager gameManagerScript;
    public RangedWeapon ranged;

    [Header("Level boundary")]
    public float xRange = 30f;
    public float yRange = 20f;

    private void Start() 
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        activeWeapon = rangedWeapon1;
    }

    // update is called once per frame
    void Update()
    {
        Move();
        Shoot();
        SwitchRangedWeapon();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);

        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
    }

    private void Move()
    {
        // get movement input
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // determine move direction
        moveDirection = new Vector2(moveX, moveY).normalized;

        // prevents player from leaving level bounds
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector2(-xRange, transform.position.y);
        }
        if (transform.position.x > xRange)
        {
            transform.position = new Vector2(xRange, transform.position.y);
        }
        if (transform.position.y < -yRange)
        {
            transform.position = new Vector2(transform.position.x, -yRange);
        }
        if (transform.position.y > yRange)
        {
            transform.position = new Vector2(transform.position.x, yRange);
        }
    }

    private void Shoot()
    {
        // determine mouse position
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKey(FireKey))
        {
            ranged.PlayerShot();
        }
    }

    private void SwitchRangedWeapon()
    {
        if (Input.GetKeyDown(SwitchToWeapon1Key))
        {
            activeWeapon.SetActive(false);
            activeWeapon = rangedWeapon1;
            activeWeapon.SetActive(true);
        }
        if (Input.GetKeyDown(SwitchToWeapon2Key))
        {
            activeWeapon.SetActive(false);
            activeWeapon = rangedWeapon2;
            activeWeapon.SetActive(true);
        }
        if (Input.GetKeyDown(SwitchToWeapon3Key))
        {
            activeWeapon.SetActive(false);
            activeWeapon = rangedWeapon3;
            activeWeapon.SetActive(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "EnemyBlade") 
        {
            Destroy(gameObject);
            gameManagerScript.IsGameOver = true;
            GameManager.IsGameFinished = true;  
		}
    }
}
