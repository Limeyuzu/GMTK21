using Assets.Scripts;
using Assets.Scripts.Generic.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Liftable)), RequireComponent(typeof(ThrowAbility))]
public class PlayerCharacter : Character, IControlSwitchable
{
    [SerializeField] float WalkSoundCooldown = 0.5f;

    protected Liftable Liftable;
    protected ThrowAbility ThrowAbility;
    protected LiftAbility liftAbility;

    private bool _controlling = false;
    private IRope _rope;
    private bool _ropeAttached;
    private bool _isMovingFromInput;
    private float _currentEmitWalkEventCooldown;

    private PlayerFeet _playerFeet;
    public virtual void CheckInputs()
    {
        Vector2 Dir = Vector2.zero;
        if (Input.GetKey(PlayerControlKeyCodes.MOVE_LEFT))
        {
            Dir += Vector2.left;
        }
        if (Input.GetKey(PlayerControlKeyCodes.MOVE_RIGHT))
        {
            Dir += Vector2.right;
        }
        if (Input.GetKeyDown(PlayerControlKeyCodes.THROW_OBJECT))
        {
            ThrowAbility.ThrowObject();
        }
        if (Input.GetKeyDown(PlayerControlKeyCodes.DETACH_ROPE))
        {
            DetachSelfFromRope();
        }

        _isMovingFromInput = Dir != Vector2.zero;

        Move(Dir);
    }

    public virtual void GiveControl(bool Locked)
    {
        _controlling = true;
        Liftable.ToggleLiftable(false);
        liftAbility.ToggleLiftability(true);
        if (Locked)
        {
            return;
        }
        ToggleRopeAnchor(true);
    }

    public virtual void RemoveControl(bool Locked)
    {
        _controlling = false;
        Stop();
        Liftable.ToggleLiftable(true);
        liftAbility.ToggleLiftability(false);
        if (Locked)
        {
            return;
        }
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
        _playerFeet = GetComponentInChildren<PlayerFeet>();
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

        HandleEmitWalkingEvent();

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

    private void HandleEmitWalkingEvent()
    {
        if (!_isMovingFromInput)
        {
            _currentEmitWalkEventCooldown = 0;
            return;
        }

        if (_currentEmitWalkEventCooldown <= 0 && HasControl() && _playerFeet.IsTouchingGround())
        {
            _currentEmitWalkEventCooldown = WalkSoundCooldown;
            EventManager.Emit(GameEvent.PlayerWalk);
        }

        _currentEmitWalkEventCooldown -= Time.deltaTime;
    }
}
