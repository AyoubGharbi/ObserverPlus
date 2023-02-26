using UnityEditor;

namespace ObserverPlus.Editor
{
    [CustomEditor(typeof(AEvent<>))]
    public class AEventEditor<T> : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            AEvent<T> aEvent = (AEvent<T>)target;

            // Display a header for the registered listeners section.
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Registered Listeners", EditorStyles.boldLabel);

            // Get the list of registered listeners for this event.
            var registeredListeners = aEvent.GetListeners();

            // If there are no registered listeners, display a message.
            if (registeredListeners.Count == 0)
            {
                EditorGUILayout.LabelField("No registered listeners.");
            }

            // Otherwise, display the registered listeners as a list.
            else
            {
                foreach (AEventListener<T> listener in registeredListeners)
                {
                    EditorGUILayout.ObjectField(listener, typeof(AEventListener<T>), true);
                }
            }
        }
    }
}
