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
    private IRope _rope;
    private bool _ropeAttached;

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
        if (Input.GetKeyDown(KeyCode.R))
        {
            DetachSelfFromRope();
        }
        Move(Dir);
    }

    public virtual void GiveControl()
    {
        _controlling = true;
        Liftable.ToggleLiftable(false);
        liftAbility.ToggleLiftability(true);
        ToggleRopeAnchor(true);
    }

    public virtual void RemoveControl()
    {
        _controlling = false;
        Stop();
        Liftable.ToggleLiftable(true);
        liftAbility.ToggleLiftability(false);
        ToggleRopeAnchor(false);
    }
    public bool HasControl() => _controlling;

    public void AttachSelfToRope()
    {
        if (!_ropeAttached)
        {
            _rope.Attach(Rigidbody);
            _ropeAttached = true;
        }
    }

    public void DetachSelfFromRope()
    {
        if (_ropeAttached)
        {
            _rope.Detach(Rigidbody);
            _ropeAttached = false;
        }
    }

    public void ToggleRopeAnchor(bool anchored)
    {
        if (_ropeAttached)
        {
            if (anchored)
            {
                _rope.Anchor(Rigidbody);
            }
            else
            {
                _rope.Unanchor(Rigidbody);
            }
        }
    }

    protected override void Awake()
    {
        base.Awake();
        Liftable = GetComponent<Liftable>();
        ThrowAbility = GetComponent<ThrowAbility>();
        liftAbility = GetComponent<LiftAbility>();
    }

    protected virtual void Start()
    {
        SubscribeToPlayerActions();
        _rope = GameObjectInstanceManager.GetPlayerRope();
    }

    protected virtual void Update()
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
