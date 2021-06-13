using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour
{
    public int MoveSpeed;

    protected Rigidbody2D Rigidbody;
    protected Animator Animator;

    public void Move(Vector2 Direction)
    {
        Vector2 NewDir = new Vector2(Direction.x * MoveSpeed, Rigidbody.velocity.y);
        Rigidbody.velocity = NewDir;
        if(Direction == Vector2.left)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
        if (Direction == Vector2.right)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }

        if (Animator)
        {
            Animator.SetBool("IsMoving", Mathf.Abs(Direction.x) > 0);
        }
    }
    public void Stop()
    {
        Move(Vector2.zero);
    }

    protected virtual void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }
}
