using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour
{
    public int MoveSpeed;

    public Rigidbody2D Rigidbody;
    protected Animator Animator;

    public Image DeathFadeOut;

    public void Move(Vector2 Direction)
    {
        Vector2 NewDir = new Vector2(Direction.x * MoveSpeed, Rigidbody.velocity.y);
        Rigidbody.velocity = NewDir;
        if(Direction == Vector2.left)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
        if (Direction == Vector2.right)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }

        if (Animator)
        {
            Animator.SetBool("IsMoving", Mathf.Abs(Direction.x) > 0);
        }
    }
    public void Stop()
    {
        Move(Vector2.zero);
    }
    public void Kill()
    {
        StartCoroutine(Die());
    }
    IEnumerator Die()
    {
        Time.timeScale = 0f;
        DeathFadeOut.enabled = true;
        for (float i = 0; i < 5; i += Time.unscaledDeltaTime)
        {
            float Alpha = i / 5;
            Color NewColor = new Color();
            NewColor = Color.black;
            NewColor.a = Alpha;
            DeathFadeOut.color = NewColor;
            yield return null;
        }
        Time.timeScale = 1f;
        FindObjectOfType<SceneHandler>().LoadScene(0);
    }
    protected virtual void Awake()
    {
        //The fact that I actually wrote this hurts me, if I don't fix this by the time this message is seen.
        //I deserve the death sentence - Z
        DeathFadeOut = FindObjectOfType<DeathFadeOut>().GetComponent<Image>();
        Rigidbody = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }
}
