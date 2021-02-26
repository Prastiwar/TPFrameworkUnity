/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using UnityEditor;
using UnityEngine;

namespace TP.Framework.Unity.Editor
{
    [CustomPropertyDrawer(typeof(InspectorBackgroundAttribute))]
    public class BackgroundPropertyDrawer : AttributePropertyDrawer<InspectorBackgroundAttribute>
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            UnityEditor.EditorGUI.DrawRect(position, Attribute.Color);
            UnityEditor.EditorGUI.PropertyField(position, property, label, true);
        }
    }
}
