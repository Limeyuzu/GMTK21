using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public int nextscene;
    public float SceneLoadDelay;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
    IEnumerator LoadScene()
    {
        Time.timeScale = 0;
        for (float i = 0; i < SceneLoadDelay; i+= Time.unscaledTime)
        {
            yield return null;
        }
        Time.timeScale = 1f;
        SceneHandler.Instance.LoadScene(nextscene);
        yield return null;
    }
}
