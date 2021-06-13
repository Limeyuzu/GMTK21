using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    public void Play()
    {
        SceneHandler.Instance.LoadScene(1);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
