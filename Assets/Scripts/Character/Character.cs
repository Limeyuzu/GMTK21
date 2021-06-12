using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour
{
    Rigidbody2D Rigidbody;
    public int MoveSpeed;
    public void Move(Vector2 Direction)
    {
        Vector2 NewDir = new Vector2(Direction.x * MoveSpeed, Rigidbody.velocity.y);
        Rigidbody.velocity = NewDir;
    }
    public void Stop()
    {
        Move(Vector2.zero);
    }
    // Start is called before the first frame update
    protected void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }
}
