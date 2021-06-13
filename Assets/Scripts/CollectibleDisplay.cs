using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectibleDisplay : MonoBehaviour
{
    public TMP_Text Display;
    void Update()
    {
        string Text = "X " + CollectibleTracker.collectibles.ToString();
        Display.text = Text;
    }
}
