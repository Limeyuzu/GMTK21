using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongCharacter : Character
{
    public Transform LiftPosition;
    public int ThrowSpeed;
    public GameObject LiftedObject = null;
    Liftable ObjectLiftable;
    public Vector2 ThrowDirection;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject CollisionObject = collision.gameObject;
        if(CollisionObject.GetComponent<Liftable>() != true || LiftedObject != null)
        {
            return;
        }
        if(CollisionObject.GetComponent<PlayerCharacter>() == true)
        {
            if (CollisionObject.GetComponent<PlayerCharacter>().Controlling == true)
            {
                return;
            }
        }
        if(gameObject.GetComponent<PlayerCharacter>().Controlling == false)
        {
            return;
        }
        AssignLiftableObject(CollisionObject);
    }
    public void AssignLiftableObject(GameObject Object)
    {
        Transform CollisionTransform = Object.transform;
        ObjectLiftable = Object.GetComponent<Liftable>();
        ObjectLiftable.Lock();
        CollisionTransform.position = LiftPosition.position;
        CollisionTransform.parent = transform;
        LiftedObject = Object;
    }
    public void ClearLiftableObject()
    {
        LiftedObject.transform.parent = null;
        LiftedObject = null;
        ObjectLiftable = null;
    }
    public void ThrowObject()
    {
        if(LiftedObject == null)
        {
            return;
        }
        ObjectLiftable.Unlock();
        Rigidbody2D RB = ObjectLiftable.ObjectRigidBody;
        Vector2 NewDir = new Vector2((ThrowDirection.x * -1), ThrowDirection.y);
        RB.velocity = ThrowDirection * ThrowSpeed;
        ClearLiftableObject();
    }
}
