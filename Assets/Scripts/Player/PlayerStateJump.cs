using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateJump : PlayerState {
    public override void DoAction() {
        float newVelocityX = player.PlayerInput.moveInput * player.jumpForceX;
        float newVelocityY = player.jumpForceY;

        if (player.isGrounded) {
            Jump(newVelocityX, newVelocityY);
        }
        DoOnGrounding();
    }

    public override void PlayAnimation() {
        SetAnimatorParametersToFalse();
        StartAnimation("Jump");
    }

    private void Jump(float jumpForceX, float jumpForceY) {
        player.rigidbody.AddForce(new Vector2(jumpForceX, jumpForceY), ForceMode2D.Impulse);
    }

    private void DoOnGrounding() {
        if (player.isGrounded) {
            TransitionToIdle();
        }
    }
}
