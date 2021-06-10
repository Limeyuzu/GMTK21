using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventSubscriber
{
    public static void SubscribeToEvent(string key, EventListener.ListenerDelegate Delegate)
    {
        GameEvent Event = GameEventBlackboard.Instance.GetGameEvent(key);
        Event.Subscribe(Delegate);
    }
}
