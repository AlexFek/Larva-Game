using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerColliderPhysics))]
public class PlayerActions : MonoBehaviour
{
    [Header("Player Settings")]
    public float moveSpeed = 0.5f;
    [Space(10)]
    public float jumpForwardForce = 1.25f;
    public float jumpUpForce = 2.5f;
    [Space(10)]
    public float wallJumpForwardForce = 3f;
    public float wallJumpUpForce = 0.7f;
    [Space(10)]
    public float wallHangTime = 5f;

    private Rigidbody2D rigidbody;
    private PlayerInput input;
    private PlayerStateManager state;
    private PlayerColliderPhysics collider;
    private Vector2 direction;

    private bool hasJumped = false;
    private float hangingTime = 0f;
    private float extraTime = 1f;
    private float wallPushForce = 0.002f;

    void Start()
    {
        input = GetComponent<PlayerInput>();
        rigidbody = GetComponent<Rigidbody2D>();
        state = GetComponent<PlayerStateManager>();
        collider = GetComponent<PlayerColliderPhysics>();
    }

    private void Update()
    {
        // Debug.Log($"{state.emplacement}, {state.action}");
    }

    ///==============///
    /// MAIN METHODS ///
    ///==============///

    public void DoOnUpdate()
    {
        switch (state.emplacement)
        {
            case Emplacement.Ground:
            {
                if (state.action != Action.Lift && state.action != Action.Crawl)
                {
                    JumpOnSpace();
                }
                break;
            }
            case Emplacement.Wall:
            {
                WallJumpOnSpace();
                HangOutOfWall();
                hangingTime += Time.deltaTime;
                break;
            }
            case Emplacement.Air:
            {
                state.action = hasJumped ? Action.Jump : Action.Fall;
                HangOnWall();
                break;
            }
        }
    }

    public void DoOnFixedUpdate()
    {
        if (state.emplacement == Emplacement.Ground)
        {
            if (!input.any && state.action != Action.Lift)
            {
                BeIdle();
            }
            else
            {
                if ((input.moveRight || input.moveLeft) && state.action != Action.PrepareToJump)
                {
                    MoveOnAD();
                    // FitSizeToGap();
                }
            }
        }
    }

    ///================///
    /// ACTION METHODS ///
    ///================///

    private void BeIdle()
    {
        state.action = Action.Idle;
        hasJumped = false;
    }
    private void MoveOnAD()
    {
        state.action = Action.Move;
        MovePlayer(moveSpeed, true);
    }
    private void JumpOnSpace()
    {
        if (input.prepareToJump)
        {
            state.action = Action.PrepareToJump;
        }
        else if (input.lift)
        {
            state.action = Action.Lift;
            TossUpPlayer(jumpForwardForce, jumpUpForce);
        }
    }
    private void WallJumpOnSpace()
    {
        if (input.lift)
        {
            JumpOutOfWall(Vector2.down, wallJumpForwardForce, wallJumpUpForce);
            state.action = Action.Lift;
        }
    }

    ///==============///
    /// MOVE METHODS ///
    ///==============///

    private void MovePlayer(float speed, bool turnRound)
    {
        direction = input.moveRight ? Vector2.right : Vector2.left;
        transform.Translate(direction * speed * Time.fixedDeltaTime);
        if (turnRound)
        {
            TurnRound();
        }
    }
    private void TurnRound()
    {
        if (state.direction == Direction.Right && direction == Vector2.left)
        {
            Reflect();
            state.direction = Direction.Left;
        }
        if (state.direction == Direction.Left && direction == Vector2.right)
        {
            Reflect();
            state.direction = Direction.Right;
        }
    }
    private void Reflect()
    {
        ScalePlayer(-1, 1);
    }
    private void ScalePlayer(float coeffX, float coeffY)
    {
        transform.localScale = new Vector2(transform.localScale.x * coeffX, transform.localScale.y * coeffY);
    }
    private void FitSizeToGap()
    {
        if ((state.isTouchWallRight || state.isTouchWallLeft))
        {
            state.action = Action.Crawl;
            collider.SqueezePlayer();
        }
        if (!collider.IsTouchUp() && state.action == Action.Crawl)
        {
            state.action = Action.Move;
            collider.ResetColliderShape();
        }
    }

    ///==============///
    /// JUMP METHODS ///
    ///==============///

    private void TossUpPlayer(float forwardForce, float upForce)
    {
        direction = GetJumpDirection();
        Vector2 jumpVector = new Vector2(direction.x * forwardForce, direction.y * upForce);
        rigidbody.AddForce(jumpVector, ForceMode2D.Impulse);
        hasJumped = true;
    }
    private Vector2 GetJumpDirection()
    {
        if (input.moveRight)
        {
            return FoldVectors(Vector2.right, Vector2.up);
        }
        else if (input.moveLeft)
        {
            return FoldVectors(Vector2.left, Vector2.up);
        }
        else
        {
            return FoldVectors(Vector2.zero, Vector2.up);
        }
    }
    private Vector2 FoldVectors(Vector2 horizontal, Vector2 vertical)
    {
        return (horizontal + vertical);
    }
    private void HangOnWall()
    {
        if ((state.isTouchWallRight || state.isTouchWallLeft))
        {
            state.action = Action.HangOnWall;
            rigidbody.constraints = RigidbodyConstraints2D.FreezePositionY;
        }
    }
    private void HangOutOfWall()
    {
        if (hangingTime > wallHangTime)
        {
            JumpOutOfWall(Vector2.zero, wallPushForce, 0);
            state.action = Action.Fall;
        }
    }
    private void JumpOutOfWall(Vector2 pushVector, float horizontalForce, float verticalForce)
    {
        direction = state.isTouchWallRight ? Vector2.left : Vector2.right;
        TurnRound();

        pushVector += state.isTouchWallRight ? Vector2.left : Vector2.right;
        pushVector.x *= horizontalForce;
        pushVector.y *= verticalForce;

        rigidbody.constraints = RigidbodyConstraints2D.None;
        rigidbody.AddForce(pushVector, ForceMode2D.Impulse);

        hangingTime = 0f;
    }
}
