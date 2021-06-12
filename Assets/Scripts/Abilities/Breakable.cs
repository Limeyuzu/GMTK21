using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Breakable : MonoBehaviour
{
    public void Break()
    {
        BreakAction();
    }
    public abstract void BreakAction();
}
