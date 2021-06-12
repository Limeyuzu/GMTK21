using System.Collections;
using UnityEngine;

public class MusicLooper : MonoBehaviour
{
    [SerializeField] AudioSource IntroClip;
    [SerializeField] AudioSource LoopClip;

    // Start is called before the first frame update
    void Start()
    {
        PlayIntro();

        StartCoroutine(TransitionAfter(IntroClip.clip.length));
    }

    private void PlayIntro()
    {
        IntroClip.Play();
    }

    private void Update()
    {
        // Debug.Log("Intro is playing: " + IntroClip.isPlaying);
        // Debug.Log("Loop is playing: " + LoopClip.isPlaying);
    }

    private IEnumerator TransitionAfter(float delay)
    {
        yield return new WaitForSeconds(delay);
        IntroClip.Stop();
        LoopClip.Play();
    }
}
