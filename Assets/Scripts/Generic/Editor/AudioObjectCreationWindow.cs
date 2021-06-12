using UnityEngine;
using UnityEditor;
using Assets.Scripts.Generic.Audio;
using Assets.Scripts.Generic.Event;
using System.Collections.Generic;

public class AudioObjectCreationWindow : EditorWindow
{
    GameEvent selected;
    string ObjectName = string.Empty;
    int Priority = 128;
    float Volume = 1;
    float Pitch = 1;
    float StereoPan = 0;
    float SpatialBlend = 0;
    float ReverbZoneMix = 1;

    // This is dumb but Unity doesn't have native support for lists in custom editors
    bool AudioClipRandomize;
    AudioClip Clip1;
    AudioClip Clip2;
    AudioClip Clip3;
    AudioClip Clip4;
    AudioClip Clip5;

    private readonly string _outputFilePathFormat = "Assets/SFX/AudioObjects/{0}.asset";

    [MenuItem("Tools/AudioObject Creation Window")]

    public static void Open()
    {
        GetWindow<AudioObjectCreationWindow>();
    }

    private void OnGUI()
    {
        GUILayout.BeginVertical();
        GUILayout.Label("Object Name:");
        ObjectName = GUILayout.TextField(ObjectName, 20);
        DrawAudioClipField();
        selected = (GameEvent)EditorGUILayout.EnumPopup("Event Trigger Options",selected);
        GUILayout.Label("Audio Priorty:");
        Priority = EditorGUILayout.IntSlider(Priority, 0, 256);
        GUILayout.Label("Audio Volume:");
        Volume = EditorGUILayout.Slider(Volume, 0f, 1f);
        GUILayout.Label("Audio Pitch:");
        Pitch = EditorGUILayout.Slider(Pitch, -3f, 3f);
        GUILayout.Label("Audio Stereo Pan:");
        StereoPan = EditorGUILayout.Slider(StereoPan, -1f, 1f);
        GUILayout.Label("Audio Spatial Blend:");
        SpatialBlend = EditorGUILayout.Slider(SpatialBlend, 0f, 1f);
        GUILayout.Label("Audio Reverb Zone Mix:");
        ReverbZoneMix = EditorGUILayout.Slider(ReverbZoneMix, 0f, 1.1f);
        if(GUILayout.Button("Create Audio Object"))
        {
            CreateAudioObject();
        }
        GUILayout.EndVertical();
    }

    private void DrawAudioClipField()
    {
        GUILayout.Label("Audio Clip");
        AudioClipRandomize = EditorGUILayout.Toggle("Randomize", AudioClipRandomize);

        // This is dumb but Unity doesn't have native support for lists in custom editors
        Clip1 = (AudioClip)EditorGUILayout.ObjectField(Clip1, typeof(AudioClip), true);
        if (AudioClipRandomize)
        {
            Clip2 = (AudioClip)EditorGUILayout.ObjectField(Clip2, typeof(AudioClip), true);
            Clip3 = (AudioClip)EditorGUILayout.ObjectField(Clip3, typeof(AudioClip), true);
            Clip4 = (AudioClip)EditorGUILayout.ObjectField(Clip4, typeof(AudioClip), true);
            Clip5 = (AudioClip)EditorGUILayout.ObjectField(Clip5, typeof(AudioClip), true);
        }
    }

    private List<AudioClip> GetClips()
    {
        return AudioClipRandomize
            ? new List<AudioClip> { Clip1, Clip2, Clip3, Clip4, Clip5 }
            : new List<AudioClip> { Clip1 };
    }

    private void CreateAudioObject()
    {
        AudioObject NewObject = ScriptableObject.CreateInstance<AudioObject>();
        NewObject.name = ObjectName;
        var path = string.Format(_outputFilePathFormat, NewObject);
        NewObject.FillAudioObject(GetClips(), selected, Priority, Volume, Pitch, StereoPan, SpatialBlend, ReverbZoneMix);
        AssetDatabase.CreateAsset(NewObject, path);
        EditorUtility.SetDirty(NewObject);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = NewObject;
    }
}
