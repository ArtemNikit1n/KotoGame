using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    [SerializeField] private float jumpForce = 4f;
    private bool isInWater = false;
    private bool isGrounded = false;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = rb.GetComponentInChildren<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        CheckGround();
        CheckWater();
    }

    private void Update()
    {
        if (isInWater == false)
        {
            rb.gravityScale = 1.5f;
            speed = 4f;
        }

        if (Input.GetKey(KeyCode.LeftShift) && !isInWater)
        {
            speed = 6.5f;
        }

        if (Input.GetButton("Horizontal"))
            Run();
        if (isGrounded && Input.GetButton("Jump") && !isInWater)
            Jump();
        if (isInWater && Input.GetButton("Jump"))
            Swim();
    }

    private void Run()
    {
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");

        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);

        sprite.flipX = dir.x < 0.0f;
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);

    }

    private void CheckGround()
    {

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1f);
        isGrounded = false; 


        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Ground"))
            {
                isGrounded = true; 
                break;
            }
        }
    }

    private void Swim()
    {
        rb.gravityScale = 0.5f;
        speed = 3.0f;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void CheckWater()
    {

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1f);
        isInWater = false;


        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Water"))
            {
                isInWater = true; 
                break; 
            }
        }
    }
}
