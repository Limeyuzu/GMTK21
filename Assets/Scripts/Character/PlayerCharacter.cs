using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerCharacter : StrongCharacter
{
    //Abilities of all characters
    //Move
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
            ThrowObject();
        }
        Move(Dir);
    }

    private void Update()
    {
        if(Controlling == true)
        {
            CheckInputs();
        }
    }
}
