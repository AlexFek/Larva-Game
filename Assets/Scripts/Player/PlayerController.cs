using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpUpForce;
    public float jumpForwardForce;

    [Space(10)]
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    [Space(10)]
    public Transform frontCheck;
    public float wallSlidingSpeed;
    public float hangingTime;

    private Rigidbody2D rb;

    // Input //
    private float move;
    private bool prepareToJump;
    private bool jump;
    private bool hangOnWall;
    private bool hangOutOfWall;

    [Space(15)]
    // Flags //
    public bool wallSliding = false;
    public bool isTouchingFront = false;
    public bool isGrounded = true;
    private bool isFacingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        move = Input.GetAxisRaw("Horizontal");
        prepareToJump = Input.GetKey(KeyCode.Space);
        jump = Input.GetKeyUp(KeyCode.Space);
        hangOnWall = Input.GetKey(KeyCode.LeftShift);
        hangOutOfWall = Input.GetKeyUp(KeyCode.LeftShift);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, whatIsGround);

        PrepareToJump();
        Jump();
        WallSliding();
        HangOnWall();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void WallJump()
    {

    }

    private void HangOnWall()
    {
        if (hangOnWall && wallSliding)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            Invoke("HangOutOfWall", hangingTime);
        }
        else if (hangOutOfWall)
        {
            HangOutOfWall();
        }
    }

    private void HangOutOfWall()
    {
        Vector2 pushVector;
        pushVector = isFacingRight ? Vector2.left : Vector2.right;

        rb.constraints = RigidbodyConstraints2D.None;
        rb.AddForce(pushVector, ForceMode2D.Impulse);
        Debug.Log(pushVector);
    }

    private void WallSliding()
    {
        if (isTouchingFront && !isGrounded && move != 0)
        {
            wallSliding = true;
        }
        else
        {
            wallSliding = false;
        }

        if (wallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
            rb.constraints = RigidbodyConstraints2D.None;
        }
    }

    private void PrepareToJump()
    {
        if (prepareToJump && isGrounded)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
    }

    private void Jump()
    {
        if (jump && isGrounded)
        {
            Vector2 jumpVector = new Vector2(move * jumpForwardForce, jumpUpForce);
            rb.constraints = RigidbodyConstraints2D.None;
            rb.AddForce(jumpVector, ForceMode2D.Impulse);
        }
    }

    private void Move()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(move * speed, rb.velocity.y);
        }

        if (move > 0 && !isFacingRight)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            isFacingRight = true;
        }
        else if (move < 0 && isFacingRight)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            isFacingRight = false;
        }
    }
}
