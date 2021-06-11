using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Generic.Event;
using Assets.Scripts;
public class InputHandler : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            EventManager.Emit(GameEvent.ButtonEPressed);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EventManager.Emit(GameEvent.ButtonSpacePressed);
        }
    }
}
