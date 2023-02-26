using UnityEngine;

namespace ObserverPlus.Examples
{
    [CreateAssetMenu(menuName = BasePath + nameof(FloatEvent), fileName = nameof(FloatEvent))]
    public class FloatEvent : AEvent<float> { }
}