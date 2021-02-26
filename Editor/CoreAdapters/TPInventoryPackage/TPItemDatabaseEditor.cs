/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace TP.Framework.Unity.Editor
{
    [CustomEditor(typeof(ItemDatabaseScriptable))]
    public class TPItemDatabaseEditor : TPScriptlessEditor<ItemDatabaseScriptable>
    {
        private readonly Vector2 errLineOffset = new Vector2(20, 0);
        private readonly Vector2 errSize = new Vector2(7, 15);
        private GUIStyle redBoxStyle;
        private bool showError;
        private SerializedProperty databaseArray;
        private ReorderableList rList;

        private void OnEnable()
        {
            databaseArray = serializedObject.FindProperty("itemDatabase");
            rList = new ReorderableList(serializedObject, databaseArray, true, true, true, true) {
                drawHeaderCallback = OnDrawHeader,
                drawElementCallback = DrawElement,
                onAddCallback = OnAdd,
                onRemoveCallback = OnRemove
            };
        }

        private void OnRemove(ReorderableList list)
        {
            int index = list.index;
            list.serializedProperty.DeleteArrayElementAtIndex(index); // sets element to null
            list.serializedProperty.DeleteArrayElementAtIndex(index); // removes element
        }

        private void OnAdd(ReorderableList list)
        {
            int lastIndex = databaseArray.arraySize;
            databaseArray.arraySize = lastIndex + 1;
            databaseArray.GetArrayElementAtIndex(lastIndex).objectReferenceValue = null;
        }

        private void OnDrawHeader(Rect rect)
        {
            EditorGUI.LabelField(rect, $"Item exists in database: ({databaseArray.arraySize} count)");
            rect.position = new Vector2(rect.position.x + 235, rect.position.y);
            rect.size = new Vector2(rect.size.x - 235, rect.size.y - 1);
            TPEditorGUI.OnButton(rect, "Load From Project", LoadItemDatabase);
        }

        private void DrawElement(Rect rect, int index, bool isActive, bool isFocused)
        {
            rect.position = new Vector2(rect.position.x, rect.position.y + 2);
            SerializedProperty arrayElement = databaseArray.GetArrayElementAtIndex(index);
            string itemID = arrayElement.objectReferenceValue != null ? (arrayElement.objectReferenceValue as ItemScriptable).Item.ID.ToString() : "-";
            EditorGUI.LabelField(new Rect(rect.position, new Vector2(110, rect.size.y)), GUIContent($"ItemID: {itemID}"));
            EditorGUI.PropertyField(new Rect(rect.position - new Vector2(-115, 0), rect.size - new Vector2(115, 4)), arrayElement, UnityEngine.GUIContent.none);

            if (databaseArray.HasAnyElementSameValue(arrayElement, index))
            {
                DrawRedBox(new Rect(rect.position - new Vector2(5, 0), rect.size - new Vector2(0, 0)));
                showError = true;
            }
        }

        public override void OnInspectorGUI()
        {
            if (redBoxStyle == null)
            {
                redBoxStyle = new GUIStyle(GUI.skin.box);
                redBoxStyle.normal.background = TPEditorTextures.RedTexture;
            }
            else if (showError)
            {
                DrawErrorMessage(new Rect(new Vector2(29, 25), new Vector2(335, 20)), 1);
                showError = false;
            }
            serializedObject.UpdateIfRequiredOrScript();
            rList.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
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
            EditorGUI.HelpBox(new Rect(rect.position + offsetByName, rect.size), "You have duplicated TPItem IDs, some changes can be lost!", MessageType.Error);
        }

        private void LoadItemDatabase()
        {
            ItemScriptable[] holders = TPEditorHelper.FindAssetsByType<ItemScriptable>();
            Target.InitDatabase(holders);
            EditorUtility.SetDirty(Target);
        }
    }
}
