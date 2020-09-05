using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public bool any { get; private set; }
    public bool moveRight { get; private set; }
    public bool moveLeft { get; private set; }
    public bool jumpPrepare { get; private set; }
    public bool jump { get; private set; }
    public bool crouch { get; private set; }

    void Update()
    {
        any = Input.anyKey;
        moveRight = Input.GetKey(KeyCode.D);
        moveLeft = Input.GetKey(KeyCode.A);
        jumpPrepare = Input.GetKey(KeyCode.Space);
        jump = Input.GetKeyUp(KeyCode.Space);
        crouch = Input.GetKey(KeyCode.C);
    }
}
