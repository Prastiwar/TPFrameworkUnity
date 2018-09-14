/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

namespace TP.Framework.Unity.Editor
{
    public static class TPEditorGUI
    {
        public static readonly float fieldHeight = 18;
        public static readonly Vector2 Space  = new Vector2(10, 20);

        public static readonly GUILayoutOption[] Fixed150Width = new GUILayoutOption[] { GUILayout.Width(150), GUILayout.MaxWidth(150), GUILayout.MinWidth(150) };

        public static readonly GUILayoutOption[] Expandable = new GUILayoutOption[] { GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true) };
        public static readonly GUILayoutOption[] ExpandableWidth = new GUILayoutOption[] { GUILayout.ExpandWidth(true) };
        public static readonly GUILayoutOption[] ExpandableHeight = new GUILayoutOption[] { GUILayout.ExpandHeight(true) };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string SearchField(string searchString)
        {
            GUILayout.BeginHorizontal(TPEditorStyles.Toolbar, null);
            {
                searchString = GUILayout.TextField(searchString, TPEditorStyles.ToolbarSerachField, ExpandableWidth);
                OnButton("", () => {
                    searchString = "";
                    GUI.FocusControl(null);
                }, TPEditorStyles.ToolbarSearchCancel, null);
            }
            GUILayout.EndHorizontal();
            return searchString;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool OnButton(string buttonText, Action onClick, GUIStyle buttonStyle = null, params GUILayoutOption[] buttonOptions)
        {
            buttonStyle = buttonStyle ?? GUI.skin.button;
            if (GUILayout.Button(buttonText, buttonStyle, buttonOptions))
            {
                onClick();
                return true;
            }
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void OnEnabledGUI(bool enabled, Action onGuiActive)
        {
            bool wasEnabled = GUI.enabled;
            GUI.enabled = enabled;
            onGuiActive();
            GUI.enabled = wasEnabled;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawField(Rect position, SerializedProperty property, GUIContent label, bool includeChildren = true, bool enabledGui = true)
        {
            bool wasEnabled = GUI.enabled;
            GUI.enabled = enabledGui;
            EditorGUI.PropertyField(position, property, label, includeChildren);
            GUI.enabled = wasEnabled;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawField(this SerializedObject serializedObject, string fieldName, bool includeChildren = true, params GUILayoutOption[] options)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty(fieldName), includeChildren, options);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawField(this SerializedObject serializedObject, string fieldName, GUIContent guiContent, bool includeChildren = true, params GUILayoutOption[] options)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty(fieldName), guiContent, includeChildren, options);
        }

        public static void ScrollView(ref Vector2 scrollPos, Action onScroll, bool alwaysShowHorizontal = false, bool alwaysShowVertical = false, params GUILayoutOption[] options)
        {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, alwaysShowHorizontal, alwaysShowVertical, options);
            {
                onScroll();
            }
            EditorGUILayout.EndScrollView();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StartBox(Color c, params GUILayoutOption[] options)
        {
            var existing = GUI.color;
            GUI.color = c;
            EditorGUILayout.BeginVertical(GUI.skin.box, options);
            GUI.color = existing;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StartBox(params GUILayoutOption[] options)
        {
            EditorGUILayout.BeginVertical(GUI.skin.box, options);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void EndBox()
        {
            EditorGUILayout.EndVertical();
        }
    }
}
