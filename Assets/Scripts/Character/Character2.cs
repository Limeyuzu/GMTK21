using UnityEngine;

public class Character2 : PlayerCharacter
{
    ClimbAbility ClimbAbility;
    protected override void Awake()
    {
        base.Awake();
        ClimbAbility = GetComponent<ClimbAbility>();
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

    protected override void Start()
    {
        base.Start();

        AttachSelfToRope();
        ToggleRopeAnchor(false);
    }
}
