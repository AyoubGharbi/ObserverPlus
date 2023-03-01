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

using NUnit.Framework;
using System.Linq;
using UnityEngine;

namespace ObserverPlus.Tests
{
    public class AEventTests
    {
        [Test]
        public void RegisterListener_AddsListenerToList()
        {
            var eventObject = ScriptableObject.CreateInstance<TestEvent>();
            var listenerObject = new GameObject().AddComponent<TestEventListener>();

            eventObject.RegisterListener(listenerObject);

            Assert.Contains(listenerObject, (System.Collections.ICollection)eventObject.GetListeners());
        }

        [Test]
        public void UnregisterListener_RemovesListenerFromList()
        {
            var eventObject = ScriptableObject.CreateInstance<TestEvent>();
            var listenerObject = new GameObject().AddComponent<TestEventListener>();

            eventObject.RegisterListener(listenerObject);
            eventObject.UnregisterListener(listenerObject);

            Assert.IsFalse(eventObject.GetListeners().Contains(listenerObject));
        }

        [Test]
        public void Raise_CallsOnEventRaisedForAllListeners()
        {
            var eventObject = ScriptableObject.CreateInstance<TestEvent>();
            var listenerObject1 = new GameObject().AddComponent<TestEventListener>();
            var listenerObject2 = new GameObject().AddComponent<TestEventListener>();
            
            eventObject.RegisterListener(listenerObject1);
            eventObject.RegisterListener(listenerObject2);

            eventObject.Raise(10);

            Assert.AreEqual(10, listenerObject1.EventValue);
            Assert.AreEqual(10, listenerObject2.EventValue);
        }

        private class TestEventListener : AEventListener<int>
        {
            public int EventValue { get; private set; }

            public override void OnEventRaised(int eventValue, int previousValue)
            {
                EventValue = eventValue;
                base.OnEventRaised(eventValue, previousValue);
            }
        }

        private class TestEvent : AEvent<int> { }
    }
}
