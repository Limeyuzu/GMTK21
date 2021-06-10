using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GameEvent : ScriptableObject
{
    public string Key;
    public List<EventListener.ListenerDelegate> Listeners = new List<EventListener.ListenerDelegate>();
    public void Subscribe(EventListener.ListenerDelegate Delegate)
    {
        if(Listeners.Contains(Delegate) == true)
        {
            return;
        }
        Listeners.Add(Delegate);
    }
    public void Unsubscribe(EventListener.ListenerDelegate Delegate)
    {
        if (Listeners.Contains(Delegate) == false)
        {
            return;
        }
        Listeners.Remove(Delegate);
    }
    public void Trigger()
    {
        foreach (EventListener.ListenerDelegate Delegate in Listeners)
        {
            Delegate();
        }
    }
}
