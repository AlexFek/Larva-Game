using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateRun : PlayerState {
    public override void DoAction() {
        float newVelocityX = player.PlayerInput.moveInput * player.speed;
        float newVelocityY = player.rigidbody.velocity.y;

        if (player.isGrounded) {
            Run(newVelocityX, newVelocityY);
            OnJumpKeyDown();
            OnNoInput();
        } else {
            TransitionToJump();
        }
    }

    public override void PlayAnimation() {
        SetAnimatorParametersToFalse();
        StartAnimation("Run");
    }

    private void Run(float x, float y) {
        TurnPlayerAround();
        player.rigidbody.velocity = new Vector2(x, y);
    }

    private void TurnPlayerAround() {
        if ((player.isFacingRight && player.PlayerInput.moveInput < 0) || (!player.isFacingRight && player.PlayerInput.moveInput > 0)) {
            player.isFacingRight = !player.isFacingRight;
            player.transform.localScale = new Vector2(-player.transform.localScale.x, player.transform.localScale.y);
        }
    }

    private void OnJumpKeyDown() {
        if (player.PlayerInput.jumpInput) {
            TransitionToJump();
        }
    }
    private void OnNoInput() {
        if (player.PlayerInput.noInput) {
            TransitionToIdle();
        }
    }
}
