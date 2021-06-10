using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Generic.Event
{
    /// <summary>
    /// The global event system which manages event subscriptions and triggers.
    /// This MonoBehaviour should be attached to a persistent GameObject so 
    /// it can still be referenced on scene changes.
    /// </summary>
    public class EventManager : MonoBehaviour
    {
        private Dictionary<GameEvent2, UnityEvent<object>> eventDictionary;

        private static EventManager eventManager;

        /// <summary>
        /// Subscribe to a GameEvent.
        /// </summary>
        /// <param name="eventName">The event name</param>
        /// <param name="listener">The delegate, which may pass one object parameter</param>
        public static void Subscribe(GameEvent2 eventName, UnityAction<object> listener)
        {
            if (Instance.eventDictionary.TryGetValue(eventName, out UnityEvent<object> thisEvent))
            {
                thisEvent.AddListener(listener);
            }
            else
            {
                thisEvent = new UnityEvent<object>();
                thisEvent.AddListener(listener);
                Instance.eventDictionary.Add(eventName, thisEvent);
            }
        }

        /// <summary>
        /// Trigger a GameEvent with an optional parameter.
        /// </summary>
        /// <param name="eventName">The event name</param>
        /// <param name="param">The parameter for this event</param>
        public static void Emit(GameEvent2 eventName, object param = null)
        {
            if (Instance.eventDictionary.TryGetValue(eventName, out UnityEvent<object> thisEvent))
            {
                thisEvent.Invoke(param);
            }
        }

        // No use case for this yet
        //public static void Unsubscribe(GameEvent eventName, UnityAction<object> listener)
        //{
        //    if (eventManager == null) return;
        //    if (Instance.eventDictionary.TryGetValue(eventName, out UnityEvent<object> thisEvent))
        //    {
        //        thisEvent.RemoveListener(listener);
        //    }
        //}

        private static EventManager Instance
        {
            get
            {
                if (!eventManager)
                {
                    eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                    if (!eventManager)
                    {
                        Debug.LogError("There needs to be one active EventManager script on a GameObject in your scene.");
                    }
                    else
                    {
                        eventManager.Init();
                    }
                }

                return eventManager;
            }
        }

        private void Init()
        {
            if (eventDictionary == null)
            {
                eventDictionary = new Dictionary<GameEvent2, UnityEvent<object>>();
            }
        }
    }
}