using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : MonoBehaviour {
    protected Player player;

    public abstract void DoAction();
    public abstract void PlayAnimation();

    public void SetContext(Player player) {
        this.player = player;
    }

    #region Set State Methods
    protected void TransitionToIdle() {
        player.TransitionTo(new PlayerStateIdle());
    }
    protected void TransitionToRun() {
        player.TransitionTo(new PlayerStateRun());
    }
    protected void TransitionToJump() {
        player.TransitionTo(new PlayerStateJump());
    }
    #endregion

    #region Animation Switch Methods
    protected void StartAnimation(string animationName) {
        player.animator.SetBool(animationName, true);
    }

    protected void SetAnimatorParametersToFalse() {
        foreach(AnimatorControllerParameter parameter in player.animator.parameters) {
            player.animator.SetBool(parameter.name, false);
        }
    }
    #endregion
}
