using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;

    Vector2 moveDirection;
    Vector2 mousePosition;

    private GameManager gameManagerScript;

    [Header("Level boundary")]
    public float xRange = 30f;
    public float yRange = 20f;

    private void Start() 
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();    
    }
    // Update is called once per frame
    void Update()
    {
        // get movement input
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // determine move direction
        moveDirection = new Vector2(moveX, moveY).normalized;

        // determine mouse position
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (transform.position.x < -xRange)
        {
            
        }
        if (transform.position.x > xRange)
        {
            
        }
        if (transform.position.y < -yRange)
        {
            
        }
        if (transform.position.y > yRange)
        {
            
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);

        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy") 
        {
            Destroy(gameObject);
            gameManagerScript.IsGameOver = true;
            GameManager.IsGameFinished = true;  
		}
    }
}
