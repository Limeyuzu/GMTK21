using Assets.Scripts.Generic.Scene;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public static SceneHandler Instance;
    public static int FarthestScene = 1;
    private void Awake()
    {
        if (FindObjectsOfType<SceneHandler>().Length > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void LoadScene(int SceneNumber)
    {
        if(SceneNumber > FarthestScene)
        {
            FarthestScene = SceneNumber;
        }
        SceneManager.LoadScene(SceneNumber);
    }

}
