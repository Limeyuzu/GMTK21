using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbableDetection : MonoBehaviour
{
    public LayerMask groundLayer;
    public float collisionRadius;
    public Vector2 RightOffset;
    public Vector2 LeftOffset;
    // Start is called before the first frame update
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position + RightOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + LeftOffset, collisionRadius);
    }
    public bool CheckWall()
    {
        Vector2 Position = (Vector2)transform.position + RightOffset;
        Vector2 OppPosition = (Vector2)transform.position + LeftOffset;
        return Physics2D.OverlapCircle(Position, collisionRadius, groundLayer)
            || Physics2D.OverlapCircle(OppPosition, collisionRadius, groundLayer);
    }
}
