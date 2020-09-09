using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpUpForce;
    public float jumpForwardForce;
    public float wallJumpForwardForce;
    public float wallJumpUpForce;

    [Space(10)]
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    [Space(10)]
    public Transform frontCheck;
    public float wallSlidingSpeed;

    private Rigidbody2D rb;

    [HideInInspector]
    public Actions action;

    // Input //
    private bool noInput;
    private float move;
    private bool jump;

    [Space(15)]
    // Flags //
    public bool wallSliding = false;
    public bool isTouchingFront = false;
    public bool isGrounded = true;
    private bool isFacingRight = true;
    public bool isJumping = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        action = Actions.Idle;
    }

    void Update()
    {
        ReadInput();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, whatIsGround);

        Idle();
        Jump();
        WallSliding();
        WallJump();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void ReadInput()
    {
        noInput = !Input.anyKey;
        move = Input.GetAxisRaw("Horizontal");
        jump = Input.GetKeyUp(KeyCode.Space);
    }

    private void Idle()
    {
        if (isGrounded && noInput)
        {
            action = Actions.Idle;
        }
    }

    private void WallJump()
    {
        if (wallSliding && jump)
        {
            action = Actions.Jump;
            int direction = isFacingRight ? -1 : 1;
            Vector2 jumpVector = new Vector2(direction * wallJumpForwardForce, wallJumpUpForce);
            rb.AddForce(jumpVector, ForceMode2D.Impulse);
            Flip();
        }
    }

    private void WallSliding()
    {
        wallSliding = isTouchingFront && !isGrounded && (move != 0);

        if (wallSliding)
        {
            action = Actions.WallSliding;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, 0));
        }
    }

    private void Jump()
    {
        if (jump && isGrounded)
        {
            action = Actions.Jump;
            Vector2 jumpVector = new Vector2(move * jumpForwardForce, jumpUpForce);
            rb.AddForce(jumpVector, ForceMode2D.Impulse);
        }
    }

    private void Move()
    {
        if (isGrounded)
        {
            if (move != 0) action = Actions.Move;
            rb.velocity = new Vector2(move * speed, rb.velocity.y);

            if ((move > 0 && !isFacingRight) || (move < 0 && isFacingRight))
            {
                Flip();
            }
        }
    }

    private void Flip()
    {
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        isFacingRight = !isFacingRight;
    }
}

public enum Actions
{
    Idle, Move, Jump, WallSliding, Fall
}
