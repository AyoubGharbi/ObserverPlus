using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        if(_coinsAnimationCrt != null)
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
