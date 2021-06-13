using Assets.Scripts.Generic.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalagtite : Breakable
{
    Rigidbody2D RB;
    public PolygonCollider2D OffCollider;
    public GameObject Large_Dust_Burst;
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();   
    }
    public override void BreakAction()
    {
        OffCollider.isTrigger = true;
        RB.constraints = RigidbodyConstraints2D.None;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            Instantiate(Large_Dust_Burst, transform.position, Quaternion.identity);
            FindObjectOfType<CameraShake>().Shake_Camera(8.0f, 0.5f);
            EventManager.Emit(GameEvent.StalactiteCrashLarge);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerCharacter>() == true)
        {
            collision.gameObject.GetComponent<PlayerCharacter>().Kill();
        }
    }
}
