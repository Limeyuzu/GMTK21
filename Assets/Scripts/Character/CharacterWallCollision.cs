using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
public class CharacterWallCollision : MonoBehaviour
{
    public PlayerCharacter PlayerCharacter;
    public Rope Rope;
    public Rigidbody2D Rigidbody2D;
    public int UpForce;
    private void OnCollisionStay2D(Collision2D collision)
    {
        Vector2 Dir = transform.position - collision.transform.position;
        if (Dir.x > 0 || Dir.x < 0)
        {
            if (PlayerCharacter.HasControl() == false)
            {
                if (Rope.CheckForMaxLength() == true)
                {
                    Rigidbody2D.AddForce(Vector2.up * UpForce);
                }
            }
        }
    }
}
