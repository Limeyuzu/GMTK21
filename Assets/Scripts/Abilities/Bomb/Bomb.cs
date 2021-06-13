using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float ExplodeTime = 5;
    BombAbility bombAbility = null;
    public GameObject Explosion;
    public void SetBombAbility(BombAbility Creator)
    {
        bombAbility = Creator;
    }
    private void Start()
    {
        StartCoroutine(Timer());
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerCharacter>())
        {
            return;
        }
        Explode();

    }
    public void Explode()
    {
        FindObjectOfType<CameraShake>().Shake_Camera(5.0f, 0.25f);
        StopCoroutine(Timer());
        bombAbility.ReloadBomb();
        GameObject ExplosionRef = Instantiate(Explosion);
        ExplosionRef.transform.position = transform.position;
        Destroy(gameObject);
    }
    IEnumerator Timer()
    {
        for (float i = 0; i < ExplodeTime; i += Time.deltaTime)
        {
            yield return null;
        }
        Explode();
    }
}
