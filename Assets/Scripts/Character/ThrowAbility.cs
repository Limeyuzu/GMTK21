using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(LiftAbility))]
public class ThrowAbility : MonoBehaviour
{
    LiftAbility LiftAbility;
    public Vector2 ThrowDirection;
    public int ThrowSpeed;
    private void Start()
    {
        LiftAbility = GetComponent<LiftAbility>();
    }
    public void ThrowObject()
    {
        if(LiftAbility.CheckForLiftedObject() == false)
        {
            return;
        }
        Liftable ThrowObject = LiftAbility.GetLiftedObject();
        ThrowObject.Unlock();
        Rigidbody2D RB = ThrowObject.ObjectRigidBody;
        Vector2 NewDir = new Vector2((ThrowDirection.x * transform.localScale.x), ThrowDirection.y);
        RB.velocity = NewDir * ThrowSpeed;
        LiftAbility.ClearLiftableObject();
    }
}
