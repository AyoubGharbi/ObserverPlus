using UnityEngine;

namespace ObserverPlus.Examples
{
    /// <summary>
    /// Tap on screen to change float value.
    /// </summary>
    public class FloatLogic : MonoBehaviour
    {
        [field: SerializeField] public FloatEvent FloatEvent { get; set; }

        private float _initialValue = 0f;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                FloatEvent.Raise(++_initialValue);
            }
        }
    }
}