using ObserverPlus.Editor;
using UnityEditor;

namespace ObserverPlus.Examples.Editor
{
    [CustomEditor(typeof(FloatEvent))]
    public class FloatEventEditor : AEventEditor<float> { }
}