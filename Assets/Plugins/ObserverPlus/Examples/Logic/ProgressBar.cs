using UnityEngine;
using UnityEngine.UI;

namespace ObserverPlus.Examples
{
    /// <summary>
    /// A progress bar that displays a value as a fill amount.
    /// </summary>
    public class ProgressBar : MonoBehaviour
    {
        /// <summary>
        /// The Image component that displays the progress bar.
        /// </summary>
        [field: SerializeField] public Image ProgressBarImg { get; set; }

        /// <summary>
        /// Sets the fill amount of the progress bar when a new progress value is received.
        /// </summary>
        /// <param name="progressValue">The new progress value.</param>
        /// <param name="prevValue">The previous progress value.</param>
        public void OnNewProgressBarValue(float progressValue, float prevValue)
        {
            // Update the fill amount of the progress bar.
            ProgressBarImg.fillAmount = progressValue;
        }
    }
}
