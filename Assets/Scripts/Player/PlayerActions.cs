using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerRaycaster))]
public class PlayerActions : MonoBehaviour
{
    [Header("Player Settings")]
    public float moveSpeed = 1f;
    public float crouchSpeed = 0.3f;
    public float jumpUpForce = 2.5f;
    public float jumpForwardForce = 1.25f;

    private Rigidbody2D rigidbody;
    private PlayerInput input;
    private PlayerStateManager state;
    private Vector2 direction;

    private bool hasJumped;

    void Start()
    {
        input = GetComponent<PlayerInput>();
        rigidbody = GetComponent<Rigidbody2D>();
        state = GetComponent<PlayerStateManager>();
    }

    void Update()
    {
        DoActions();
        Debug.Log($"{state.action}, {state.emplacement}, {hasJumped}");
    }

    private void DoActions()
    {
        if (state.emplacement == Emplacement.Ground)
        {
            DoOnGround();
        }
        else
        {
            DoInTheAir();
        }
    }

    private void DoOnGround()
    {
        if (input.moveRight || input.moveLeft)
        {
            if (input.prepareToJump)
            {
                state.action = Action.PrepareToJump;
            }
            else if (input.jump)
            {
                state.action = Action.Lift;
                TossUpPlayer(jumpForwardForce, jumpUpForce);
            }
            else if (input.crouch)
            {
                state.action = Action.Crouch;
                MovePlayer(crouchSpeed, true);
            }
            else
            {
                state.action = Action.Move;
                MovePlayer(moveSpeed, true);
            }
        }
        else if (input.prepareToJump)
        {
            state.action = Action.PrepareToJump;
        }
        else if (input.jump)
        {
            state.action = Action.Lift;
            TossUpPlayer(jumpForwardForce, jumpUpForce);
        }
        else if (!input.any && state.action != Action.Lift)
        {
            state.action = Action.Idle;
            hasJumped = false;
        }
    }

    private void DoInTheAir()
    {
        state.action = hasJumped ? Action.Jump : Action.Fall;
    }

    private void MovePlayer(float speed, bool turnRound)
    {
        direction = input.moveRight ? Vector2.right : Vector2.left;
        transform.Translate(direction * speed * Time.deltaTime);
        if (turnRound)
        {
            TurnRound();
        }
    }

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

    private Vector2 FoldVectors(Vector2 horizontal, Vector2 vertical)
    {
        return (horizontal + vertical);
    }

    private void Reflect()
    {
        ScalePlayer(-1, 1);
    }

    private void ScalePlayer(float coeffX, float coeffY)
    {
        transform.localScale = new Vector2(transform.localScale.x * coeffX, transform.localScale.y * coeffY);
    }
}
