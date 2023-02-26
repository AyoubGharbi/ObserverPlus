using UnityEngine;

namespace ObserverPlus
{
    /// <summary>
    /// A scriptable object event that does not pass any data to its listeners.
    /// </summary>
    [CreateAssetMenu(menuName = BasePath + nameof(NoParamsEvent), fileName = nameof(NoParamsEvent))]
    public class NoParamsEvent : AEvent<object> { }
}