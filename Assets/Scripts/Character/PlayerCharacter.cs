using Assets.Scripts;
using Assets.Scripts.Generic.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Liftable)), RequireComponent(typeof(ThrowAbility))]
public class PlayerCharacter : Character, IControlSwitchable
{
    Liftable Liftable;
    ThrowAbility ThrowAbility;
    LiftAbility liftAbility;
    private bool _controlling = false;
    private Rope _rope;
    public void CheckInputs()
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
        if(_rope == null)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _rope.PullRope();
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            _rope.ReleaseRope();
        }
    }

    public void GiveControl()
    {
        _controlling = true;
        if (_rope)
        {
            _rope.FlipRopeTarget();
        }
        Liftable.ToggleLiftable(false);
        liftAbility.ToggleLiftability(true);
        FindObjectOfType<CameraTarget>().Parent_Character = this.gameObject;
        FindObjectOfType<CameraTarget>().Reassign();
    }

    public void RemoveControl()
    {
        _controlling = false;
        if (_rope)
        {
            _rope.FlipRopeTarget();
        }
        Stop();
        Liftable.ToggleLiftable(true);
        liftAbility.ToggleLiftability(false);
    }
    public bool HasControl() => _controlling;

    protected void Awake()
    {
        Liftable = GetComponent<Liftable>();
        ThrowAbility = GetComponent<ThrowAbility>();
        _rope = GetComponent<Rope>();
        liftAbility = GetComponent<LiftAbility>();
        SubscribeToPlayerActions();
    }
    private void Update()
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
