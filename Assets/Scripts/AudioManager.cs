using Assets.Scripts.Generic.Event;
using System.Collections.Generic;
using UnityEngine;

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
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.clip = audioObject.Clip;
        source.priority = audioObject.Priority;
        source.volume = audioObject.Volume;
        source.pitch = audioObject.Pitch;
        source.panStereo = audioObject.StereoPan;
        source.spatialBlend = audioObject.SpatialBlend;
        source.reverbZoneMix = audioObject.ReverbZoneMix;

        EventManager.Subscribe(audioObject.TriggerEventKey, _ => source.Play());
    }
}
