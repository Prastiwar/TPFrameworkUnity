/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System;
using System.Runtime.CompilerServices;
using UnityEditor;

namespace TPFramework.Unity.Editor
{
    public static partial class TPExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static object GetTargetObject(this SerializedProperty prop)
        {
            object targetObj = prop.serializedObject.targetObject;
            string[] elements = prop.propertyPath.Replace(".Array.data[", "[").Split('.');
            int length = elements.Length;
            for (int i = 0; i < length; i++)
            {
                if (elements[i].Contains("["))
                {
                    string elementName = elements[i].Substring(0, elements[i].IndexOf("["));
                    int index = Convert.ToInt32(elements[i].Substring(elements[i].IndexOf("[")).Replace("[", "").Replace("]", ""));
                    targetObj = Core.TPExtensions.GetValue(targetObj, elementName, index);
                }
                else
                {
                    targetObj = Core.TPExtensions.GetValue(targetObj, elements[i]);
                }
            }
            return targetObj;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static object GetValue(this SerializedProperty prop)
        {
            switch (prop.propertyType)
            {
                case SerializedPropertyType.Integer:
                    return prop.intValue;
                case SerializedPropertyType.Float:
                    return prop.floatValue;
                case SerializedPropertyType.String:
                    return prop.stringValue;
                case SerializedPropertyType.Enum:
                    return prop.enumValueIndex;
                case SerializedPropertyType.Boolean:
                    return prop.boolValue;
                case SerializedPropertyType.Color:
                    return prop.colorValue;
                case SerializedPropertyType.ObjectReference:
                    return prop.objectReferenceValue;
                case SerializedPropertyType.Vector2:
                    return prop.vector2Value;
                case SerializedPropertyType.Vector3:
                    return prop.vector3Value;
                case SerializedPropertyType.Vector4:
                    return prop.vector4Value;
                case SerializedPropertyType.Quaternion:
                    return prop.quaternionValue;
                case SerializedPropertyType.Vector2Int:
                    return prop.vector2IntValue;
                case SerializedPropertyType.Vector3Int:
                    return prop.vector3IntValue;
                case SerializedPropertyType.ExposedReference:
                    return prop.exposedReferenceValue;
                case SerializedPropertyType.ArraySize:
                    return prop.arraySize;
                case SerializedPropertyType.Rect:
                    return prop.rectValue;
                case SerializedPropertyType.RectInt:
                    return prop.rectIntValue;
                case SerializedPropertyType.Bounds:
                    return prop.boundsValue;
                case SerializedPropertyType.BoundsInt:
                    return prop.boundsIntValue;
                case SerializedPropertyType.FixedBufferSize:
                    return prop.fixedBufferSize;
                case SerializedPropertyType.AnimationCurve:
                    return prop.animationCurveValue;
                //case SerializedPropertyType.Generic:
                //    return key.;
                //case SerializedPropertyType.LayerMask:
                //    return key.;
                //case SerializedPropertyType.Character:
                //    return key.;
                //case SerializedPropertyType.Gradient:
                //    return key.;
                default:
                    break;
            }

            string typ = prop.type;
            if (typ == "double")
            {
                return prop.doubleValue;
            }
            else if (typ == "long")
            {
                return prop.longValue;
            }
            return prop.GetTargetObject();
        }
    }
}
