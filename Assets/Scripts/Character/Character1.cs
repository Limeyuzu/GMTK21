using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
public class Character1 : PlayerCharacter
{
    public BombAbility bombAbility;
    public override void CheckInputs()
    {
        base.CheckInputs();
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (liftAbility.CheckForLiftedObject())
            {
                return;
            }
            bombAbility.SpawnBomb();
        }       
    }
    public override void Update()
    {
        base.Update();
        CheckPull();
    }
    public void CheckPull()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _rope.PullRope();
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            _rope.ReleaseRope();
        }
    }
    protected override void Awake()
    {
        base.Awake();
        _rope = GetComponent<Rope>();
    }
}
