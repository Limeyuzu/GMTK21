using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    void Start()
    {
        Reassign(GameObjectInstanceManager.GetPlayer1().transform);
    }

    public void Reassign(Transform characterTransform)
    {
        transform.position = characterTransform.position;
        transform.parent = characterTransform;
    }
}
