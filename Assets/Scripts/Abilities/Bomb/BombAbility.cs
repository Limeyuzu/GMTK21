using Assets.Scripts.Generic.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAbility : MonoBehaviour
{
    public GameObject Bomb;
    bool Loaded = true;
    public void SpawnBomb()
    {
        if(Loaded == false)
        {
            return;
        }
        GameObject CurrentBomb = Instantiate(Bomb);
        Vector2 Position = new Vector2(transform.position.x, transform.position.y + .5f);
        CurrentBomb.transform.position = Position;
        CurrentBomb.GetComponent<Bomb>().SetBombAbility(this);
        Loaded = false;
        EventManager.Emit(GameEvent.PulloutBomb);
    }
    public void ReloadBomb()
    {
        Loaded = true;
    }
}
