using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputReader : MonoBehaviour {
    [HideInInspector]
    public bool noInput;
    [HideInInspector]
    public float moveInput;
    [HideInInspector]
    public bool jumpInput;

    void Update() {
        ReadInput();
    }

    private void ReadInput() {
        noInput = !Input.anyKey;
        moveInput = Input.GetAxisRaw("Horizontal");
        jumpInput = Input.GetKeyDown(KeyCode.Space);
    }
}
