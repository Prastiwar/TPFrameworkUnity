/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using UnityEditor;

namespace TPFramework.Unity.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(TPItemHolder))]
    public class TPItemHolderEditor : TPScriptlessEditor<TPItemHolder> { }

    [CanEditMultipleObjects]
    [CustomEditor(typeof(TPItemSlotHolder))]
    public class TPItemSlotHolderEditor : TPScriptlessEditor<TPItemSlotHolder> { }

    [CanEditMultipleObjects]
    [CustomEditor(typeof(TPEquipSlotHolder))]
    public class TPEquipSlotHolderEditor : TPScriptlessEditor<TPEquipSlotHolder> { }

    [CanEditMultipleObjects]
    [CustomEditor(typeof(TPSlotsSpawner))]
    public class TPSlotsSpawnerHolderEditor : TPScriptlessEditor<TPSlotsSpawner> { }
}
