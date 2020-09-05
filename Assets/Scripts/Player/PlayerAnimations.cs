using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerActions))]
public class PlayerAnimations : MonoBehaviour
{
    private PlayerStateManager state;
    private Animator animator;

    private AnimatorControllerParameter[] parameters;

    void Start()
    {
        state = GetComponent<PlayerStateManager>();
        animator = GetComponent<Animator>();
        parameters = animator.parameters;
    }

    void Update()
    {
        SetCurrentAnimation();
    }

    private void SetCurrentAnimation()
    {
        switch (state.action)
        {
            case Action.Move:
                {
                    Play("Move");
                    break;
                }
            case Action.PrepareToJump:
                {
                    Play("PrepareToJump");
                    break;
                }
            case Action.Jump:
                {
                    Play("Jump", 2.5f);
                    break;
                }
            case Action.Crouch:
                {
                    Play("Crouch");
                    break;
                }
            case Action.Fall:
                {
                    Play("Fall");
                    break;
                }
            default:
                {
                    SetAllFalse();
                    break;
                }
        }
    }

    private void Play(string animationName)
    {
        SetAllFalse();
        SetTrue(animationName);
        animator.speed = 1;
    }

    private void Play(string animationName, float speed)
    {
        SetAllFalse();
        SetTrue(animationName);
        animator.speed = speed;
    }

    private void SetTrue(string parameterName)
    {
        animator.SetBool(parameterName, true);
    }

    private void SetFalse(string parameterName)
    {
        animator.SetBool(parameterName, false);
    }

    private void SetAllFalse()
    {
        foreach (AnimatorControllerParameter parameter in parameters)
        {
            animator.SetBool(parameter.name, false);
            animator.speed = 1;
        }
    }
}
