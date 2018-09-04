using TPFramework.Unity;
using TPFramework.Unity.Editor;
using UnityEditor;

[CustomEditor(typeof(InventoryExample))]
public class InventoryExampleEditor : TPScriptlessEditor<InventoryExample>
{
    public override void OnInspectorGUI()
    {
        TPEditorGUI.OnButton("Spawn Slots", Target.SpawnSlots);
        TPEditorGUI.OnButton("Load ItemDatabase", LoadItemDatabase);
        base.OnInspectorGUI();
    }

    private void LoadItemDatabase()
    {
        TPItemHolder[] holders = TPEditorHelper.FindAssetsByType<TPItemHolder>();
        Target.InitializeDatabase(holders);
    }
}
