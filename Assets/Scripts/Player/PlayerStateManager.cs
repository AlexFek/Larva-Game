using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Action
{
    Idle, Move, PrepareToJump, Lift, Jump, Crouch, Fall
}
public enum Direction
{
    Right, Left
}
public enum Emplacement
{
    Ground, Air
}

[RequireComponent(typeof(PlayerActions))]
public class PlayerStateManager : MonoBehaviour
{
    [HideInInspector]
    public Action action;

    [HideInInspector]
    public Direction direction;

    [HideInInspector]
    public Emplacement emplacement;

    private PlayerRaycaster raycaster;

    void Start()
    {
        raycaster = GetComponent<PlayerRaycaster>();
        direction = Direction.Right;
        action = Action.Idle;
    }

    void Update()
    {
        emplacement = CheckEmplacement();
    }

    private Emplacement CheckEmplacement()
    {
        return raycaster.IsGrounded() ? Emplacement.Ground : Emplacement.Air;
    }
}
