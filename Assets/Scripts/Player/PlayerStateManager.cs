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

    private PlayerColliderPhysics colliderPhysics;

    void Start()
    {
        colliderPhysics = GetComponent<PlayerColliderPhysics>();
        direction = Direction.Right;
        action = Action.Idle;
    }

    void Update()
    {
        emplacement = CheckEmplacement();
        isTouchWallRight = colliderPhysics.IsTouchRight();
        isTouchWallLeft = colliderPhysics.IsTouchLeft();
    }

    private Emplacement CheckEmplacement()
    {
        if (!colliderPhysics.IsGrounded())
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
    Idle, Move, PrepareToJump, Lift, Jump, Fall, HangOnWall, Crawl
}
public enum Direction
{
    Right, Left
}
public enum Emplacement
{
    Ground, Air, Wall
}

