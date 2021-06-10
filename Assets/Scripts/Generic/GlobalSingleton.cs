using UnityEngine;

namespace Assets.Scripts.Generic
{
    /// <summary>
    /// A MonoBehaviour that keeps a GameObject alive on scene changes.
    /// </summary>
    public class GlobalSingleton : MonoBehaviour
    {
        private void Awake()
        {
            if (FindObjectsOfType<GlobalSingleton>().Length > 1)
            {
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
            }
        }

        public void ResetState()
        {
            Destroy(gameObject);
        }
    }
}