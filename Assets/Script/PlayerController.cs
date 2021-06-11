using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D PlayerRigidbody;
    public float MoveSpeed;
    public void EvaluateInput()
    {
        Vector2 MovementDirection = Vector2.zero;
        if(Input.GetKey(KeyCode.A) == true)
        {
            MovementDirection += Vector2.left;
        }
        if (Input.GetKey(KeyCode.D) == true)
        {
            MovementDirection += Vector2.right;
        }
        ApplyVelocity(MovementDirection);
    }
    public void ApplyVelocity(Vector2 Direction)
    {
        Vector2 VelocityDirection = new Vector2(Direction.x * MoveSpeed, PlayerRigidbody.velocity.y);
        PlayerRigidbody.velocity = VelocityDirection;
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayerRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        EvaluateInput();
    }
}
