/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity 
*/

using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

namespace TP.Framework.Unity.Editor
{
    [CustomEditor(typeof(AudioBundle))]
    public class AudioBundleEditor : UnityEditor.Editor
    {
        private UnityEditorInternal.ReorderableList list;
        private bool isValid;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void OnEnable()
        {
            list = new UnityEditorInternal.ReorderableList(serializedObject, serializedObject.FindProperty("AudioObjects"), true, true, true, true) {
                drawElementCallback = DrawElement,
                onAddCallback = OnAdd,
                drawHeaderCallback = (Rect rect) => { UnityEditor.EditorGUI.LabelField(rect, "Audio Objects in this bundle"); }
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void OnAdd(UnityEditorInternal.ReorderableList reList)
        {
            var index = reList.serializedProperty.arraySize;
            reList.serializedProperty.arraySize++;
            reList.index = index;
            var element = reList.serializedProperty.GetArrayElementAtIndex(index);
            element.FindPropertyRelative("Name").stringValue = "";
            element.FindPropertyRelative("Clip").objectReferenceValue = null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void DrawElement(Rect rect, int index, bool isActive, bool isFocused)
        {
            var element = list.serializedProperty.GetArrayElementAtIndex(index);
            rect.y += 2;
            float halfWidth = rect.width / 2;
            int length = list.serializedProperty.arraySize;

            UnityEditor.EditorGUI.PropertyField(
                new Rect(rect.x, rect.y, halfWidth, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("Name"), GUIContent.none);

            UnityEditor.EditorGUI.PropertyField(
                new Rect(rect.x + halfWidth, rect.y, halfWidth, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("Clip"), GUIContent.none);

            for (int i = 0; i < length; i++)
            {
                if (i == index)
                {
                    continue;
                }

                var otherElement = list.serializedProperty.GetArrayElementAtIndex(i);
                if (otherElement.FindPropertyRelative("Name").stringValue == element.FindPropertyRelative("Name").stringValue)
                {
                    isValid = false;
                    UnityEditor.EditorGUI.DrawRect(new Rect(rect.x - 16, rect.y, 15, EditorGUIUtility.singleLineHeight), Color.red);
                    return;
                }
            }
            isValid = true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void OnInspectorGUI()
        {
            serializedObject.UpdateIfRequiredOrScript();
            list.DoLayoutList();
            if (!isValid)
            {
                if (list.serializedProperty.arraySize < 1)
                {
                    isValid = true;
                }
                EditorGUILayout.HelpBox("You have non unique names in bundle!", MessageType.Error);
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
