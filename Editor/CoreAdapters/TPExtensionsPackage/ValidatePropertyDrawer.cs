/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using UnityEditor;
using UnityEngine;

namespace TP.Framework.Unity.Editor
{
    public class ValidatePropertyDrawer<TAttribute> : AttributePropertyDrawer<TAttribute>
        where TAttribute : PropertyAttribute
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            UnityEditor.EditorGUI.PropertyField(position, property, label, true);
            Validate();
        }

        protected void ShowError(string message)
        {
            EditorGUILayout.HelpBox(message, MessageType.Error);
        }

        protected void ShowWarning(string message)
        {
            EditorGUILayout.HelpBox(message, MessageType.Warning);
        }

        protected virtual void Validate() { }
    }
}
