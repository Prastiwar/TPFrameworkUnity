/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System;
using UnityEditor;
using System.Runtime.CompilerServices;

namespace TPFramework.Unity.Editor
{
    public class TPScriptlessEditor<T> : TPEditor<T> where T : UnityEngine.Object
    {
        private string[] excluders = new string[] { TPEditorHelper.InspectorScriptField };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ExcludeProperty(string property)
        {
            int newIndex = excluders.Length;
            Array.Resize(ref excluders, newIndex + 1);
            excluders[newIndex] = property;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void OnInspectorGUI()
        {
            serializedObject.UpdateIfRequiredOrScript();

            DrawPropertiesExcluding(serializedObject, excluders);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
