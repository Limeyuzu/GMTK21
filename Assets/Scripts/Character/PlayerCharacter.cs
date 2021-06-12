using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Liftable)), RequireComponent(typeof(ThrowAbility))]
public class PlayerCharacter : Character, IControlSwitchable
{
    //Abilities of all characters
    //Move
    Liftable Liftable;
    ThrowAbility ThrowAbility;
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
    }

    public void GiveControl()
    {
        _controlling = true;
        if (_rope)
        {
            _rope.FlipRopeTarget();
        }
        Liftable.ToggleLiftable(false);
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
    }
    public bool HasControl() => _controlling;
    private void Awake()
    {
        Liftable = GetComponent<Liftable>();
        ThrowAbility = GetComponent<ThrowAbility>();
        _rope = GetComponent<Rope>();
    }
    private void Update()
    {
        if(_controlling == true)
        {
            CheckInputs();
        }
    }
}
