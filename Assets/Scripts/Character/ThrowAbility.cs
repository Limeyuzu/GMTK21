using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Generic.Event;
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

        // Chaces Trail Render addition
        ThrowObject.RenderTrail();

        Rigidbody2D RB = ThrowObject.ObjectRigidBody;
        Vector2 NewDir = new Vector2((ThrowDirection.x * transform.localScale.x), ThrowDirection.y);
        RB.velocity = NewDir * ThrowSpeed;
        LiftAbility.ClearLiftableObject();
        if (ThrowObject.gameObject.GetComponent<Bomb>() != null)
        {
            EventManager.Emit(GameEvent.ThrowBomb, gameObject);
        }
        else
        {
            EventManager.Emit(GameEvent.ThrowCharacter, gameObject);
        }

    }
}
