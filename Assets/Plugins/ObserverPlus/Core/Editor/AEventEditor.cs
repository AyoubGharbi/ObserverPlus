/*
 * Copyright (c) 2023 Ayoub Gharbi
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

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
