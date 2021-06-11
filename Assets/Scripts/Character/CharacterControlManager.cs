using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControlManager : MonoBehaviour
{
    public PlayerCharacter Character1;
    public PlayerCharacter Character2;
    public void SwitchCharacters()
    {
        if (Character1.transform.parent != null || Character2.transform.parent != null)
        {
            return;
        }
        if(Character1.Controlling == true)
        {
            Character1.Controlling = false;
            Character2.Controlling = true;
            Character1.Stop();
            return;
        }
        Character1.Controlling = true;
        Character2.Controlling = false;
        Character2.Stop();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwitchCharacters();
        }
    }
}
