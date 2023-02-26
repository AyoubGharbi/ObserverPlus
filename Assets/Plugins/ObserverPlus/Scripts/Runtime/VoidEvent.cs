using System.Collections.Generic;
using UnityEngine;

namespace ObserverPlus
{
    /// <summary>
    /// Base class for all scriptable object events that do not pass any data to listeners.
    /// </summary>
    [CreateAssetMenu(menuName = BasePath + nameof(VoidEvent), fileName = nameof(VoidEvent))]
    public class VoidEvent : ScriptableObject
    {
        // The base path for the CreateAssetMenu attribute used by concrete event classes.
        public const string BasePath = "ObserverPlus/Events/";

        // List of listeners that are registered to this event.
        private readonly List<VoidEventListener> _eventListeners = new();
        public IReadOnlyList<VoidEventListener> GetListeners() => _eventListeners;

        /// <summary>
        /// Register a new listener to this event.
        /// </summary>
        /// <param name="listener">The listener to register.</param>
        public void RegisterListener(VoidEventListener listener)
        {
            _eventListeners.Add(listener);

            // Trigger the listener.
            listener.OnEventRaised();
        }

        /// <summary>
        /// Unregister a listener from this event.
        /// </summary>
        /// <param name="listener">The listener to unregister.</param>
        public void UnregisterListener(VoidEventListener listener) => _eventListeners.Remove(listener);

        /// <summary>
        /// Raise the event by calling the OnEventRaised method of each registered listener.
        /// </summary>
        public void Raise()
        {
            for (int i = _eventListeners.Count - 1; i >= 0; i--)
            {
                _eventListeners[i].OnEventRaised();
            }
        }
    }
}
