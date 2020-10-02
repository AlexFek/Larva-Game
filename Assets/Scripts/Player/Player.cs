using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Player : MonoBehaviour {
    #region Public Characteristics
    public float speed = 1.2f;
    public float jumpForceX = 1.3f;
    public float jumpForceY = 0.77f;
    #endregion

    #region Current State
    [HideInInspector]
    public bool isGrounded;
    [HideInInspector]
    public bool isFacingRight;
    #endregion

    #region Touch Checkers
    [Header("Touch Checkers")]
    [Space(10)]
    public Transform groundChecker;
    public float checkRadius = 0.01f;
    public LayerMask whatIsGround;
    #endregion

    [HideInInspector]
    public PlayerInputReader PlayerInput { get; private set; }
    [HideInInspector]
    public Rigidbody2D rigidbody;
    [HideInInspector]
    public Animator animator;
    private PlayerState state;

    void Awake() {
        InitializeInstances();
        FreezePlayerRotation();
        SetInitialFacingDirection();
        SetInitialState();
    }

    void Update() {
        CheckGrounding();
        state.DoAction();
        state.PlayAnimation();
    }

    #region Awake Methods
    private void InitializeInstances() {
        PlayerInput = GetComponent<PlayerInputReader>();
        rigidbody = GetComponent<Rigidbody2D>();
        state = GetComponent<PlayerState>();
        animator = GetComponent<Animator>();
    }

    private void FreezePlayerRotation() {
        rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void SetInitialFacingDirection() {
        isFacingRight = true;
    }

    private void SetInitialState() {
        TransitionTo(new PlayerStateIdle());
    }
    #endregion

    public void TransitionTo(PlayerState state) {
        this.state = state;
        this.state.SetContext(this);
    }

    private void CheckGrounding() {
        isGrounded = Physics2D.OverlapCircle(groundChecker.position, checkRadius);
    }
}
