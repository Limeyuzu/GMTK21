using Assets.Scripts.Generic.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallStalagtite : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerCharacter>())
        {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            EventManager.Emit(GameEvent.StalactiteFall);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Character>())
        {
            collision.gameObject.GetComponent<Character>().Kill();
        }
        EventManager.Emit(GameEvent.StalactiteCrashSmall);
        Destroy(gameObject);
    }
}
