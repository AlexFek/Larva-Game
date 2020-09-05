using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Action
{
    Idle, Move, PrepareToJump, Jump, Crouch, Fall
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
    [HideInInspector, TabGroup("States")]
    public Action action;
    public Direction direction;
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
