/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using TPFramework.Core;
using UnityEngine;

namespace TPFramework.Unity
{
    public class TPInventory : Core.TPInventory, ISerializationCallbackReceiver
    {
        [SerializeField] private ITPItemSlot[] itemSlots;
        [SerializeField] private ITPEquipSlot[] equipSlots;

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            equipSlots = EquipSlots;
            itemSlots = ItemSlots;
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            EquipSlots = equipSlots;
            ItemSlots = itemSlots;
        }
    }
}
