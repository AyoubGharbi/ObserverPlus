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
        [SerializeField] private UnityEvent<T, T> _unityEventAction = new();

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
        public virtual void OnEventRaised(T eventValue, T previousValue)
        {
            _unityEventAction?.Invoke(eventValue, previousValue);
        }
    }
}
