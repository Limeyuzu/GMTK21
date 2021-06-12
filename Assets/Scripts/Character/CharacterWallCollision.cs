using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
public class CharacterWallCollision : MonoBehaviour
{
    public PlayerCharacter PlayerCharacter;
    public Rope Rope;
    public Rigidbody2D Rigidbody2D;
    private void Update()
    {
        Vector2 Origin = new Vector2(transform.position.x + .5f, transform.position.y);
        RaycastHit2D hit = Physics2D.Raycast(Origin, Vector2.right,1f);
        Debug.DrawRay(Origin, Vector2.right, Color.yellow,1f);
        if (hit)
        {
            if(hit.collider.gameObject != gameObject)
            {
                if(PlayerCharacter.HasControl() == false)
                {
                    if(Rope.CheckForMaxLength() == true)
                    {
                        Debug.Log("Adding");
                        Rigidbody2D.AddForce(Vector2.up * 10);
                    }
                }
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        Vector2 Dir = transform.position - collision.transform.position;
        if (Dir.x > 0)
        {
            //Debug.Log(gameObject);
            if (PlayerCharacter.HasControl() == false)
            {
                Debug.Log(gameObject);
                if (Rope.CheckForMaxLength() == true)
                {
                    Debug.Log("Adding");
                    Rigidbody2D.AddForce(Vector2.up * 100);
                }
            }
        }
    }
}
