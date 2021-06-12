using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableRock : Breakable
{
    public override void BreakAction()
    {
        Destroy(gameObject);
    }
}
