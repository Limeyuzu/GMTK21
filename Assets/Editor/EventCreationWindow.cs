using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class EventCreationWindow : EditorWindow
{
    string EventName = string.Empty;
    string EventKey = string.Empty;    
    [MenuItem("Tools/Event Creation Window")]
    public static void Open()
    {
        GetWindow<EventCreationWindow>();
    }
    public GameEventBlackboard GameEventBlackboard;
    private void OnGUI()
    {
        GameEventBlackboard = (GameEventBlackboard)EditorGUILayout.ObjectField(GameEventBlackboard, typeof(GameEventBlackboard), true);
        if(GameEventBlackboard == null)
        {
            return;
        }
        GUILayout.Label("Event Name:");
        EventName = GUILayout.TextField(EventName, 20);
        GUILayout.Label("Event Key:");
        EventKey = GUILayout.TextField(EventKey, 20);
        GUILayout.BeginVertical();
        if(GUILayout.Button("Create Event"))
        {
            CreateNewEvent();
        }
        GUILayout.Box("Existing Events",GUILayout.ExpandWidth(true));
        GameEvent EventsToDelete = null;
        string KeyToDelete = null;
        if(GameEventBlackboard.ExistingKeys.Count == 0)
        {
            return;
        }
        foreach (string key in GameEventBlackboard.ExistingKeys)
        {
            GUILayout.BeginHorizontal();
            GameEvent Event = GameEventBlackboard.GetGameEvent(key);
            GUILayout.Label(key);
            GUILayout.Label(Event.name);
            if(GUILayout.Button("Delete Event"))
            {
                KeyToDelete = key;
                EventsToDelete = Event;
                Debug.Log(Event);
            }
            GUILayout.EndHorizontal();
        }     
        if(GUILayout.Button("Clear Keys"))
        {
            ClearKeys();
        }
        GUILayout.EndVertical();
        if (KeyToDelete == null)
        {
            return;
        }
        GameEventBlackboard.RemoveEvent(KeyToDelete);
        if (EventsToDelete == null)
        {
            return;
        }
        AssetDatabase.DeleteAsset("Assets/Scripts/" +
            EventsToDelete.name + ".asset");
    }
    private void ClearKeys()
    {
        //ExistingEvents.Clear();
    }

    private void CreateNewEvent()
    {
        GameEvent NewLayout = ScriptableObject.CreateInstance<GameEvent>();

        string path = "Assets/Scripts/" + EventName + ".asset";
        NewLayout.name = EventName;
        NewLayout.Key = EventKey;
        AssetDatabase.CreateAsset(NewLayout, path);
        EditorUtility.SetDirty(NewLayout);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = NewLayout;
        GameEventBlackboard.AddEvent(NewLayout);
    }
}
