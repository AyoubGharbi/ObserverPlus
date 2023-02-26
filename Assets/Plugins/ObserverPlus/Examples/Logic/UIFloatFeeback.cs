using TMPro;
using UnityEngine;

namespace ObserverPlus.Examples
{
    /// <summary>
    /// Display float value changed.
    /// </summary>
    public class UIFloatFeeback : MonoBehaviour
    {
        [field:SerializeField] public TextMeshProUGUI FloatFeedbackText { get; set; }

        public void OnFloatChanged(float floatValue, float prevValue)
        {
            FloatFeedbackText.text = $"Prev {prevValue}, Val {floatValue}";
        }

        public void OnDoubleChanged(double doubleValue, double prevValue)
        {
            FloatFeedbackText.text = doubleValue.ToString();
        }

        public void OnSomethingChanged()
        {
            FloatFeedbackText.text = "Something's Changed!";
        }
    }
}