using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    float speed = 5f;
    float jumpStrength = 28f;
    float climbSpeed = 10f;

    bool isAlive = true;

    [SerializeField] GameObject laserPrefab;
    Rigidbody2D rb;
    Animator animator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    float gravityScaleAtStart;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = rb.gravityScale;
    }

    void Update()
    {
        if (!isAlive)
        {
            return;
        }

        PlayerRunning();
        PlayerJumping();
        FlipSprite();
        Climbing();
        PlayerDeath();
        Fire();
    }

    private void Fire()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            Instantiate(laserPrefab, transform.position, Quaternion.identity);
        }
    }

    private void PlayerDeath()
    {
        if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
            isAlive = false;
            animator.SetBool("Dying", true);
            Vector2 deathVelocityToAdd = new Vector2(-28f, jumpStrength);
            rb.velocity += deathVelocityToAdd;
        }
    }

    private void PlayerRunning()
    {
        float moveDirection = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveDirection * speed, rb.velocity.y);
        ChangeAnimToRun();
    }

    private void Climbing()
    {
        if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            animator.SetBool("Climbing", false);
            rb.gravityScale = gravityScaleAtStart;
            return;
        }

        float controlThrow = Input.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(rb.velocity.x, controlThrow * climbSpeed);
        rb.velocity = climbVelocity;
        rb.gravityScale = 0f;

        bool playerHasVerticalSpeed = Mathf.Abs(rb.velocity.y) > Mathf.Epsilon;
        animator.SetBool("Climbing", playerHasVerticalSpeed);

    }
    
    private void PlayerJumping()
    {
        if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Foreground")))
        {
            return;
        }
        
        if (Input.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpStrength);
            rb.velocity += jumpVelocityToAdd;
        }
    }

    private void ChangeAnimToRun()
    {
        bool playerHasHorizontalVelocity = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        animator.SetBool("Running", playerHasHorizontalVelocity);
    }

    private void FlipSprite()
    {
        bool playerHasHorizontalVelocity = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalVelocity)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
        }
    }
}
