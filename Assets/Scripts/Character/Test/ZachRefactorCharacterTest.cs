using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Liftable)),RequireComponent(typeof(ThrowAbility))]
public class ZachRefactorCharacterTest : Character
{
    //Abilities of all characters
    //Move
    Liftable Liftable;
    ThrowAbility ThrowAbility;
    public bool Controlling = false;
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
    public bool CheckControl()
    {
        return Controlling;
    }
    public void BeginControlling()
    {
        Controlling = true;
        Liftable.ToggleLiftable(false);
    }
    public void StopControlling()
    {
        Controlling = false;
        Stop();
        Liftable.ToggleLiftable(true);
    }
    private void Awake()
    {
        Liftable = GetComponent<Liftable>();
        ThrowAbility = GetComponent<ThrowAbility>();
    }
    public override void Start()
    {
        base.Start();       
    }
    private void Update()
    {
        if (Controlling == true)
        {
            CheckInputs();
        }
    }
}
