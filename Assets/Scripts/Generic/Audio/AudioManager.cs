using Assets.Scripts.Generic.Event;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Generic.Audio
{
    public class AudioManager : MonoBehaviour
    {
        public List<AudioObject> AudioObjects = new List<AudioObject>();

        private void Awake()
        {
            PopulateSources();
        }

        public void PopulateSources()
        {
            foreach (AudioObject Audio in AudioObjects)
            {
                CreateSource(Audio);
            }
        }

        public void CreateSource(AudioObject audioObject)
        {
            var sources = new List<AudioSource>();
            foreach (var clip in audioObject.Clips)
            {
                AudioSource source = gameObject.AddComponent<AudioSource>();
                source.clip = clip;
                source.priority = audioObject.Priority;
                source.volume = audioObject.Volume;
                source.pitch = audioObject.Pitch;
                source.panStereo = audioObject.StereoPan;
                source.spatialBlend = audioObject.SpatialBlend;
                source.reverbZoneMix = audioObject.ReverbZoneMix;
                source.playOnAwake = false;
                sources.Add(source);
            }

            EventManager.Subscribe(audioObject.TriggerEventKey, _ => {
                var randomSource = sources[Random.Range(0, sources.Count)];
                randomSource.Play();
            });
        }
    }
}