/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using UnityEditor;
using UnityEngine;

namespace TP.Framework.Unity.Editor
{
    public class AttributePropertyDrawer<TAttribute> : PropertyDrawer
        where TAttribute : PropertyAttribute
    {
        private bool isEnabled;

        protected TAttribute Attribute { get; private set; }
        protected SerializedObject SerializedObject { get; private set; }

        public sealed override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!isEnabled)
            {
                OnEnable(property);
            }
            return GetPropHeight(property, label);
        }

        public virtual float GetPropHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label);
        }

        protected void OnEnable(SerializedProperty property)
        {
            isEnabled = true;
            Attribute = (TAttribute)attribute;
            SerializedObject = property.serializedObject;
            OnEnabled(property);
        }

        protected virtual void OnEnabled(SerializedProperty property) { }
    }
}
