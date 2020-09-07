using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public bool any { get; private set; }
    public bool moveRight { get; private set; }
    public bool moveLeft { get; private set; }
    public bool prepareToJump { get; private set; }
    public bool lift { get; private set; }

    void Update()
    {
        any = Input.anyKey;
        moveRight = Input.GetKey(KeyCode.D);
        moveLeft = Input.GetKey(KeyCode.A);
        prepareToJump = Input.GetKey(KeyCode.Space);
        lift = Input.GetKeyUp(KeyCode.Space);
    }
}
