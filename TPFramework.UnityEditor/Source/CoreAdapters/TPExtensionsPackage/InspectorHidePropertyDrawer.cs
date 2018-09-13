/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

namespace TP.Framework.Unity.Editor
{
    [CustomPropertyDrawer(typeof(InspectorHideAttribute))]
    public class InspectorHidePropertyDrawer : PropertyDrawer
    {
        private readonly BindingFlags findValueFlags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;
        private bool isDrawerEnabled = false;

        private SerializedObject serializedObject;
        private InspectorHideAttribute hideAtt;
        private Action<Rect, GUIContent> OnGUIActive;
        private bool propEnabled = false;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (hideAtt.DisableOnly || propEnabled)
            {
                TPEditorGUI.DrawField(position, property, label, true, propEnabled);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!isDrawerEnabled)
            {
                isDrawerEnabled = true;
                OnEnable(property);
            }
            propEnabled = GetResultFromAttribute(hideAtt, property);
            return hideAtt.DisableOnly || propEnabled
                ? EditorGUI.GetPropertyHeight(property, label)
                : -EditorGUIUtility.standardVerticalSpacing;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void OnEnable(SerializedProperty property)
        {
            serializedObject = property.serializedObject;
            hideAtt = (InspectorHideAttribute)attribute;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool GetResultFromAttribute(InspectorHideAttribute hideAtt, SerializedProperty property)
        {
            bool shouldEnable = GetResultFromConditions(hideAtt.TrueConditions, true) && GetResultFromConditions(hideAtt.FalseConditions, false);
            return shouldEnable;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool GetResultFromConditions(string[] conditions, bool getBool)
        {
            int length = conditions.Length;
            for (int i = 0; i < length; i++)
            {
                bool matchBool = GetBool(conditions[i]);
                if (matchBool != getBool)
                {
                    return false;
                }
            }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool GetBool(string condition)
        {
            Type classType = serializedObject.targetObject.GetType();

            SerializedProperty property = serializedObject.FindProperty(condition);
            if (property != null)
            {
                return GetBool(property);
            }

            MethodInfo method = classType.GetMethod(condition, findValueFlags);
            if (method != null)
            {
                object methodValue = method.Invoke(serializedObject.targetObject, null);
                return GetBool(methodValue);
            }

            FieldInfo field = classType.GetField(condition, findValueFlags);
            if (field != null)
            {
                object fieldValue = field.GetValue(serializedObject.targetObject);
                return GetBool(fieldValue);
            }

            Debug.LogError($"Your condition in InspectorHideAttribute is invalid! Class: {classType}");
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool GetBool(SerializedProperty property)
        {
            switch (property.propertyType)
            {
                case SerializedPropertyType.Boolean:
                    return property.boolValue;
                case SerializedPropertyType.ObjectReference:
                    return property.objectReferenceValue != null;
                default:
                    Debug.LogError("Data type of the property used for InspectorHide (" + property.propertyType + ") is currently not supported");
                    return true;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool GetBool(object value)
        {
            return value is bool
                ? (bool)value
                : value as UnityEngine.Object != null;
        }
    }
}
