using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFeet : MonoBehaviour
{
    private int Ground_Touching = 0;
    private bool _onGround;

    public GameObject Landing_Burst;

    public bool IsTouchingGround()
    {
        return _onGround;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (Ground_Touching == 0)
        {
            if (collision.gameObject.layer == 6)
            {
                Instantiate(Landing_Burst, transform.position, transform.rotation);
            }

            if (GetComponentInParent<TrailRenderer>().enabled == true)
            {
                GetComponentInParent<TrailRenderer>().enabled = false;
            }
        }

        Ground_Touching += 1;
        _onGround = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        Ground_Touching -= 1;
        _onGround = false;
    }
}
