using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
public class Character2 : PlayerCharacter
{
    ClimbAbility ClimbAbility;
    CharacterControlManager CharacterControlManager;
    protected override void Awake()
    {
        base.Awake();
        ClimbAbility = GetComponent<ClimbAbility>();
        CharacterControlManager = FindObjectOfType<CharacterControlManager>();
    }
    public override void CheckInputs()
    {
        base.CheckInputs();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ClimbAbility.DetachfromWall();
            ClimbAbility.ToggleDetecting();
        }
    }
    public override void GiveControl()
    {
        base.GiveControl();
        if (ClimbAbility.CheckAttachStatus() == true)
        {
            Rigidbody.isKinematic = false;
            //_rope.FlipRopeTarget();           
        }
    }
    public override void RemoveControl()
    {
        base.RemoveControl();
        if(ClimbAbility.CheckAttachStatus() == true)
        {
            Rigidbody.isKinematic = true;
        }
        ClimbAbility.StopDetecting();
    }
}
