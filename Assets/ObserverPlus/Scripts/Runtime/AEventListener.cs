using UnityEngine;
using UnityEngine.Events;

namespace ObserverPlus
{
    /// <summary>
    /// Base class for all event listeners.
    /// </summary>
    /// <typeparam name="T">The type of data that this listener is listening for.</typeparam>
    public abstract class AEventListener<T> : MonoBehaviour
    {
        // The event that this listener is listening to.
        [field: SerializeField] public AEvent<T> Event { get; set; }

        // The UnityEvent action to execute when the event is raised.
        [field: SerializeField] public UnityEvent<T> UnityEventAction { get; set; } = new UnityEvent<T>();

        // When the listener is enabled, register it to the event.
        private void OnEnable() => Event.RegisterListener(this);

        // When the listener is disabled, unregister it from the event.
        private void OnDisable() => Event.UnregisterListener(this);

        // Execute the UnityEvent action when the event is raised.
        public void OnEventRaised(T eventValue)
        {
            UnityEventAction?.Invoke(eventValue);
        }
    }
}
