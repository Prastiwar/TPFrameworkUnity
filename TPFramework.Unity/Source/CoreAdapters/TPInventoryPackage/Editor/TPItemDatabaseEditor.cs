/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using UnityEditor;

namespace TPFramework.Unity.Editor
{
    [CustomEditor(typeof(TPItemDatabase))]
    public class TPItemDatabaseEditor : TPScriptlessEditor<TPItemDatabase>
    {
        public override void OnInspectorGUI()
        {
            serializedObject.UpdateIfRequiredOrScript();
            TPEditorGUI.OnButton("Load All TPItems", LoadItemDatabase);
            base.OnInspectorGUI();
            serializedObject.ApplyModifiedProperties();
        }

        private void LoadItemDatabase()
        {
            TPItemHolder[] holders = TPEditorHelper.FindAssetsByType<TPItemHolder>();
            Target.InitDatabase(holders);
            EditorUtility.SetDirty(Target);
        }
    }
}
