using Assets.Scripts.Generic.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public GameObject ParticleBurst;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 0)
        {
            Instantiate(ParticleBurst, transform.position, Quaternion.identity);
            CollectibleTracker.IncrementCollectibles();
            EventManager.Emit(GameEvent.CollectCoin);
            Destroy(gameObject);
        }
    }
}
