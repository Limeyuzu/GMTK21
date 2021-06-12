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
            GameObjectInstanceManager.GetPlayerRope().PullRope();
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            GameObjectInstanceManager.GetPlayerRope().UnpullRope();
        }
    }
}
