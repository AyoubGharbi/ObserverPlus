using UnityEngine;
using UnityEngine.Events;

namespace ObserverPlus
{
    /// <summary>
    /// Base class for all event listeners without parameters.
    /// </summary>
    public class VoidEventListener : MonoBehaviour
    {
        /// <summary>
        /// The event that this listener is listening to.
        /// </summary>
        [field: SerializeField] public VoidEvent Event { get; set; }

        /// <summary>
        /// The UnityEvent action to execute when the event is raised.
        /// </summary>
        [field: SerializeField] public UnityEvent UnityEventAction { get; set; } = new UnityEvent();

        /// <summary>
        /// When the listener is enabled, register it to the event.
        /// </summary>
        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        /// <summary>
        /// When the listener is disabled, unregister it from the event.
        /// </summary>
        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }

        /// <summary>
        /// Execute the UnityEvent action when the event is raised.
        /// </summary>
        public void OnEventRaised()
        {
            UnityEventAction?.Invoke();
        }
    }
}
