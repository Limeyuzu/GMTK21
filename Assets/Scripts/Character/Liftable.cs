using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Liftable : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody2D ObjectRigidBody;
    private void Start()
    {
        ObjectRigidBody = GetComponent<Rigidbody2D>();
    }
    public void Lock()
    {
        ObjectRigidBody.isKinematic = true;
        ObjectRigidBody.velocity = Vector2.zero;
    }
    public void Unlock()
    {
        ObjectRigidBody.isKinematic = false;
    }
}