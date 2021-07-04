using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{   
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    
    private Animator anim;
    
    private Rigidbody2D body;
    private bool grounded;
    private BoxCollider2D boxCollider;
    // private float wallJumpCooldown;
    private float horizontalInput;

    // Stores current state of player - to get around messy state machine animator layout
    private string currentAnimaton;

    // Animation States
    const string PLAYER_IDLE = "Idle";
    const string PLAYER_RUN = "Run";
    const string PLAYER_JUMP = "Jump";

    private void Awake()    {
        // Get components checks player object for RigidBody2D and Animator
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    //================================================================
    // Update is called once per frame
    //================================================================
    private void Update()   {
        horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
    }
    
    //================================================================
    // Physics based time step loop
    //================================================================
    private void FixedUpdate()  {
        // Flip player when moving left and right
        if(horizontalInput > 0)
            transform.localScale = Vector3.one;
        else if(horizontalInput < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        // If player is on the ground
        if(isGrounded())  {
            // Determine if player is idle or running and apply the corresponding animation
            if(horizontalInput != 0)    {
                ChangeAnimationState(PLAYER_RUN);
            } else {
                ChangeAnimationState(PLAYER_IDLE);
            }
        }
        // Handles jump and animation
        if(Input.GetKeyDown(KeyCode.Space)) Jump();

        
    }

    //=====================================================
    // Mini animation manager
    //=====================================================
    public void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimaton == newAnimation) return;

        anim.Play(newAnimation);
        currentAnimaton = newAnimation;
    }

    private void Jump() {
        if(isGrounded())    {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            // anim.SetTrigger("jump");
            ChangeAnimationState(PLAYER_JUMP);
        } else if(onWall() && !isGrounded())    {
            if(horizontalInput == 0)    {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);

            } else {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
            }
            // wallJumpCooldown = 0;
        }
    }

    private bool isGrounded()   {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()   {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()  {
        return horizontalInput == 0 && isGrounded() && !onWall();
    }
}
