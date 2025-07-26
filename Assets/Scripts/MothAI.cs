using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothAI : MonoBehaviour
{
    public float minSpeed = 0.5f;
    public float maxSpeed = 2f;
    public float idleChance = 0.3f; 

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private float moveTimer;
    private float waitTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ChooseNextState();
    }

    void FixedUpdate()
    {
        if (waitTimer > 0)
        {
            waitTimer -= Time.fixedDeltaTime;
            rb.velocity = Vector2.zero;
        }
        else
        {
            rb.velocity = moveDirection;
            moveTimer -= Time.fixedDeltaTime;
            if (moveTimer <= 0)
            {
                ChooseNextState();
            }
        }
        if (rb.velocity != Vector2.zero)
        {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = angle;
        }

    }

    void ChooseNextState()
    {
        if (Random.value < idleChance)
        {
            waitTimer = Random.Range(0.5f, 2f);
        }
        else
        {
            Vector2 dir = Random.insideUnitCircle.normalized;
            float speed = Random.Range(minSpeed, maxSpeed);
            moveDirection = dir * speed;
            moveTimer = Random.Range(1f, 3f);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        moveDirection = -moveDirection;
    }
}
