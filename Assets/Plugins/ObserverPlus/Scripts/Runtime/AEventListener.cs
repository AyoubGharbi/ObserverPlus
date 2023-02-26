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
        [field: SerializeField] public UnityEvent<T, T> UnityEventAction { get; set; } = new UnityEvent<T, T>();

        // When the listener is enabled, register it to the event and set the previous event value.
        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        // When the listener is disabled, unregister it from the event.
        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }

        // Execute the UnityEvent action when the event is raised.
        public void OnEventRaised(T eventValue, T previousValue)
        {
            UnityEventAction?.Invoke(eventValue, previousValue);
        }
    }
}
