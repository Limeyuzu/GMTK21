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
        if (Input.GetKeyDown(PlayerControlKeyCodes.SPECIAL_ABILITY))
        {
            if (liftAbility.CheckForLiftedObject())
            {
                return;
            }
            bombAbility.SpawnBomb();
        }       
    }
    protected override void Update()
    {
        base.Update();
        CheckPull();
    }
    public void CheckPull()
    {
        if (Input.GetKeyDown(PlayerControlKeyCodes.PULL_ROPE))
        {
            GameObjectInstanceManager.GetPlayerRope().PullRope();
        }
        if (Input.GetKeyUp(PlayerControlKeyCodes.PULL_ROPE))
        {
            GameObjectInstanceManager.GetPlayerRope().UnpullRope();
        }
    }

    protected override void Start()
    {
        base.Start();

        AttachSelfToRope();
        ToggleRopeAnchor(true);
    }
}
