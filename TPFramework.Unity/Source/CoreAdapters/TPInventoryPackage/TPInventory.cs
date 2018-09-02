/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System;
using UnityEngine;

namespace TPFramework.Unity
{
    [Serializable]
    public class TPInventory : Core.TPInventory<TPItemSlot, TPEquipSlot, Core.TPItem>, ISerializationCallbackReceiver
    {
        [SerializeField] private TPItemSlot[] itemSlots;
        [SerializeField] private TPEquipSlot[] equipSlots;

        public TPInventory(TPEquipSlot[] equipSlots, TPItemSlot[] itemSlots)
        {
            EquipSlots = equipSlots;
            ItemSlots = itemSlots;
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            if (EquipSlots?.Length > equipSlots?.Length)
            {
                equipSlots = EquipSlots;
            }
            if (ItemSlots?.Length > itemSlots?.Length)
            {
                itemSlots = ItemSlots;
            }
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            EquipSlots = equipSlots;
            ItemSlots = itemSlots;
        }
    }
}
