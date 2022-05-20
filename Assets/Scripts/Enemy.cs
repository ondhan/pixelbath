using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Vector2 moveDirection;
    public float moveSpeed;
    Transform target;
    public Rigidbody2D rb;

    void Start()
    {
        target = GameObject.Find("Player").transform;
    }

    void Update()
    {
        if (target)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
            moveDirection = direction;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
		if (collision.gameObject.tag == "Bullet") 
        {
			Destroy(gameObject);
		}
	}
}
