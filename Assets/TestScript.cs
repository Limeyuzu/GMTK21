using Assets.Scripts;
using Assets.Scripts.Generic.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField] GameEvent EventToFire;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.Emit(EventToFire);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
