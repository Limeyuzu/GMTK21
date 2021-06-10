using Assets.Scripts;
using UnityEngine;

[System.Serializable]
public class AudioObject : ScriptableObject
{
    public AudioClip Clip;
    public GameEvent TriggerEventKey;
    public int Priority = 128;
    public float Volume = 1;
    public float Pitch = 1;
    public float StereoPan = 0;
    public float SpatialBlend = 0;
    public float ReverbZoneMix = 1;

    public void FillAudioObject(AudioClip _Clip, GameEvent _TriggerEventKey, int _Priority, float _Volume, float _Pitch, float _StereoPan, float _SpatialBlend, float _ReverbZoneMix)
    {
        Clip = _Clip;
        TriggerEventKey = _TriggerEventKey;
        Priority = _Priority;
        Volume = _Volume;
        Pitch = _Pitch;
        StereoPan = _StereoPan;
        SpatialBlend = _SpatialBlend;
        ReverbZoneMix = _ReverbZoneMix;
    }
}
