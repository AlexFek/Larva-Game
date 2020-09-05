using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Action
{
    Idle, Move, PrepareToJump, Jump, Crouch
}
public enum Direction
{
    Right, Left
}

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerRaycaster))]
public class PlayerActions : MonoBehaviour
{
    [Header("Player Settings")]
    public float moveSpeed = 1f;
    public float jumpUpForce = 2f;
    public float jumpForwardForce = 0.5f;

    [HideInInspector]
    public Action currentAction { get; private set; }

    private PlayerInput input;
    private PlayerRaycaster raycaster;
    private Rigidbody2D rigidbody;
    private Direction currentDirection;
    private Vector2 direction;

    void Start()
    {
        input = GetComponent<PlayerInput>();
        raycaster = GetComponent<PlayerRaycaster>();
        rigidbody = GetComponent<Rigidbody2D>();
        currentDirection = Direction.Right;
        currentAction = Action.Idle;
    }

    void Update()
    {
        Move();
        Jump();
        Crouch();
    }

    private void Move()
    {
        if (raycaster.IsGrounded())
        {
            if ((input.moveRight || input.moveLeft) && (!input.jumpPrepare))
            {
                direction = input.moveRight ? Vector2.right : Vector2.left;
                if (!raycaster.IsTouchRight() && !raycaster.IsTouchLeft())
                {
                    MovePlayer(direction, moveSpeed);
                }
                TurnRound();
                currentAction = Action.Move;
            }
            else if (!input.any)
            {
                currentAction = Action.Idle;
            }
        }
        else
        {
            currentAction = Action.Jump;
        }
    }

    private void Jump()
    {
        if (raycaster.IsGrounded())
        {
            if (input.jumpPrepare)
            {
                currentAction = Action.PrepareToJump;
            }
            else if (input.jump)
            {
                if (input.moveRight)
                {
                    direction = Vector2.up + (Vector2.right * jumpForwardForce);
                }
                else if (input.moveLeft)
                {
                    direction = Vector2.up + (Vector2.left * jumpForwardForce);
                }
                else
                {
                    direction = Vector2.up;
                }

                rigidbody.AddForce(direction * jumpUpForce, ForceMode2D.Impulse);
                currentAction = Action.Jump;
            }
            else if (!input.any)
            {
                currentAction = Action.Idle;
            }
        }
        else
        {
            currentAction = Action.Jump;
        }
    }

    private void Crouch()
    {
        if (raycaster.IsGrounded())
        {
            if (input.crouch && !input.jumpPrepare)
            {
                currentAction = Action.Crouch;
            }
        }
    }

    private void MovePlayer(Vector2 forceDirection, float forceAmount)
    {
        transform.Translate(forceDirection * forceAmount * Time.deltaTime);
    }

    private void TurnRound()
    {
        if (currentDirection == Direction.Right && direction == Vector2.left)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            currentDirection = Direction.Left;
        }
        if (currentDirection == Direction.Left && direction == Vector2.right)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            currentDirection = Direction.Right;
        }
    }
}
