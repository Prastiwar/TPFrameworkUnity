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
    public static class EditorStyles
    {
        public static GUIStyle Toolbar { get { return GUI.skin.FindStyle("Toolbar"); } }
        public static GUIStyle ToolbarSerachField { get { return GUI.skin.FindStyle("ToolbarSeachTextField"); } }
        public static GUIStyle ToolbarSearchCancel { get { return GUI.skin.FindStyle("ToolbarSeachCancelButton"); } }
        
        private static GUIStyle boldText;
        public static GUIStyle BoldText {
            get {
                if (boldText == null)
                {
                    boldText = new GUIStyle(GUI.skin.textField) {
                        fontStyle = FontStyle.Bold,
                    };
                }
                return boldText;
            }
        }

        private static GUIStyle boldCenterText;
        public static GUIStyle BoldCenterText {
            get {
                if (boldCenterText == null)
                {
                    boldCenterText = new GUIStyle(BoldText) {
                        alignment = TextAnchor.MiddleCenter
                    };
                }
                return boldCenterText;
            }
        }

        private static GUIStyle boldLeftText;
        public static GUIStyle BoldLeftText {
            get {
                if (boldLeftText == null)
                {
                    boldLeftText = new GUIStyle(BoldText) {
                        alignment = TextAnchor.MiddleLeft
                    };
                }
                return boldLeftText;
            }
        }

        private static GUIStyle boldRightText;
        public static GUIStyle BoldRightText {
            get {
                if (boldRightText == null)
                {
                    boldRightText = new GUIStyle(BoldText) {
                        alignment = TextAnchor.MiddleRight
                    };
                }
                return boldRightText;
            }
        }

        private static GUIStyle boldLabel;
        public static GUIStyle BoldLabel {
            get {
                if (boldLabel == null)
                {
                    boldLabel = new GUIStyle(GUI.skin.label) {
                        fontStyle = FontStyle.Bold,
                    };
                }
                return boldLabel;
            }
        }

        private static GUIStyle boldCenterLabel;
        public static GUIStyle BoldCenterLabel {
            get {
                if (boldCenterLabel == null)
                {
                    boldCenterLabel = new GUIStyle(BoldLabel) {
                        alignment = TextAnchor.MiddleCenter
                    };
                }
                return boldCenterLabel;
            }
        }

        private static GUIStyle boldLeftLabel;
        public static GUIStyle BoldLeftLabel {
            get {
                if (boldLeftLabel == null)
                {
                    boldLeftLabel = new GUIStyle(BoldLabel) {
                        alignment = TextAnchor.MiddleLeft
                    };
                }
                return boldLeftLabel;
            }
        }

        private static GUIStyle boldRightLabel;
        public static GUIStyle BoldRightLabel {
            get {
                if (boldRightLabel == null)
                {
                    boldRightLabel = new GUIStyle(BoldLabel) {
                        alignment = TextAnchor.MiddleRight
                    };
                }
                return boldRightLabel;
            }
        }

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
                    richTextWrap = new GUIStyle(TextWrap) {
                        richText = true
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
                    richText = new GUIStyle(UnityEditor.EditorStyles.textField) {
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
                    richLabel = new GUIStyle(UnityEditor.EditorStyles.label) {
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
                    richLeftButton = new GUIStyle(UnityEditor.EditorStyles.miniButtonLeft) {
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
                    richMidButton = new GUIStyle(UnityEditor.EditorStyles.miniButtonLeft) {
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
                    richRightButton = new GUIStyle(UnityEditor.EditorStyles.miniButtonLeft) {
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
            bool hasAtt = Framework.Extensions.TryGetCustomAttribute(icon.GetType().GetField(iconText), out StringValueAttribute att);
            return EditorGUIUtility.IconContent(hasAtt ? att.StringValue : iconText);
        }
    }
}
