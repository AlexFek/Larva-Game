using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour {
    public Actions action;
    public bool isGrounded;
    public bool isMoving;
    public bool isJumping;
    public bool isTouchingFront;
    public bool isWallSliding;
    public bool isFacingRight;

    [Space (10)]
    [Header("Touch Check")]
    public Transform groundChecker;
    public Transform frontChecker;
    public float checkRadius;
    public LayerMask whatIsGround;

    void Start() {
        SetInitialState();
    }

    void Update() {

        UpdateState();
    }

    private void SetInitialState() {
        isFacingRight = true;
    }

    private void UpdateState() {
        isMoving = action == Actions.Move;
        isJumping = action == Actions.Jump;
        isWallSliding = action == Actions.WallSlide;
        isGrounded = Physics2D.OverlapCircle(groundChecker.position, checkRadius, whatIsGround);
        isTouchingFront = Physics2D.OverlapCircle(frontChecker.position, checkRadius, whatIsGround);
    }
}

public enum Actions {
    Idle, Move, Jump, WallSlide, Fall
}
