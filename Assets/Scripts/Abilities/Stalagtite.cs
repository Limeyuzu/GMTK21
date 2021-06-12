using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalagtite : Breakable
{
    Rigidbody2D RB;
    public PolygonCollider2D OffCollider;
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();   
    }
    public override void BreakAction()
    {
        OffCollider.enabled = false;
        RB.constraints = RigidbodyConstraints2D.None;
    }
}
