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
                    Play("Jump");
                    animator.speed = 2.5f;
                    break;
                }
            case Action.Fall:
                {
                    Play("Fall");
                    break;
                }
            case Action.HangOnWall:
                {
                    Play("HangOnWall");
                    break;
                }
            case Action.Crawl:
                {
                    Play("Crawl");
                    break;
                }
            default:
                {
                    ResetParameters();
                    animator.speed = 1;
                    break;
                }
        }
    }

    private void Play(string animationName)
    {
        animator.speed = 1f;
        ResetParameters();
        SetParameterToTrue(animationName);
    }

    private void SetParameterToTrue(string parameterName)
    {
        animator.SetBool(parameterName, true);
    }

    private void ResetParameters()
    {
        foreach (AnimatorControllerParameter parameter in parameters)
        {
            animator.SetBool(parameter.name, false);
        }
    }
}
