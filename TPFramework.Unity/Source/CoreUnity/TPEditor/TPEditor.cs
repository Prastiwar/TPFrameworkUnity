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
