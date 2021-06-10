using Assets.Scripts;
using Assets.Scripts.Generic.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventManager.Emit(GameEvent2.EnumEventOne);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
