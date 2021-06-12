using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFeet : MonoBehaviour
{
    private int Ground_Touching = 0;

    public GameObject Landing_Burst;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (Ground_Touching == 0)
        {
            Instantiate(Landing_Burst, transform.position, Quaternion.identity);

            if (GetComponentInParent<TrailRenderer>().enabled == true)
            {
                GetComponentInParent<TrailRenderer>().enabled = false;
            }
        }

        Ground_Touching += 1;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        Ground_Touching -= 1;
    }
}
