using UnityEngine;

public class PlayerController : MonoBehaviour {
    [Header("Player Stats")]
    [Space(10)]
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
    
    [Header("Player State")]
    [Space(10)]
    public Actions action;

    public bool isWallSliding;
    public bool isTouchingFront;
    public bool isGrounded;
    public bool isFacingRight;
    public bool isJumping;

    public Vector2 velocity;

    private Rigidbody2D rb;

    private bool noInput;
    private float moveInput;
    private bool jumpInput;

    void Start() {
        InitComponentsInstances();
        SetInitState();
    }

    void Update() {
        UpdateVariables();
        ListenForInput();
        DoOnUpdateIfInput();
    }

    private void FixedUpdate() {
        DoIfMove();
    }

    private void DoOnFixedUpdateIfInput() {
        DoIfMove();
    }

    private void DoOnUpdateIfInput() {
        DoIfWallSliding();
        DoIfWallJump();
        DoIfNoInput();
        DoIfJump();
    }

    private void InitComponentsInstances() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void SetInitState() {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        action = Actions.Idle;
    }

    private void UpdateVariables() {
        UpdateTouchState();
        UpdateVelocity();
    }

    private void UpdateTouchState() {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, whatIsGround);
    }

    private void UpdateVelocity() {
        velocity.x = rb.velocity.x;
        velocity.y = rb.velocity.y;
    }

    private void ListenForInput() {
        noInput = !UnityEngine.Input.anyKey;
        moveInput = UnityEngine.Input.GetAxisRaw("Horizontal");
        jumpInput = UnityEngine.Input.GetKeyUp(KeyCode.Space);
    }

    private void SetActionStateIdle() {
        action = SetActionState(Actions.Idle);
    }

    private void SetActionStateJump() {
        action = SetActionState(Actions.Jump);
    }

    private Actions SetActionState(Actions actionState) {
        return actionState;
    }

    private void DoIfNoInput() {
        if (isGrounded && velocity.x == 0)       SetActionStateIdle();
        else if (!isGrounded && velocity.y > 1f) SetActionStateJump();
    }

    private void DoIfWallJump() {
        if (isWallSliding && jumpInput) {
            action = Actions.Jump;
            int direction = isFacingRight ? -1 : 1;
            Vector2 jumpVector = new Vector2(direction * wallJumpForwardForce, wallJumpUpForce);
            rb.AddForce(jumpVector, ForceMode2D.Impulse);
            Flip();
        }
    }

    private void DoIfWallSliding() {
        isWallSliding = isTouchingFront && !isGrounded && (moveInput != 0);

        if (isWallSliding) {
            action = Actions.WallSlide;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, 0));
        }
    }

    private void DoIfJump() {
        if (jumpInput && isGrounded) {
            action = Actions.Jump;
            Vector2 jumpVector = new Vector2(moveInput * jumpForwardForce, jumpUpForce);
            rb.AddForce(jumpVector, ForceMode2D.Impulse);
        }
    }

    private void DoIfMove() {
        if (isGrounded) {
            if (moveInput != 0)
                action = Actions.Move;
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

            if ((moveInput > 0 && !isFacingRight) || (moveInput < 0 && isFacingRight)) {
                Flip();
            }
        }
    }

    private void Flip() {
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        isFacingRight = !isFacingRight;
    }
}

public enum Actions {
    Idle, Move, Jump, WallSlide, Fall
}
