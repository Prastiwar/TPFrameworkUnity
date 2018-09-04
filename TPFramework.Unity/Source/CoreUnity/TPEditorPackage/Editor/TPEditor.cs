/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using UnityEngine;

namespace TPFramework.Unity.Editor
{
    public class TPEditor<T> : UnityEditor.Editor where T : Object
    {
        private GUIContent tempContent = new GUIContent();

        protected T Target { get { return target as T; } }

        protected T this[int index] { get { return targets[index] as T; } }

        protected GUIContent GUIContent(string text)
        {
            tempContent.text = text;
            return tempContent;
        }

        protected GUIContent GUIContent(Texture image)
        {
            tempContent.image = image;
            return tempContent;
        }

        protected GUIContent GUIContent(string text, Texture image)
        {
            tempContent.text = text;
            tempContent.image = image;
            return tempContent;
        }

        protected GUIContent GUIContent(string text, Texture image, string tooltip)
        {
            tempContent.text = text;
            tempContent.image = image;
            tempContent.tooltip = tooltip;
            return tempContent;
        }

        protected GUIContent GUIContent(string text, string tooltip)
        {
            tempContent.text = text;
            tempContent.tooltip = tooltip;
            return tempContent;
        }

        protected GUIContent GUIContent(Texture image, string tooltip)
        {
            tempContent.image = image;
            tempContent.tooltip = tooltip;
            return tempContent;
        }

        public override void OnInspectorGUI() { base.OnInspectorGUI(); }
    }
}
