using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycaster : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Vector2 center;
    private Vector2 extents;

    private float extraLength = 0.025f;
    private int layerMask = 1 << 9;
    
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        center = boxCollider.bounds.center;
        extents = boxCollider.bounds.extents;
    }

    public bool IsGrounded()
    {
        float distance = (extents.x - extraLength) * 2;
        Vector2 start = new Vector2((center.x - extents.x + extraLength), (center.y - extents.y - extraLength));
        Vector2 direction = Vector2.right;
        // Debug.DrawRay(start, direction * distance, Color.red);
        return IsCollide(start, direction, distance, layerMask);
    }

    public bool IsTouchRight()
    {
        float distance = (extents.y - extraLength) * 2;
        Vector2 start = new Vector2((center.x + extents.x + extraLength), (center.y + extents.y - extraLength));
        Vector2 direction = Vector2.down;
        // Debug.DrawRay(start, direction * distance, Color.red);
        return IsCollide(start, direction, distance, layerMask);
    }

    public bool IsTouchLeft()
    {
        float distance = (extents.y - extraLength) * 2;
        Vector2 start = new Vector2((center.x - extents.x - extraLength), (center.y + extents.y - extraLength));
        Vector2 direction = Vector2.down;
        // Debug.DrawRay(start, direction * distance, Color.red);
        return IsCollide(start, direction, distance, layerMask);
    }

    private bool IsCollide(Vector2 start, Vector2 direction, float distance, int layerMask)
    {
        RaycastHit2D hit = Physics2D.Raycast(start, direction, distance, layerMask);
        return hit.collider;
    }
}
