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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GUIContent GetIcon(UnityEditorIcons icon)
        {
            string iconText = icon.ToString();
            bool hasAtt = Framework.TPExtensions.TryGetCustomAttribute(icon.GetType().GetField(iconText), out StringValueAttribute att);
            return EditorGUIUtility.IconContent(hasAtt ? att.StringValue : iconText);
        }
    }
}
