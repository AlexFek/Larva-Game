using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerActions))]
public class PlayerAnimations : MonoBehaviour
{
    private PlayerActions playerActions;
    private Animator animator;
    private Action currentAction;

    private AnimatorControllerParameter[] parameters;

    void Start()
    {
        playerActions = GetComponent<PlayerActions>();
        animator = GetComponent<Animator>();
        parameters = animator.parameters;
    }

    void Update()
    {
        currentAction = playerActions.currentAction;
        SetCurrentAnimation();
    }

    private void SetCurrentAnimation()
    {
        switch (currentAction)
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
                    Play("Jump");
                    break;
                }
            case Action.Crouch:
            {
                Play("Crouch");
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
        }
    }
}
