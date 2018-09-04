/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using UnityEditor;
using UnityEngine;

namespace TPFramework.Unity.Editor
{
    [CustomEditor(typeof(TPItemDatabase))]
    public class TPItemDatabaseEditor : TPScriptlessEditor<TPItemDatabase>
    {
        private readonly Vector2 errLineOffset = new Vector2(7, 0);
        private readonly Vector2 errSize = new Vector2(7, 15);
        private GUIStyle redBoxStyle;
        private bool showError;
        private SerializedProperty databaseArray;

        private void OnEnable()
        {
            databaseArray = serializedObject.FindProperty("itemDatabase");
        }

        public override void OnInspectorGUI()
        {
            if (redBoxStyle == null)
            {
                redBoxStyle = new GUIStyle(GUI.skin.box);
                redBoxStyle.normal.background = TPEditorTextures.RedTexture;
            }
            serializedObject.UpdateIfRequiredOrScript();

            TPEditorGUI.OnButton("Load All TPItems", LoadItemDatabase);
            TPEditorGUI.OnButton("Add new TPItem", AddNewItem);

            if (showError)
            {
                DrawErrorMessage(GUILayoutUtility.GetLastRect(), 1);
                showError = false;
            }
            int length = databaseArray.arraySize;
            for (int i = 0; i < length; i++)
            {
                SerializedProperty arrayElement = databaseArray.GetArrayElementAtIndex(i);
                EditorGUILayout.PropertyField(arrayElement);
                if (arrayElement != null && HasAnySameKeyValue(arrayElement, i))
                {
                    DrawRedBox(GUILayoutUtility.GetLastRect());
                    showError = true;
                }
            }
            serializedObject.ApplyModifiedProperties();
        }

        private void AddNewItem()
        {
            int lastIndex = databaseArray.arraySize;
            databaseArray.arraySize = lastIndex + 1;
            databaseArray.GetArrayElementAtIndex(lastIndex).objectReferenceValue = null;
        }

        private void DrawRedBox(Rect position)
        {
            float oldWidth = EditorGUIUtility.labelWidth;
            Rect iconRect = new Rect(position.position - errLineOffset, errSize);
            EditorGUIUtility.labelWidth = 50;
            GUI.Label(iconRect, UnityEngine.GUIContent.none, redBoxStyle); // hack for drawing red error box without flickering or losing focus
            EditorGUIUtility.labelWidth = oldWidth;
            showError = true;
        }

        private void DrawErrorMessage(Rect rect, int nameLength)
        {
            Vector2 offsetByName = new Vector2(nameLength * 8.25f, 0);
            Vector2 size = new Vector2(rect.size.x, 17);
            EditorGUI.HelpBox(new Rect(rect.position + offsetByName, size), "You have duplicated TPItem IDs, some changes can be lost!", MessageType.Error);
        }

        private bool HasAnySameKeyValue(SerializedProperty key1, int actualIndex)
        {
            return true; // FIXME: fix null exception 
            int length = databaseArray.arraySize;
            for (int i = 0; i < length; i++)
            {
                if (i == actualIndex)
                {
                    continue;
                }

                SerializedProperty key2 = databaseArray.GetArrayElementAtIndex(i);
                if (key2 != null && key1.GetValue().Equals(key2.GetValue()))
                {
                    return true;
                }
            }
            return false;
        }

        private void LoadItemDatabase()
        {
            TPItemHolder[] holders = TPEditorHelper.FindAssetsByType<TPItemHolder>();
            Target.InitDatabase(holders);
            EditorUtility.SetDirty(Target);
        }
    }
}
