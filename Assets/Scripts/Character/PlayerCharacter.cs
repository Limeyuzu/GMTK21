using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : StrongCharacter, IControlSwitchable
{
    //Abilities of all characters
    //Move

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
            ThrowObject();
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
    }

    public void RemoveControl()
    {
        _controlling = false;
        if (_rope)
        {
            _rope.FlipRopeTarget();
        }
        Stop();
    }

    public bool HasControl() => _controlling;

    protected override void Start()
    {
        _rope = GetComponent<Rope>();
        base.Start();
    }

    private void Update()
    {
        if(_controlling == true)
        {
            CheckInputs();
        }
    }
}
