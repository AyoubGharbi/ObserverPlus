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
