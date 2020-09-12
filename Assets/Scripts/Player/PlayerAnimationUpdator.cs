using UnityEngine;

public class PlayerAnimationUpdator : MonoBehaviour {
    [SerializeField]
    private float regularAnimationSpeed = 1f;
    [SerializeField]
    private float jumpAnimationSpeed = 2f;

    private Animator animator;
    private PlayerController controller;

    private Actions playerAction;
    private Actions actionMove = Actions.Move;
    private Actions actionJump = Actions.Jump;
    private Actions actionWallSlide = Actions.WallSlide;

    private bool isMoving;
    private bool isJumping;
    private bool isWallSliding;

    void Start() {
        InitVariables();
    }

    void Update() {
        UpdateVariables();
        UpdateAnimtion();
    }

    private void UpdateAnimtion() {
        if (isMoving)
            PlayMove();
        else if (isJumping)
            PlayJump();
        else if (isWallSliding)
            PlayWallSlide();
        else
            PlayIdle();
    }

    private void InitVariables() {
        animator = GetComponent<Animator>();
        controller = GetComponent<PlayerController>();
        playerAction = controller.action;
    }

    private void UpdateVariables() {
        isMoving = playerAction == actionMove;
        isJumping = playerAction == actionJump;
        isWallSliding = playerAction == actionWallSlide;
    }

    private void PlayWallSlide() {
        Play("WallSlide");
    }

    private void PlayJump() {
        SetAnimationSpeed(jumpAnimationSpeed);
        Play("Jump");
    }

    private void PlayMove() {
        Play("Move");
    }

    private void PlayIdle() {
        if (controller.isGrounded) {
            ResetParameters();
        }
    }

    private void Play(string animationName) {
        ResetParameters();
        SetParamToTrue(animationName);
    }

    private void SetAnimationSpeed(float speed) {
        animator.speed = speed;
    }

    private void SetParamToTrue(string animationName) {
        animator.SetBool(animationName, true);
    }

    private void ResetParameters() {
        SetAnimationSpeed(regularAnimationSpeed);
        foreach (AnimatorControllerParameter parameter in animator.parameters) {
            animator.SetBool(parameter.name, false);
        }
    }
}
