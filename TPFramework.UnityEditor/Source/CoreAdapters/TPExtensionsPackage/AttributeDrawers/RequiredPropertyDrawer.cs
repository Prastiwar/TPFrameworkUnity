/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace TP.Framework.Unity.Editor
{
    [CustomPropertyDrawer(typeof(RequiredAttribute))]
    public class RequiredPropertyDrawer : AttributePropertyDrawer<RequiredAttribute>
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            base.OnGUI(position, property, label);
            RequiredAttribute requiredAttribute = (RequiredAttribute)attribute;

            if (property.propertyType == SerializedPropertyType.ObjectReference)
            {
                if (property.objectReferenceValue == null)
                {
                    string errorMessage = property.name + " is required";
                    if (!string.IsNullOrEmpty(requiredAttribute.Message))
                    {
                        errorMessage = requiredAttribute.Message;
                    }

                    EditorGUILayout.HelpBox(errorMessage, MessageType.Error);
                    //Debug.LogError(errorMessage, PropertyUtility.GetTargetObject(property));
                }
            }
            else
            {
                string warning = requiredAttribute.GetType().Name + " works only on reference types";
                EditorGUILayout.HelpBox(warning, MessageType.Warning);
                //Debug.LogWarning(warning, PropertyUtility.GetTargetObject(property));
            }
        }
    }
}
