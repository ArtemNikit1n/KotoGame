using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float jumpForce = 0.1f;
    public float swimForce = 5f;
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
        if (Input.GetButton("Horizontal"))
            Run();
        if (isGrounded && Input.GetButton("Jump") && !isInWater)
            Jump();
        if (isInWater && Input.GetButton("Jump"))
            Jump();
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
        if (Input.GetButton("Jump"))
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }
            
        //rb.AddForce(new Vector2(0, swimForce), ForceMode2D.Force);
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
