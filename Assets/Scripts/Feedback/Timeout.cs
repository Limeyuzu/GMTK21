using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timeout : MonoBehaviour
{
    private float Timer = 0.0f;
    public float End_At;

    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer >= End_At)
        {
            Destroy(gameObject);
        }
    }
}
