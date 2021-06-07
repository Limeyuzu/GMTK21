using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpController : MonoBehaviour
{
    Rigidbody2D PlayerRigidbody;
    private int AvailableJumps = 0;
    public int LandingRechargeAmount = 1;
    public int JumpSpeed;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        AvailableJumps += LandingRechargeAmount;   
    }
    public void Jump()
    {
        if(AvailableJumps == 0)
        {
            return;
        }
        Vector2 VelocityDirection = new Vector2(PlayerRigidbody.velocity.x, JumpSpeed);
        PlayerRigidbody.velocity = VelocityDirection;
        AvailableJumps -= 1;
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayerRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }
}
