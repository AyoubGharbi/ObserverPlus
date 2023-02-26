using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace ObserverPlus.Editor
{
    [CustomEditor(typeof(VoidEvent))]
    public class AEventEditor : UnityEditor.Editor
    {
        private ReorderableList _listenerList;

        private void OnEnable()
        {
            VoidEvent aEvent = (VoidEvent)target;

            _listenerList = new ReorderableList(
                (System.Collections.IList)aEvent.GetListeners(),
                typeof(VoidEventListener), true, true, true, true)
            {
                drawHeaderCallback = (Rect rect) => EditorGUI.LabelField(rect, "Registered Listeners", EditorStyles.boldLabel)
            };

            _listenerList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                VoidEventListener listener = (VoidEventListener)_listenerList.list[index];
                EditorGUI.ObjectField(rect, listener, typeof(VoidEventListener), true);
            };
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            _listenerList.DoLayoutList();
        }
    }

    [CustomEditor(typeof(AEvent<>))]
    public class AEventEditor<T> : UnityEditor.Editor
    {
        private ReorderableList _listenerList;

        private void OnEnable()
        {
            AEvent<T> aEvent = (AEvent<T>)target;

            _listenerList = new ReorderableList(
                (System.Collections.IList)aEvent.GetListeners(),
                typeof(AEventListener<T>), true, true, true, true)
            {
                drawHeaderCallback = (Rect rect) => EditorGUI.LabelField(rect, "Registered Listeners", EditorStyles.boldLabel)
            };

            _listenerList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                AEventListener<T> listener = (AEventListener<T>)_listenerList.list[index];
                EditorGUI.ObjectField(rect, listener, typeof(AEventListener<T>), true);
            };
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            _listenerList.DoLayoutList();
        }
    }
}
