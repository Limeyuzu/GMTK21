using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventBlackboard : MonoBehaviour
{
    public static GameEventBlackboard Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        Instance = this;
    }
    public List<EventStorage> Events = new List<EventStorage>();
    public List<string> ExistingKeys = new List<string>();
    public bool EventListContainsKey(string key)
    {
        foreach (EventStorage eventStorage in Events)
        {
            if(eventStorage.EventKey == key)
            {
                return true;
            }
        }
        return false;
    }
    public GameEvent? GetGameEvent(string key)
    {
        for (int i = 0; i < Events.Count; i++)
        {
            if (Events[i].EventKey == key)
            {
                return Events[i].Event;
            }
        }
        Debug.LogError("Key not contained within Event list");
        return null;
    }
    public void AddEvent(GameEvent NewEvent)
    {
        EventStorage storage = new EventStorage(NewEvent, NewEvent.Key);
        Events.Add(storage);
        ExistingKeys.Add(NewEvent.Key);
    }
    public void TriggerEvent(string key)
    {
        GameEvent? Trigger = GetGameEvent(key);
        if (Trigger != null)
        {
            Trigger.Trigger();
        }
    }
    public void RemoveEvent(string key)
    {
        GameEvent? EventToRemove = GetGameEvent(key);
        if (EventToRemove == null)
        {
            return;
        }
        EventStorage removal = null;
        for (int i = 0; i < Events.Count; i++)
        {
            if (Events[i].EventKey == key)
            {
                removal = Events[i];
                break;
            }
        }
        Events.Remove(removal);
        ExistingKeys.Remove(key);
    }
}
[System.Serializable]
public class EventStorage
{
    public GameEvent Event;
    public string EventKey;
    public EventStorage(GameEvent _event, string _key)
    {
        Event = _event;
        EventKey = _key;
    }
}
