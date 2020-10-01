using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateIdle : PlayerState {
    public override void DoAction() { 
        if (player.isGrounded) {
            if (player.PlayerInput.moveInput != 0) {
                TransitionToRun();
            }
            if (player.PlayerInput.jumpInput) {
                TransitionToJump();
            }
        }
    }

    public override void PlayAnimation() {
        SetAnimatorParametersToFalse();
    }
}
