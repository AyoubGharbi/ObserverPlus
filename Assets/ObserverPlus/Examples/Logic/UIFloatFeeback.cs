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

        public void OnFloatChanged(float floatValue)
        {
            FloatFeedbackText.text = floatValue.ToString();
        }

        public void OnDoubleChanged(double doubleValue)
        {
            FloatFeedbackText.text = doubleValue.ToString();
        }
    }
}