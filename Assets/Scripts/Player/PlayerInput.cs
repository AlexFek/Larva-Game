using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {
    [HideInInspector]
    public bool noInput;
    [HideInInspector]
    public float moveInput;
    [HideInInspector]
    public bool jumpInput;

    void Update() {
        ListenForInput();
    }

    private void ListenForInput() {
        noInput = !Input.anyKey;
        moveInput = Input.GetAxisRaw("Horizontal");
        jumpInput = Input.GetKeyUp(KeyCode.Space);
    }
}
