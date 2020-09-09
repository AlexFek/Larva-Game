using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private PlayerController controller;

    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<PlayerController>();
    }

    void Update()
    {
        switch (controller.action)
        {
            case Actions.Move:
                {
                    Play("Move");
                    break;
                }
            case Actions.Jump:
                {
                    Play("Jump");
                    animator.speed = 2f;
                    break;
                }
            case Actions.WallSliding:
                {
                    Play("WallSliding");
                    break;
                }
            default:
                {
                    if (controller.isGrounded)
                    {
                        SetAllParamsToFalse();
                    }
                    break;
                }
        }
    }

    private void Play(string animationName)
    {
        SetAllParamsToFalse();
        SetParamToTrue(animationName);
    }

    private void SetParamToTrue(string animationName)
    {
        animator.SetBool(animationName, true);
    }

    private void SetAllParamsToFalse()
    {
        animator.speed = 1f;
        foreach (AnimatorControllerParameter parameter in animator.parameters)
        {
            animator.SetBool(parameter.name, false);
        }
    }
}
