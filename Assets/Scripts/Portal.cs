using Assets.Scripts.Generic.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public int nextscene;
    public float SceneLoadDelay;
    private void OnTriggerEnter2D(Collider2D collision)
    {       
        StartCoroutine(LoadScene());
    }
    IEnumerator LoadScene()
    {
        Time.timeScale = 0;
        EventManager.Emit(GameEvent.PortalReached);
        EventManager.Emit(GameEvent.Portal);
        for (float i = 0; i < SceneLoadDelay; i += Time.unscaledDeltaTime)
        {
            yield return null;
        }
        Time.timeScale = 1f;
        SceneHandler.Instance.LoadScene(nextscene);
        yield return null;
    }
}
