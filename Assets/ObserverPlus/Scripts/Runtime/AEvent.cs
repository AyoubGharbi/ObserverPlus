using System.Collections.Generic;
using UnityEngine;

namespace ObserverPlus
{
    /// <summary>
    /// Base class for all scriptable object events.
    /// </summary>
    /// <typeparam name="T">The type of data that this event will pass to listeners.</typeparam>
    public abstract class AEvent<T> : ScriptableObject
    {
        // The base path for the CreateAssetMenu attribute used by concrete event classes.
        public const string BasePath = "ObserverPlus/Events/";

        // List of listeners that are registered to this event.
        private readonly List<AEventListener<T>> _eventListeners = new();
        public List<AEventListener<T>> GetListeners() => _eventListeners;

        // Register a new listener to this event.
        public void RegisterListener(AEventListener<T> listener) => _eventListeners.Add(listener);

        // Unregister a listener from this event.
        public void UnregisterListener(AEventListener<T> listener) => _eventListeners.Remove(listener);

        // Raise the event by calling the OnEventRaised method of each registered listener.
        public void Raise(T eventValue)
        {
            for (int i = _eventListeners.Count - 1; i >= 0; i--)
            {
                _eventListeners[i].OnEventRaised(eventValue);
            }
        }

    }
}
