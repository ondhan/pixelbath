using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class Enemy : MonoBehaviour
{
    Vector2 moveDirection;
    public float moveSpeed;
    Transform target;
    Vector2 pointPosition;
    public Rigidbody2D rb;
    private GameManager gameManagerScript;

    [Header("Weapon")]
    public GameObject weapon;
    public bool knife;


    // at the start
    void Start()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        target = GameObject.Find("Player").transform;
    }

    // once per frame
    void Update()
    {
        SeekTarget();
    }

    // once per physics frame
    private void FixedUpdate()
    {
        Move();
        AimAtTarget();

        if (knife)
        {
            weapon.transform.position = new Vector2(2,2);
        }
    }

    // seeks target
    public void SeekTarget()
    {
        if (target)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
            moveDirection = direction;
        }
    }

    // always faces the target
    public void AimAtTarget()
    {
        pointPosition = target.position;

        Vector2 aimDirection = pointPosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
    }

    // applies movement
    public void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    // when hit by projectile, dies
    void OnCollisionEnter2D(Collision2D collision)
    {
		if (collision.gameObject.tag == "Projectile") 
        {
            gameManagerScript.scoreCount += 1;
			Destroy(gameObject);
		}
	}
}
