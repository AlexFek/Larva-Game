using UnityEngine;

public class PlayerAnimationUpdator : MonoBehaviour {
    [SerializeField]
    private float regularAnimationSpeed = 1f;
    [SerializeField]
    private float jumpAnimationSpeed = 2f;

    private Animator animator;
    private PlayerState state;

    void Start() {
        InitializeInstances();
    }

    void Update() {
        UpdateAnimtion();
    }

    private void UpdateAnimtion() {
        if (state.isMoving)
            PlayMove();
        else if (state.isJumping)
            PlayJump();
        else if (state.isWallSliding)
            PlayWallSlide();
        else
            PlayIdle();
    }

    private void InitializeInstances() {
        animator = GetComponent<Animator>();
        state = GetComponent<PlayerState>();
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
        if (state.isGrounded) {
            ResetParameters();
        }
    }

    private void Play(string animationName) {
        ResetParameters();
        SetParamToTrue(animationName);
    }

    private void ResetParameters() {
        SetAnimationSpeed(regularAnimationSpeed);
        SetAllParametersToFalse();
    }

    private void SetAnimationSpeed(float speed) {
        animator.speed = speed;
    }

    private void SetParamToTrue(string animationName) {
        animator.SetBool(animationName, true);
    }

    private void SetAllParametersToFalse() {
        foreach (AnimatorControllerParameter parameter in animator.parameters) {
            animator.SetBool(parameter.name, false);
        }
    }
}
