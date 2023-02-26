using System.Collections.Generic;
using UnityEngine;

namespace ObserverPlus
{
    /// <summary>
    /// Base class for all scriptable object events that pass data of type <typeparamref name="T"/> to its listeners.
    /// </summary>
    /// <typeparam name="T">The type of data that this event will pass to listeners.</typeparam>
    public abstract class AEvent<T> : ScriptableObject
    {
        // The base path for the CreateAssetMenu attribute used by concrete event classes.
        public const string BasePath = "ObserverPlus/Events/";

        // List of listeners that are registered to this event.
        private readonly List<AEventListener<T>> _eventListeners = new();
        public IReadOnlyList<AEventListener<T>> GetListeners() => _eventListeners;

        // The previous value of the event.
        private T _previousValue = default;

        /// <summary>
        /// Register a new listener to this event.
        /// </summary>
        /// <param name="listener">The listener to register.</param>
        public void RegisterListener(AEventListener<T> listener)
        {
            _eventListeners.Add(listener);

            // Trigger the listener with the previous value of the event.
            listener.OnEventRaised(_previousValue, default);
        }

        /// <summary>
        /// Unregister a listener from this event.
        /// </summary>
        /// <param name="listener">The listener to unregister.</param>
        public void UnregisterListener(AEventListener<T> listener) => _eventListeners.Remove(listener);


        /// <summary>
        /// Raise the event by calling the OnEventRaised method of each registered listener.
        /// </summary>
        /// <param name="eventValue">The value to pass to the listeners.</param>
        public void Raise(T eventValue)
        {
            for (int i = GetListeners().Count - 1; i >= 0; i--)
            {
                if (GetListeners()[i] is AEventListener<T> listener)
                {
                    listener.OnEventRaised(eventValue, _previousValue);
                }
            }

            // Save the current value as the previous value.
            _previousValue = eventValue;
        }
    }
}
