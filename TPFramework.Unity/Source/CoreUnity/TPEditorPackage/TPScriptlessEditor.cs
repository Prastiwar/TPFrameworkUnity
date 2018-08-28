#if UNITY_EDITOR
using System;
using UnityEngine;
using UnityEditor;

namespace TPFramework.Unity.Source.CoreUnity.TPEditor
{
    public class TPScriptlessEditor<T> : TPEditor<T> where T : MonoBehaviour
    {
        private string[] excluders = new string[] { TPEditorHelper.InspectorScriptField };

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
}
#endif
