/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace TP.Framework.Unity.Editor
{
    public class AttributePropertyDrawer<TAttribute> : PropertyDrawer
        where TAttribute : PropertyAttribute
    {
        private bool isEnabled;

        protected readonly BindingFlags findValueFlags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static;

        protected TAttribute Attribute { get; private set; }
        protected SerializedProperty Property { get; private set; }
        protected SerializedObject SerializedObject { get; private set; }
        protected Object TargetObject { get; private set; }
        protected GUIContent PropertyLabel { get; private set; }

        public sealed override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!isEnabled)
            {
                OnEnable(property, label);
            }
            return GetPropHeight();
        }

        protected void OnEnable(SerializedProperty property, GUIContent label)
        {
            isEnabled = true;
            Attribute = (TAttribute)attribute;
            Property = property;
            PropertyLabel = label;
            SerializedObject = property.serializedObject;
            TargetObject = SerializedObject.targetObject;
            OnEnabled();
        }

        public virtual float GetPropHeight()
        {
            return base.GetPropertyHeight(Property, PropertyLabel);
        }

        protected virtual void OnEnabled() { }
    }
}
