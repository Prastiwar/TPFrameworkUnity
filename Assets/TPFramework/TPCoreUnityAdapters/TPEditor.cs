#if UNITY_EDITOR
using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace TPFramework.Editor
{
    /* ----------------------------------------------------------------------- Editor Common GUI ----------------------------------------------------------------------- */

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

    /* ----------------------------------------------------------------------- Editor Styles ----------------------------------------------------------------------- */

    public static class TPEditorStyles
    {
        public static GUIStyle Toolbar { get { return GUI.skin.FindStyle("Toolbar"); } }
        public static GUIStyle ToolbarSerachField { get { return GUI.skin.FindStyle("ToolbarSeachTextField"); } }
        public static GUIStyle ToolbarSearchCancel { get { return GUI.skin.FindStyle("ToolbarSeachCancelButton"); } }

        private static GUIStyle textWrap;
        public static GUIStyle TextWrap {
            get {
                if (textWrap == null)
                {
                    textWrap = new GUIStyle(GUI.skin.textField) {
                        wordWrap = true
                    };
                }
                return textWrap;
            }
        }

        private static GUIStyle richTextWrap;
        public static GUIStyle RichTextWrap {
            get {
                if (richTextWrap == null)
                {
                    richTextWrap = new GUIStyle(EditorStyles.textField) {
                        richText = true,
                        wordWrap = true
                    };
                }
                return richTextWrap;
            }
        }

        private static GUIStyle richText;
        public static GUIStyle RichText {
            get {
                if (richText == null)
                {
                    richText = new GUIStyle(EditorStyles.textField) {
                        richText = true
                    };
                }
                return richText;
            }
        }

        private static GUIStyle richLabel;
        public static GUIStyle RichLabel {
            get {
                if (richLabel == null)
                {
                    richLabel = new GUIStyle(EditorStyles.label) {
                        richText = true
                    };
                }
                return richLabel;
            }
        }

        private static GUIStyle richLeftButton;
        public static GUIStyle RichLeftButton {
            get {
                if (richLeftButton == null)
                {
                    richLeftButton = new GUIStyle(EditorStyles.miniButtonLeft) {
                        alignment = TextAnchor.MiddleLeft,
                        richText = true
                    };
                }
                return richLeftButton;
            }
        }

        private static GUIStyle richMidButton;
        public static GUIStyle RichMidButton {
            get {
                if (richMidButton == null)
                {
                    richMidButton = new GUIStyle(EditorStyles.miniButtonLeft) {
                        alignment = TextAnchor.MiddleCenter,
                        richText = true
                    };
                }
                return richMidButton;
            }
        }

        private static GUIStyle richRightButton;
        public static GUIStyle RichRightButton {
            get {
                if (richRightButton == null)
                {
                    richRightButton = new GUIStyle(EditorStyles.miniButtonLeft) {
                        alignment = TextAnchor.MiddleRight,
                        richText = true
                    };
                }
                return richRightButton;
            }
        }
    }

    /* ----------------------------------------------------------------------- Editor Common ----------------------------------------------------------------------- */

    public static class TPEditor
    {
        public static string InspectorScriptField { get { return "m_Script"; } }

        public static T[] FindAssetsByType<T>() where T : UnityEngine.Object
        {
            List<T> assets = new List<T>();
            string[] guids = AssetDatabase.FindAssets(string.Format("t:{0}", typeof(T)));
            for (int i = 0; i < guids.Length; i++)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
                T asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);
                if (asset != null)
                {
                    assets.Add(asset);
                }
            }
            return assets.ToArray();
        }
    }

    /* ----------------------------------------------------------------------- Scriptless Editor ----------------------------------------------------------------------- */

    public class TPScriptlessEditor<T> : TPCustomEditor<T> where T : MonoBehaviour
    {
        private string[] excluders = new string[] { TPEditor.InspectorScriptField };

        public void ExcludeProperty(string property)
        {
            int newIndex = excluders.Length;
            Array.Resize(ref excluders, newIndex + 1);
            excluders[newIndex] = property;
        }

        public void ExcludeProperties(params string[] properties)
        {
            int firstNewIndex = excluders.Length;
            int newLength = firstNewIndex + properties.Length;
            Array.Resize(ref excluders, newLength);

            int propIndex = 0;
            for (int i = firstNewIndex; i < newLength; i++)
            {
                excluders[i] = properties[propIndex];
            }
        }

        public override void OnInspectorGUI()
        {
            serializedObject.UpdateIfRequiredOrScript();

            DrawPropertiesExcluding(serializedObject, excluders);

            serializedObject.ApplyModifiedProperties();
        }
    }

    /* ----------------------------------------------------------------------- TP Inspector ----------------------------------------------------------------------- */

    public class TPCustomEditor<T> : UnityEditor.Editor where T : MonoBehaviour
    {
        protected T Target { get { return target as T; } }

        public override void OnInspectorGUI() { base.OnInspectorGUI(); }
    }
}
#endif
