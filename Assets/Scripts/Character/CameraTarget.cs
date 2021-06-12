using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    public GameObject Parent_Character;

    // Start is called before the first frame update
    void Start()
    {
        Reassign();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reassign()
    {
        transform.position = Parent_Character.transform.position;
        transform.parent = Parent_Character.transform;
    }
}
