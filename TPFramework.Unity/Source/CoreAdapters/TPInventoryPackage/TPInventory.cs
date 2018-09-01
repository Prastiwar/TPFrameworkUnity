/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System;
using System.Linq;
using TPFramework.Core;
using UnityEngine;

namespace TPFramework.Unity
{
    [Serializable]
    public class TPInventory : Core.TPInventory, ISerializationCallbackReceiver
    {
        [SerializeField] private TPEquipSlot[] equipSlots;
        [SerializeField] private TPItemSlot[] itemSlots;

        public TPInventory(ITPEquipSlot[] equipSlots, ITPItemSlot[] itemSlots)
        {
            EquipSlots = equipSlots;
            ItemSlots = itemSlots;
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            if (EquipSlots?.Length > equipSlots.Length)
            {
                equipSlots = EquipSlots.Cast<TPEquipSlot>().ToArray();
            }
            if (ItemSlots?.Length > itemSlots.Length)
            {
                itemSlots = ItemSlots.Cast<TPItemSlot>().ToArray();
            }
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            if (equipSlots != null)
            {
                EquipSlots = equipSlots.Cast<ITPEquipSlot>().ToArray();
            }
            if (itemSlots != null)
            {
                ItemSlots = itemSlots.Cast<ITPItemSlot>().ToArray();
            }
        }
    }
}
