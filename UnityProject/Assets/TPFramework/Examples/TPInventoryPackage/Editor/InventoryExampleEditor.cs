using TPFramework.Unity.Editor;
using UnityEditor;

[CustomEditor(typeof(InventoryExample))]
public class InventoryExampleEditor : TPScriptlessEditor<InventoryExample>
{
    public override void OnInspectorGUI()
    {
        TPEditorGUI.OnButton("Spawn Slots", Target.SpawnSlots);
        base.OnInspectorGUI();
    }
}
