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
    bool AnchorLock = false;
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
    public void SetAnchorLock(bool set)
    {
        AnchorLock = set;
    }

    public void ControlCharacter1()
    {
        _character1.GiveControl(AnchorLock);
        _cameraTarget.Reassign(_character1.transform);
        _character2.RemoveControl(AnchorLock);
    }

    private void ControlCharacter2()
    {
        _character2.GiveControl(AnchorLock);
        _cameraTarget.Reassign(_character2.transform);
        _character1.RemoveControl(AnchorLock);
    }
    private void Start()
    {
        _character1 = GameObjectInstanceManager.GetPlayer1();
        _character2 = GameObjectInstanceManager.GetPlayer2();
        _cameraTarget = FindObjectOfType<CameraTarget>();

        ControlCharacter1();
    }

    void Update()
    {
        if (Input.GetKeyDown(PlayerControlKeyCodes.SWITCH_CHARACTER))
        {
            SwitchCharacters();
        }
    }
}
