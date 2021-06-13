using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
[RequireComponent(typeof(ClimbableDetection))]
public class ClimbAbility : MonoBehaviour
{
    ClimbableDetection ClimbableDetection;
    Rigidbody2D Rigidbody2D;
    CharacterControlManager ControlManager;
    bool AttachedToWall;
    bool Detecting = false;
    // Start is called before the first frame update
    void Start()
    {
        ClimbableDetection = GetComponent<ClimbableDetection>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        ControlManager = FindObjectOfType<CharacterControlManager>();
    }
    public void ToggleDetecting()
    {
        Detecting = !Detecting;
    }
    public void BeginDetecting()
    {
        Detecting = true;
    }
    public void StopDetecting()
    {
        Detecting = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Detecting == false)
        {
            return;
        }
        if(AttachedToWall == false)
        {
            if (ClimbableDetection.CheckWall())
            {
                AttachToWall();
            }
        }
        if(!ClimbableDetection.CheckWall())
        {
            if (AttachedToWall)
            {
                DetachfromWall();
            }          
        }
    }
    public bool CheckAttachStatus() => AttachedToWall;
    public void TakeClimbInputs()
    {
        Vector2 Dir = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            Dir += Vector2.up;
        }
        if (Input.GetKey(KeyCode.S))
        {
            Dir += Vector2.down;
        }
        Climb(Dir);
    }
    public void Climb(Vector2 Dir)
    {
        Vector2 VelocityDir = new Vector2(Rigidbody2D.velocity.x, Dir.y * 5);
        Rigidbody2D.velocity = VelocityDir;
    }
    public void AttachToWall()
    {
        Rigidbody2D.gravityScale = 0;
        AttachedToWall = true;
        ControlManager.SetAnchorLock(true);
    }
    public void DetachfromWall()
    {
        Rigidbody2D.gravityScale = 1;
        AttachedToWall = false;
        ControlManager.SetAnchorLock(false);
    }
}
