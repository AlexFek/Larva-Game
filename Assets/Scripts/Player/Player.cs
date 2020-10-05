using System.Collections;
using System.Collections.Generic;
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

    #region Touch Checker
    [Header("Touch Checker")]
    [Space(10)]
    public LayerMask whatIsGround;
    public float extraLength = 0.016f;
    #endregion

    [HideInInspector]
    public PlayerInputReader PlayerInput { get; private set; }
    [HideInInspector]
    public Rigidbody2D rigidbody;
    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    private BoxCollider2D boxCollider;
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
        boxCollider = GetComponent<BoxCollider2D>();
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
        RaycastHit2D hit = Physics2D.Raycast(boxCollider.bounds.center, Vector2.down, boxCollider.bounds.extents.y + extraLength, whatIsGround);
        isGrounded = hit.collider != null;
    }
}
