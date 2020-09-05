using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerRaycaster))]
public class PlayerActions : MonoBehaviour
{
    [Header("Player Settings")]
    public float moveSpeed = 1f;
    public float jumpUpForce = 2f;
    public float jumpForwardForce = 0.5f;

    private Rigidbody2D rigidbody;
    private PlayerInput input;
    private PlayerStateManager state;
    private Vector2 direction;

    void Start()
    {
        input = GetComponent<PlayerInput>();
        rigidbody = GetComponent<Rigidbody2D>();
        state = GetComponent<PlayerStateManager>();
    }

    void Update()
    {
        CheckForInput();
    }

    private void CheckForInput()
    {
        Move();
        Jump();
        Crouch();
        Fall();
    }

    private void Move()
    {
        if (state.emplacement == Emplacement.Ground)
        {
            if ((input.moveRight || input.moveLeft) && (!input.jumpPrepare))
            {
                direction = input.moveRight ? Vector2.right : Vector2.left;
                MovePlayer(direction, moveSpeed);
                TurnRound();
                state.action = Action.Move;
            }
            else if (!input.any)
            {
                state.action = Action.Idle;
            }
        }
        else
        {
            state.action = Action.Jump;
        }
    }

    private void Jump()
    {
        if (state.emplacement == Emplacement.Ground && input.any)
        {
            if (input.jumpPrepare)
            {
                state.action = Action.PrepareToJump;
            }
            else if (input.jump)
            {
                SetJumpDirection();
                rigidbody.AddForce(direction * jumpUpForce, ForceMode2D.Impulse);
                state.action = Action.Jump;
            }
            else
            {
                if (!input.any)
                {
                    state.action = Action.Idle;
                }
            }
        }
        else
        {
            state.action = Action.Jump;
        }
    }

    private void Crouch()
    {
        if (state.emplacement == Emplacement.Ground)
        {
            if (input.crouch && !input.jumpPrepare)
            {
                state.action = Action.Crouch;
            }
            else
            {
                state.action = Action.Idle;
            }
        }
    }

    private void Fall()
    {
        if (state.emplacement == Emplacement.Air)
        {
            state.action = (state.action != Action.Jump) ? Action.Fall : Action.Jump;
        }
        else if (!input.any)
        {
            state.action = Action.Idle;
        }
    }

    private void MovePlayer(Vector2 forceDirection, float forceAmount)
    {
        transform.Translate(forceDirection * forceAmount * Time.deltaTime);
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

    private void SetJumpDirection()
    {
        if (input.moveRight)
        {
            SetDirection(Vector2.right * jumpForwardForce, Vector2.up);
        }
        else if (input.moveLeft)
        {
            SetDirection(Vector2.left * jumpForwardForce, Vector2.up);
        }
        else
        {
            SetDirection(Vector2.zero, Vector2.zero);
        }
    }

    private void SetDirection(Vector2 horizontal, Vector2 vertical)
    {
        direction = horizontal + vertical;
    }
}
