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
