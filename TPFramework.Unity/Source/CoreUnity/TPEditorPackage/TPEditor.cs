/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace TPFramework.Unity
{
    public class TPEditor<T> : Editor where T : MonoBehaviour
    {
        protected T Target { get { return target as T; } }

        public override void OnInspectorGUI() { base.OnInspectorGUI(); }
    }
}
#endif
