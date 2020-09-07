using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderPhysics : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private RaycastHit2D hit;
    private Vector2 start;
    private Vector2 direction;
    private Vector2 center;
    private Vector2 extents;
    private Vector2 size;
    private Vector2 defaulSize;
    private Vector2 defaulOffset;

    private float extraLength = 0.025f;
    private int layerMask = 1 << 9;
    private float distance;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        defaulSize = boxCollider.size;
        defaulOffset = boxCollider.offset;
    }

    void Update()
    {
        center = boxCollider.bounds.center;
        extents = boxCollider.bounds.extents;
        size = boxCollider.size;
    }

    public void ResetColliderShape()
    {
        boxCollider.size = defaulSize;
        boxCollider.offset = defaulOffset;
    }
    public void SqueezePlayer()
    {
        if (hit.collider)
        {
            boxCollider.size = new Vector2(boxCollider.size.x, boxCollider.size.y - Mathf.Abs(transform.position.y - hit.point.y));
            boxCollider.offset = new Vector2(boxCollider.offset.x, boxCollider.offset.y - Mathf.Abs(transform.position.y - hit.point.y));
            Debug.Log(hit.point.y);
        }
    }
    private RaycastHit2D CastRay(Vector2 start, Vector2 direction, float distance, int layerMask)
    {
        return Physics2D.Raycast(start, direction, distance, layerMask);
    }

    ///==========================///
    /// CHECK COLLISIONS METHODS ///
    ///==========================///
    
    public bool IsGrounded()
    {
        start = new Vector2((center.x - extents.x + extraLength), (center.y - extents.y - extraLength));
        distance = size.y - extraLength * 2;
        direction = Vector2.right;
        // Debug.DrawRay(start, direction * distance, Color.red);
        return IsCollide(start, direction, distance, layerMask);
    }
    public bool IsTouchUp()
    {
        start = new Vector2((center.x - extents.x - extraLength), (center.y + extents.y + extraLength));
        distance = (extents.x + extraLength) * 2;
        direction = Vector2.right;
        hit = CastRay(start, direction, distance, layerMask);
        Debug.DrawRay(start, direction * distance, Color.red);
        return IsCollide(start, direction, distance, layerMask);
    }
    public bool IsTouchRight()
    {
        start = new Vector2((center.x + extents.x + extraLength), (center.y + extents.y));
        distance = size.y;
        direction = Vector2.down;
        hit = CastRay(start, direction, distance, layerMask);
        Debug.DrawRay(start, direction * distance, Color.red);
        return IsCollide(start, direction, distance, layerMask);
    }
    public bool IsTouchLeft()
    {
        start = new Vector2((center.x - extents.x - extraLength), (center.y + extents.y));
        distance = size.y;
        direction = Vector2.down;
        hit = CastRay(start, direction, distance, layerMask);
        // Debug.DrawRay(start, direction * distance, Color.red);
        return IsCollide(start, direction, distance, layerMask);
    }
    private bool IsCollide(Vector2 start, Vector2 direction, float distance, int layerMask)
    {
        RaycastHit2D hit = CastRay(start, direction, distance, layerMask);
        return hit.collider;
    }
}
