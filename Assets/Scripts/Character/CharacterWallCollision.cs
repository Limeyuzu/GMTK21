using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
public class CharacterWallCollision : MonoBehaviour
{
    public PlayerCharacter PlayerCharacter;
    public Rigidbody2D Rigidbody2D;
    public int UpForce;

    private IRope _rope;
    private void OnCollisionStay2D(Collision2D collision)
    {
        var distance = Vector3.Distance(transform.position, collision.transform.position);
        if (distance > 0 && !PlayerCharacter.HasControl() && _rope.MaxLengthReached())
        {
            Rigidbody2D.AddForce(Vector2.up * UpForce);
        }
    }

    private void Start()
    {
        PlayerCharacter = GetComponent<PlayerCharacter>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        _rope = GameObjectInstanceManager.GetPlayerRope();
    }
}
