using UnityEngine;
using Assets.Scripts.Generic.Event;
using System.Collections.Generic;

namespace Assets.Scripts.Generic.Audio
{
    [System.Serializable]
    public class AudioObject : ScriptableObject
    {
        public List<AudioClip> Clips;
        public GameEvent TriggerEventKey;
        public int Priority = 128;
        public float Volume = 1;
        public float Pitch = 1;
        public float StereoPan = 0;
        public float SpatialBlend = 0;
        public float ReverbZoneMix = 1;

        public void FillAudioObject(List<AudioClip> _Clips, GameEvent _TriggerEventKey, int _Priority, float _Volume, float _Pitch, float _StereoPan, float _SpatialBlend, float _ReverbZoneMix)
        {
            Clips = _Clips;
            TriggerEventKey = _TriggerEventKey;
            Priority = _Priority;
            Volume = _Volume;
            Pitch = _Pitch;
            StereoPan = _StereoPan;
            SpatialBlend = _SpatialBlend;
            ReverbZoneMix = _ReverbZoneMix;
        }
    }
}
