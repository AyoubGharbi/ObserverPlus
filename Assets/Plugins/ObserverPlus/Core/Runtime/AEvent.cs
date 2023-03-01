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
    /// Base class for all scriptable object events that pass data of type <typeparamref name="T"/> to its listeners.
    /// </summary>
    /// <typeparam name="T">The type of data that this event will pass to listeners.</typeparam>
    public abstract class AEvent<T> : ScriptableObject
    {
        // The base path for the CreateAssetMenu attribute used by concrete event classes.
        public const string BasePath = "ObserverPlus/Events/";

        // List of listeners that are registered to this event.
        private readonly List<AEventListener<T>> _eventListeners = new();
        public IReadOnlyList<AEventListener<T>> GetListeners() => _eventListeners;

        // The previous value of the event.
        private T _previousValue = default;

        /// <summary>
        /// Register a new listener to this event.
        /// </summary>
        /// <param name="listener">The listener to register.</param>
        public void RegisterListener(AEventListener<T> listener)
        {
            _eventListeners.Add(listener);

            // Trigger the listener with the previous value of the event.
            listener.OnEventRaised(_previousValue, default);
        }

        /// <summary>
        /// Unregister a listener from this event.
        /// </summary>
        /// <param name="listener">The listener to unregister.</param>
        public void UnregisterListener(AEventListener<T> listener) => _eventListeners.Remove(listener);


        /// <summary>
        /// Raise the event by calling the OnEventRaised method of each registered listener.
        /// </summary>
        /// <param name="eventValue">The value to pass to the listeners.</param>
        public void Raise(T eventValue)
        {
            for (int i = GetListeners().Count - 1; i >= 0; i--)
            {
                if (GetListeners()[i] is AEventListener<T> listener)
                {
                    listener.OnEventRaised(eventValue, _previousValue);
                }
            }

            // Save the current value as the previous value.
            _previousValue = eventValue;
        }
    }
}
