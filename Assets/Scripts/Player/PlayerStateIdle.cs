using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateIdle : PlayerState {
    public override void DoAction() { 
        if (player.isGrounded) {
            Stop();

            if (player.PlayerInput.moveInput != 0) {
                TransitionToRun();
            }
            if (player.PlayerInput.jumpInput) {
                TransitionToJump();
            }
        } else {
            TransitionToJump();
        }
    }

    public override void PlayAnimation() {
        SetAnimatorParametersToFalse();
    }

    private void Stop() {
        player.rigidbody.velocity = new Vector2(0, player.rigidbody.velocity.y);
    }
}
