using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Generic.Audio
{
    /// <summary>
    /// Plays given IntroClip once and then LoopClip infinitely.
    /// </summary>
    public class MusicLooper : MonoBehaviour
    {
        [SerializeField] AudioSource IntroClip;
        [SerializeField] AudioSource LoopClip;
        [SerializeField] float StartLoopDelay = -0.2f;

        // Start is called before the first frame update
        void Start()
        {
            PlayIntro();

            var startLoopAfterSeconds = IntroClip.clip.length + StartLoopDelay;
            StartCoroutine(TransitionAfter(startLoopAfterSeconds));
        }

        private void PlayIntro()
        {
            IntroClip.Play();
        }

        private IEnumerator TransitionAfter(float delay)
        {
            yield return new WaitForSeconds(delay);
            IntroClip.Stop();
            LoopClip.Play();
        }
    }
}
