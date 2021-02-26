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
    public class TPItemHolderEditor : TPScriptlessEditor<ItemScriptable> { }

    [CanEditMultipleObjects]
    [CustomEditor(typeof(ItemSlotHoldBehaviour))]
    public class TPItemSlotHolderEditor : TPScriptlessEditor<ItemSlotHoldBehaviour> { }

    [CanEditMultipleObjects]
    [CustomEditor(typeof(EquipSlotHoldBehaviour))]
    public class TPEquipSlotHolderEditor : TPScriptlessEditor<EquipSlotHoldBehaviour> { }

    [CanEditMultipleObjects]
    [CustomEditor(typeof(SlotsSpawnBehaviour))]
    public class TPSlotsSpawnerHolderEditor : TPScriptlessEditor<SlotsSpawnBehaviour> { }
}
