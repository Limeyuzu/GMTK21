using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Generic.Scene
{
    /// <summary>
    /// A helper class for scene transitions.
    /// </summary>
    public class SceneLoader : MonoBehaviour 
    {
        public static void LoadStartScene()
        {
            SceneManager.LoadScene(0);
            FindObjectOfType<GlobalSingleton>().ResetState();
        }

        public static IEnumerator LoadEndSceneCoroutine(float delaySeconds)
        {
            yield return new WaitForSeconds(delaySeconds);
            var lastSceneIndex = SceneManager.sceneCountInBuildSettings - 1;
            SceneManager.LoadScene(lastSceneIndex);
        }

        public static void LoadNextScene()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex + 1);
        }

        public static IEnumerator LoadNextSceneCoroutine(float delay = 0)
        {
            yield return new WaitForSeconds(delay);
            LoadNextScene();
        }

        public static void QuitGame()
        {
            Application.Quit();
        }
    }
}