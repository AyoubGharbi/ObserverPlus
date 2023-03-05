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

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ObserverPlus.Examples
{
    public class CoinsUI : MonoBehaviour
    {
        [field: SerializeField] private Image CoinsVisualImg { get; set; }
        [field: SerializeField] private TextMeshProUGUI CoinsValueTxt { get; set; }

        private const float _transitionTime = 1f;
        private Coroutine _coinsAnimationCrt = default;

        public void OnCoinsValueChanged(int newValue, int previousValue)
        {
            StartTransition(newValue, previousValue);
        }

        private void StartTransition(int newCoinValue, int previousValue)
        {
            if (_coinsAnimationCrt != null)
            {
                StopCoroutine(_coinsAnimationCrt);
            }

            _coinsAnimationCrt = StartCoroutine(TransitionCoroutine(newCoinValue, previousValue));
        }

        private IEnumerator TransitionCoroutine(int newCoinValue, int previousValue)
        {
            float elapsedTime = 0f;

            while (elapsedTime < _transitionTime)
            {
                // Calculate the new value to display
                int displayValue = Mathf.RoundToInt(Mathf.Lerp(previousValue, newCoinValue, elapsedTime / _transitionTime));

                // Update the UI text element
                CoinsValueTxt.text = displayValue.ToString();

                // Wait for the next frame
                yield return null;

                // Update the elapsed time
                elapsedTime += Time.deltaTime;
            }

            // Set the final value
            CoinsValueTxt.text = newCoinValue.ToString();
        }
    }
}