using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Generic.Event;
[RequireComponent(typeof(Collider2D))]
public class LiftAbility : MonoBehaviour
{
    Liftable ObjectLiftable;
    public Transform LiftPosition;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject CollisionObject = collision.gameObject;
        if(AssessLiftObject(CollisionObject) == false)
        {
            return;
        }
        AssignLiftableObject(CollisionObject);
    }
    public void AssignLiftableObject(GameObject Object)
    {
        ObjectLiftable = Object.GetComponent<Liftable>();
        ObjectLiftable.Lock();
        Transform CollisionTransform = Object.transform;
        CollisionTransform.position = LiftPosition.position;
        CollisionTransform.parent = transform;
        EventManager.Emit(GameEvent.PickUpObject);
    }
    bool AssessLiftObject(GameObject Object)
    {
        if (ObjectLiftable != null || Object.GetComponent<Liftable>() == false)
        {
            return false;
        }
        Liftable LiftObject = Object.GetComponent<Liftable>();
        if (LiftObject.CheckLiftableStatus() == false)
        {
            return false;
        }
        return true;
    }
    public void ClearLiftableObject()
    {
        ObjectLiftable.transform.parent = null;
        ObjectLiftable = null;
    }
    public bool CheckForLiftedObject()
    {
        if(ObjectLiftable == null)
        {
            return false;
        }
        return true;
    }
    public Liftable GetLiftedObject()
    {
        return ObjectLiftable;
    }
}
