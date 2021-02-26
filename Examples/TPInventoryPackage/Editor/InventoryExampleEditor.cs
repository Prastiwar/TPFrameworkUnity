using TP.Framework.Unity.Editor;
using UnityEditor;

[CustomEditor(typeof(InventoryExample))]
public class InventoryExampleEditor : ScriptlessEditor<InventoryExample>
{
    public override void OnInspectorGUI()
    {
        TP.Framework.Unity.Editor.EditorGUI.OnButton("Spawn Slots", Target.SpawnSlots);
        base.OnInspectorGUI();
    }
}
