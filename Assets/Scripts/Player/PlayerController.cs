using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [Header("Player Characteristics")]
    public float speed;
    public float jumpUpForce;
    public float jumpForwardForce;
    public float wallJumpForwardForce;
    public float wallJumpUpForce;
    public float wallSlidingSpeed;

    private Rigidbody2D rb;
    private PlayerState state;
    private PlayerInput input;

    [Space(10)]
    public bool shouldFlip;
    public bool shouldMove;

    void Start() {
        InitializeInstances();
        FreezePlayerRotation();
    }

    void Update() {
        UpdateConditions();

        if (state.isGrounded) {

        }
    }

    private void FixedUpdate() {
        if (state.isGrounded) {
            Move();
        }
    }

    private void UpdateConditions() {
        shouldFlip = (state.isFacingRight && input.moveInput < 0) || (!state.isFacingRight && input.moveInput > 0);
        shouldMove = (input.moveInput > 0) || (input.moveInput < 0);
    }

    private void InitializeInstances() {
        rb = GetComponent<Rigidbody2D>();
        state = GetComponent<PlayerState>();
        input = GetComponent<PlayerInput>();
    }

    private void FreezePlayerRotation() {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void Jump() {

    }

    private void WallJump() {

    }

    private void WallSlide() {
        AddVelocity(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, 0));
    }

    private void Move() {
        AddVelocity(input.moveInput * speed, rb.velocity.y);
    }

    private void AddVelocity(float x, float y) {
        rb.velocity = new Vector2(x, y);
    }

    private void Flip() {
        ReflectPlayer();
        ChangeFacingState();
    }

    private void SetActoinStateToIdle() {
        SetActionState(Actions.Idle);
    }
    private void SetActionStateToMove() {
        SetActionState(Actions.Move);
    }
    private void SetActionStateToJump() {
        SetActionState(Actions.Jump);
    }
    private void SetActionStateToWallSlide() {
        SetActionState(Actions.WallSlide);
    }
    private void SetActionStateToFall() {
        SetActionState(Actions.Fall);
    }
    private void SetActionState(Actions action) {
        state.action = action;
    }
    
    private void SetWallSlidingState() {
        state.isWallSliding = state.isTouchingFront && !state.isGrounded && (input.moveInput != 0);
    }
    
    private void ChangeFacingState() {
        state.isFacingRight = !state.isFacingRight;
    }
    private void ReflectPlayer() {
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }
}
