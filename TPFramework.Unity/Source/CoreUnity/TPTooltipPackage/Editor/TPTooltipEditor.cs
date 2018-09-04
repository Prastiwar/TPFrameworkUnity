#if UNITY_EDITOR
/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

namespace TPFramework.Unity.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(TPTooltip))]
    public class TPTooltipEditor : TPEditor<TPTooltip>
    {
        private bool foldoutLayout = true;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void OnInspectorGUI()
        {
            serializedObject.UpdateIfRequiredOrScript();
            if(targets.Length == 1)
            {
                DrawSingle();
            }
            else
            {
                base.OnInspectorGUI();
            }
            serializedObject.ApplyModifiedProperties();
        }

        private void DrawSingle()
        {
            Target.TooltipType = (TPTooltipType)EditorGUILayout.EnumPopup(GUIContent("Tooltip Type"), Target.TooltipType);
            Target.IsObserving = EditorGUILayout.Toggle(GUIContent("Is Observing?"), Target.IsObserving);

            foldoutLayout = EditorGUILayout.Foldout(foldoutLayout, "Layout Settings");
            if (foldoutLayout)
            {
                Target.TooltipLayout.UIWindowPrefab =
                    (GameObject)EditorGUILayout.ObjectField(GUIContent("Prefab Layout"), Target.TooltipLayout.UIWindowPrefab, typeof(GameObject), false);

                Target.TooltipLayout.UseSharedLayout = EditorGUILayout.Toggle(GUIContent("Should share layout?"), Target.TooltipLayout.UseSharedLayout);

                if (Target.TooltipType.IsDynamic())
                {
                    Target.TooltipLayout.DynamicOffset = EditorGUILayout.Vector2Field(GUIContent("Dynamic Offset"), Target.TooltipLayout.DynamicOffset);
                }
                else
                {
                    Target.TooltipLayout.StaticPosition =
                        (Transform)EditorGUILayout.ObjectField(GUIContent("Static Position"), Target.TooltipLayout.StaticPosition, typeof(Transform), true);
                }
            }
        }
    }
}
#endif