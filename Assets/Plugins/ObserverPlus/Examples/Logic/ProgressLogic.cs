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

namespace ObserverPlus.Examples
{
    /// <summary>
    /// Tap on screen to change float value.
    /// </summary>
    public class ProgressLogic : MonoBehaviour
    {
        [field: SerializeField] public FloatEvent FloatEvent { get; set; }

        private float _initialValue = default;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _initialValue = Mathf.Clamp01(_initialValue + 0.04f);
                FloatEvent.Raise(_initialValue);
            }

            if (Input.GetMouseButtonDown(1))
            {
                _initialValue = Mathf.Clamp01(_initialValue - 0.04f);
                FloatEvent.Raise(_initialValue);
            }
        }
    }
}