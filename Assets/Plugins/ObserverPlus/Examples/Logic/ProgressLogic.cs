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