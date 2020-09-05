using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    [Header("Active skills values")]
    public Vector3 farsighOffset;

    void Update()
    {
        FollowCharacter();
    }

    public void LookRight()
    {
        transform.position += farsighOffset;
    }

    private void FollowCharacter()
    {
        transform.position = target.position + offset;
    }
}
