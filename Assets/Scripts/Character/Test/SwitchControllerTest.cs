using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchControllerTest : MonoBehaviour
{
    public ZachRefactorCharacterTest Character1;
    public ZachRefactorCharacterTest Character2;
    public void SwitchCharacters()
    {
        if (Character1.transform.parent != null || Character2.transform.parent != null)
        {
            return;
        }
        if(Character1.CheckControl() == true)
        {
            Character1.StopControlling();
            Character2.BeginControlling();
            return;
        }
        Character1.BeginControlling();
        Character2.StopControlling();
    }
    private void Start()
    {
        SwitchCharacters();
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
