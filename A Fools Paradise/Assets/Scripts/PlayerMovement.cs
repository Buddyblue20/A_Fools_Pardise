using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private bool isFacingRight = true;

    private float coyoteTime = 0.2f; // used for jumping afert leaving platform
    private float coyoteTimer;

    private float jumpBufferTime = 0.2f; // used for storing imput prior to hitting the ground
    private float jumpBufferCounter;

    
    public float speed = 8f;
    public float jumpingPower = 16f;
    public Animator animator;
    

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (IsGrounded())
        {

            coyoteTimer = coyoteTime;// check s if player is grounded and set coyote timer to current coyte time
            animator.SetBool("IsJumping", false); //set jumping animation to false and plays idle
        }
        else
        {
            coyoteTimer -= Time.deltaTime;// is player is not grounds add time to coyote timer
        }

        //lets us input a jump prior to ground contact
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime; 
        }
        else
        {
            
            jumpBufferCounter -= Time.deltaTime;
        }

        // allows for coyte jump
        if ( jumpBufferCounter > 0f && coyoteTimer > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            jumpBufferCounter = 0f;   
        }
        
        // A regular jump
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            coyoteTimer = 0f;
        }

        if (Input.GetButton("Jump"))
        {
            animator.SetBool("IsJumping", true);
        }
        Flip();



        animator.SetFloat("Speed", Mathf.Abs(horizontal)); // takes the horizontal velocity and play run animation


    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer); // projects a circle at the player feel to check for ground layer
        
    }

    public bool CanAttackIdle() 
    {
        return horizontal == 0f && IsGrounded();
    }

    public bool CanAttackRun() 
    {
        return horizontal != 0f && IsGrounded();
    }

    private void Flip()
    {
        //fips the player on the x axis when horizontal velocity is 1 or -1
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
