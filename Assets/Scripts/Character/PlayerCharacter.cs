using Assets.Scripts;
using Assets.Scripts.Generic.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Liftable)), RequireComponent(typeof(ThrowAbility))]
public class PlayerCharacter : Character, IControlSwitchable
{
    protected Liftable Liftable;
    protected ThrowAbility ThrowAbility;
    protected LiftAbility liftAbility;
    private bool _controlling = false;
    public virtual void CheckInputs()
    {
        Vector2 Dir = Vector2.zero;
        if (Input.GetKey(KeyCode.A))
        {
            Dir += Vector2.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            Dir += Vector2.right;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            ThrowAbility.ThrowObject();
        }        
        Move(Dir);
    }

    public void GiveControl()
    {
        _controlling = true;
        Liftable.ToggleLiftable(false);
        liftAbility.ToggleLiftability(true);
    }

    public void RemoveControl()
    {
        _controlling = false;
        Stop();
        Liftable.ToggleLiftable(true);
        liftAbility.ToggleLiftability(false);
    }
    public bool HasControl() => _controlling;

    protected virtual void Awake()
    {
        Liftable = GetComponent<Liftable>();
        ThrowAbility = GetComponent<ThrowAbility>();
        liftAbility = GetComponent<LiftAbility>();
        SubscribeToPlayerActions();
    }
    public virtual void Update()
    {
        if(_controlling == true)
        {
            CheckInputs();
        }

        // TODO Should include being in the air going up, not just down
        var isFalling = this.Rigidbody.velocity.y < -0.2f;
        Animator.SetBool("IsFalling", isFalling);
        Animator.SetBool("IsBeingPickedUp", Liftable.IsBeingLifted);
    }

    private void SubscribeToPlayerActions()
    {
        EventManager.Subscribe(GameEvent.PickUpObject, o => {
            var gameObject = (GameObject)o;
            if (gameObject == this.gameObject) 
            { 
                Animator.SetBool("IsPickingUp", true); 
            }
        });
        EventManager.Subscribe(GameEvent.ThrowCharacter, o => {
            var gameObject = (GameObject)o;
            if (gameObject != this.gameObject) { return; }

            Animator.SetBool("IsPickingUp", false);
            Animator.SetTrigger("IsThrowing");
        });
    }
}
