using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Generic.Event;
public class CharacterControlManager : MonoBehaviour
{
    private PlayerCharacter _character1;
    private PlayerCharacter _character2;
    private CameraTarget _cameraTarget;
    private IRope _playerRope;

    public void SwitchCharacters()
    {
        //Checks if one character is carrying the other.
        if (_character1.transform.parent != null || _character2.transform.parent != null)
        {
            return;
        }

        EventManager.Emit(GameEvent.CharacterSwitcher);

        if (_character1.HasControl())
        {
            ControlCharacter2();
        } 
        else
        {
            ControlCharacter1();
        }

    }

    private void ControlCharacter1()
    {
        _character1.GiveControl();
        _cameraTarget.Reassign(_character1.transform);
        _playerRope.Anchor(_character1.GetComponent<Rigidbody2D>());

        _character2.RemoveControl();
        _playerRope.Unanchor(_character2.GetComponent<Rigidbody2D>());
    }

    private void ControlCharacter2()
    {
        _character2.GiveControl();
        _cameraTarget.Reassign(_character2.transform);
        _playerRope.Anchor(_character2.GetComponent<Rigidbody2D>());

        _character1.RemoveControl();
        _playerRope.Unanchor(_character1.GetComponent<Rigidbody2D>());
    }

    private void Start()
    {
        _character1 = GameObjectInstanceManager.GetPlayer1();
        _character2 = GameObjectInstanceManager.GetPlayer2();
        _cameraTarget = FindObjectOfType<CameraTarget>();
        _playerRope = GameObjectInstanceManager.GetPlayerRope();

        ControlCharacter1();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwitchCharacters();
        }
    }
}
