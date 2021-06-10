using UnityEngine;
using UnityEditor;
using Assets.Scripts;

public class AudioObjectCreationWindow : EditorWindow
{
    GameEvent2 selected;
    string ObjectName = string.Empty;
    AudioClip Clip;
    int Priority = 128;
    float Volume = 1;
    float Pitch = 1;
    float StereoPan = 0;
    float SpatialBlend = 0;
    float ReverbZoneMix = 1;

    private readonly string _outputFilePathFormat = "Assets/Scripts/{0}.asset";

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
        GUILayout.Label("Audio Clip");
        Clip = (AudioClip)EditorGUILayout.ObjectField(Clip,typeof(AudioClip),true);
        selected = (GameEvent2)EditorGUILayout.EnumPopup("Event Trigger Options",selected);
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

    private void CreateAudioObject()
    {
        AudioObject NewObject = ScriptableObject.CreateInstance<AudioObject>();
        NewObject.name = ObjectName;
        var path = string.Format(_outputFilePathFormat, NewObject);
        NewObject.FillAudioObject(Clip, selected, Priority, Volume, Pitch, StereoPan, SpatialBlend, ReverbZoneMix);
        AssetDatabase.CreateAsset(NewObject, path);
        EditorUtility.SetDirty(NewObject);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = NewObject;
    }
}
