using UnityEngine;
using UnityEngine.Events;

namespace ObserverPlus
{
    /// <summary>
    /// Base class for creating event listeners that can listen to events of type AEvent<T>.
    /// </summary>
    /// <typeparam name="T">The type of data that this listener is listening for.</typeparam>
    public abstract class AEventListener<T> : MonoBehaviour
    {
        /// <summary>
        /// The event that this listener is listening to.
        /// </summary>
        [SerializeField] private AEvent<T> _event;

        /// <summary>
        /// The UnityEvent action to execute when the event is raised.
        /// </summary>
        [SerializeField] private UnityEvent<T, T> _unityEventAction = new UnityEvent<T, T>();

        /// <summary>
        /// Register the listener to the event when the listener is enabled.
        /// </summary>
        private void OnEnable()
        {
            _event.RegisterListener(this);
        }

        /// <summary>
        /// Unregister the listener from the event when the listener is disabled.
        /// </summary>
        private void OnDisable()
        {
            _event.UnregisterListener(this);
        }

        /// <summary>
        /// Execute the UnityEvent action when the event is raised.
        /// </summary>
        /// <param name="eventValue">The current value of the event.</param>
        /// <param name="previousValue">The previous value of the event.</param>
        public void OnEventRaised(T eventValue, T previousValue)
        {
            _unityEventAction?.Invoke(eventValue, previousValue);
        }
    }
}
