using UnityEngine;
using Assets.Scripts;
using UnityEngine.UI;
using System.Collections;

public class Character2 : PlayerCharacter
{
    ClimbAbility ClimbAbility;
    CharacterControlManager CharacterControlManager;
    public GameObject Radial;
    public Image StaminaCountdown;
    public float GripTime;
    protected override void Awake()
    {
        base.Awake();
        ClimbAbility = GetComponent<ClimbAbility>();
        CharacterControlManager = FindObjectOfType<CharacterControlManager>();
    }
    protected override void Update()
    {
        base.Update();
        if (ClimbAbility.CheckAttachStatus() && HasControl() == true)
        {
            ClimbAbility.TakeClimbInputs();
        }
    }
    public override void CheckInputs()
    {
        base.CheckInputs();
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            ClimbAbility.DetachfromWall();
            ClimbAbility.ToggleDetecting();
        }
    }
    public override void GiveControl(bool Lock)
    {
        base.GiveControl(Lock);
        if (ClimbAbility.CheckAttachStatus() == true)
        {          
            Rigidbody.isKinematic = false;
            StopCoroutine(DetachTimer());
        }        
    }
    IEnumerator DetachTimer()
    {
        Radial.SetActive(true);
        for (float i = 0; i < GripTime; i+= Time.deltaTime)
        {
            float Count = GripTime - i;
            StaminaCountdown.fillAmount = Count / GripTime;
            //StaminaCountdown.text = Count.ToString();
            yield return null;
        }
        Radial.SetActive(false);
        Rigidbody.isKinematic = false;
        ClimbAbility.DetachfromWall();
        CharacterControlManager.ControlCharacter1();
    }
    public override void RemoveControl(bool Lock)
    {
        base.RemoveControl(Lock);
        if(ClimbAbility.CheckAttachStatus() == true)
        {
            Rigidbody.isKinematic = true;
            StartCoroutine(DetachTimer());
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
