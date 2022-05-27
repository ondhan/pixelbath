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

    [Header("Player controls")]
    public KeyCode FireKey = KeyCode.Mouse0;

    [Header("Scripts")]
    private GameManager gameManagerScript;
    public Weapon weapon;

    [Header("Level boundary")]
    public float xRange = 30f;
    public float yRange = 20f;

    private void Start() 
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // update is called once per frame
    void Update()
    {
        Move();
        Shoot();
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
            weapon.PlayerShot();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "EnemyBlade") 
        {
            Destroy(gameObject);
            DataManager.IsGameOver = true;
            DataManager.IsGameFinished = true;  
		}
    }
}
