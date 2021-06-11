using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControlManager : MonoBehaviour
{
    public PlayerCharacter Character1;
    public PlayerCharacter Character2;

    public void SwitchCharacters()
    {
        //if (Character1.transform.parent != null || Character2.transform.parent != null)
        //{
        //    return;
        //}
        if (Character1.HasControl())
        {
            Character1.RemoveControl();
            Character2.GiveControl();
            return;
        }

        Character2.RemoveControl();
        Character1.GiveControl();
    }

    private void Start()
    {
        Character1.GiveControl();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwitchCharacters();
        }
    }
}
