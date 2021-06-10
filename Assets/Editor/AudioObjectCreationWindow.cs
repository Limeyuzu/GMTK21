using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class AudioObjectCreationWindow : EditorWindow
{
    public GameEventBlackboard GameEventBlackboard;
    int selected = 0;
    string ObjectName = string.Empty;
    AudioClip Clip;
    string TriggerEventKey = "";
    int Priority = 128;
    float Volume = 1;
    float Pitch = 1;
    float StereoPan = 0;
   float SpatialBlend = 0;
    float ReverbZoneMix = 1;
    [MenuItem("Tools/AudioObject Creation Window")]
    public static void Open()
    {
        GetWindow<AudioObjectCreationWindow>();
    }
    private void OnGUI()
    {
        GameEventBlackboard = (GameEventBlackboard)EditorGUILayout.ObjectField(GameEventBlackboard, typeof(GameEventBlackboard), true);
        if (GameEventBlackboard == null)
        {
            return;
        }
        GUILayout.BeginVertical();
        GUILayout.Label("Object Name:");
        ObjectName = GUILayout.TextField(ObjectName, 20);
        GUILayout.Label("Audio Clip");
        Clip = (AudioClip)EditorGUILayout.ObjectField(Clip,typeof(AudioClip),true);
        string[] options = new string[GameEventBlackboard.ExistingKeys.Count];
        for (int i = 0; i < options.Length; i++)
        {
            options[i] = GameEventBlackboard.ExistingKeys[i];
        }
        selected = EditorGUILayout.Popup("Event Trigger Options",selected, options);
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
            TriggerEventKey = string.Copy(options[selected]);
            CreateAudioObject();
        }
        GUILayout.EndVertical();
    }

    private void CreateAudioObject()
    {
        AudioObject NewObject = ScriptableObject.CreateInstance<AudioObject>();
        NewObject.name = ObjectName;
        string path = "Assets/Scripts/"
            + NewObject + ".asset";
        NewObject.FillAudioObject(Clip, TriggerEventKey, Priority, Volume, Pitch, StereoPan, SpatialBlend, ReverbZoneMix);
        AssetDatabase.CreateAsset(NewObject, path);
        EditorUtility.SetDirty(NewObject);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = NewObject;
    }
}
