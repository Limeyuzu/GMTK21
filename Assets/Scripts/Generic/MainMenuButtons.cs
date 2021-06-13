using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    public void Play()
    {
        SceneHandler.Instance.LoadScene(SceneHandler.FarthestScene);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
