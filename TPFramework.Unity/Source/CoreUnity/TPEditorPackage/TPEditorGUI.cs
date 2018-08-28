/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace TPFramework.Unity
{
    public static class TPEditorGUI
    {
        public static readonly Vector2 Space  = new Vector2(10, 20);
        public static readonly GUILayoutOption[] Fixed150Width = new GUILayoutOption[] { GUILayout.Width(150), GUILayout.MaxWidth(150), GUILayout.MinWidth(150) };

        public static string SearchField(string searchString)
        {
            GUILayout.BeginHorizontal(TPEditorStyles.Toolbar);
            {
                searchString = GUILayout.TextField(searchString, TPEditorStyles.ToolbarSerachField, GUILayout.ExpandWidth(true));
                OnButton("", () => {
                    searchString = "";
                    GUI.FocusControl(null);
                }, TPEditorStyles.ToolbarSearchCancel);
            }
            GUILayout.EndHorizontal();
            return searchString;
        }

        public static bool OnButton(string buttonText, Action onClick, GUIStyle buttonStyle = null, params GUILayoutOption[] buttonOptions)
        {
            buttonStyle = buttonStyle ?? EditorStyles.miniButtonMid;
            if (GUILayout.Button(buttonText, buttonStyle, buttonOptions))
            {
                onClick();
                return true;
            }
            return false;
        }

        public static bool OnButton(Rect rect, string buttonText, Action onClick, GUIStyle buttonStyle = null)
        {
            buttonStyle = buttonStyle ?? EditorStyles.miniButtonMid;
            if (GUI.Button(rect, buttonText, buttonStyle))
            {
                onClick();
                return true;
            }
            return false;
        }

        public static bool ToggleButton(string buttonText, bool value, Action onToggle, GUIStyle buttonStyle = null, params GUILayoutOption[] buttonOptions)
        {
            buttonStyle = buttonStyle ?? EditorStyles.miniButtonMid;
            if (GUILayout.Button(buttonText, buttonStyle, buttonOptions))
            {
                value = !value;
            }
            else if (value)
            {
                onToggle();
            }
            return value;
        }

        public static bool ToggleButton(Rect rect, string buttonText, bool value, Action onToggle, GUIStyle buttonStyle = null)
        {
            buttonStyle = buttonStyle ?? EditorStyles.miniButtonMid;
            if (GUI.Button(rect, buttonText, buttonStyle))
            {
                value = !value;
            }
            else if (value)
            {
                onToggle();
            }
            return value;
        }

        public static void DrawField(this SerializedObject serializedObject, string fieldName, bool includeChildren = true)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty(fieldName), includeChildren);
        }

        public static void DrawField(this SerializedObject serializedObject, string fieldName, GUIContent guiContent, bool includeChildren = true)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty(fieldName), guiContent, includeChildren);
        }

        public static void StartBox(Color c)
        {
            var existing = GUI.color;
            GUI.color = c;
            EditorGUILayout.BeginVertical(GUI.skin.box);
            GUI.color = existing;
        }

        public static void StartBox()
        {
            EditorGUILayout.BeginVertical(GUI.skin.box);
        }

        public static void EndBox()
        {
            EditorGUILayout.EndVertical();
        }
    }
}
#endif
