using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerActions))]
public class PlayerStateManager : MonoBehaviour
{
    [HideInInspector]
    public Action action;
    [HideInInspector]
    public Direction direction;
    [HideInInspector]
    public Emplacement emplacement;

    [HideInInspector]
    public bool isTouchWallRight;
    [HideInInspector]
    public bool isTouchWallLeft;

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
        isTouchWallRight = raycaster.IsTouchRight();
        isTouchWallLeft = raycaster.IsTouchLeft();
    }

    private Emplacement CheckEmplacement()
    {
        if (!raycaster.IsGrounded())
        {
            return (isTouchWallRight || isTouchWallLeft) ? Emplacement.Wall : Emplacement.Air;
        }
        else
        {
            return Emplacement.Ground;
        }
    }
}

public enum Action
{
    Idle, Move, PrepareToJump, Lift, Jump, Fall, HangOnWall
}
public enum Direction
{
    Right, Left
}
public enum Emplacement
{
    Ground, Air, Wall
}

