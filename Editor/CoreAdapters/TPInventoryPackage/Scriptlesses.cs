/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using UnityEditor;

namespace TP.Framework.Unity.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(ItemScriptable))]
    public class TPItemHolderEditor : ScriptlessEditor<ItemScriptable> { }

    [CanEditMultipleObjects]
    [CustomEditor(typeof(ItemSlotHoldBehaviour))]
    public class TPItemSlotHolderEditor : ScriptlessEditor<ItemSlotHoldBehaviour> { }

    [CanEditMultipleObjects]
    [CustomEditor(typeof(EquipSlotHoldBehaviour))]
    public class TPEquipSlotHolderEditor : ScriptlessEditor<EquipSlotHoldBehaviour> { }

    [CanEditMultipleObjects]
    [CustomEditor(typeof(SlotsSpawnBehaviour))]
    public class TPSlotsSpawnerHolderEditor : ScriptlessEditor<SlotsSpawnBehaviour> { }
}
