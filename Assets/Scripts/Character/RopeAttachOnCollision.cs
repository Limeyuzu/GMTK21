using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Generic.Event;
[RequireComponent(typeof(Collider2D))]
public class RopeAttachOnCollision : MonoBehaviour
{
    private PlayerCharacter _playerCharacter;

    private void Start()
    {
        _playerCharacter = GetComponent<PlayerCharacter>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_playerCharacter)
        {
            return;
        }

        _playerCharacter.AttachSelfToRope();
        _playerCharacter.ToggleRopeAnchor(_playerCharacter.HasControl());
    }
}
