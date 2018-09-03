/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System;
using System.Runtime.CompilerServices;
using TPFramework.Core;
using UnityEngine;

namespace TPFramework.Unity
{
    [Serializable]
    public class TPInventory : TPInventory<TPItemSlot, TPEquipSlot, TPItem>, ISerializationCallbackReceiver
    {
        [SerializeField] private TPItemSlotHolder[] itemSlotHolders;
        [SerializeField] private TPEquipSlotHolder[] equipSlotsHolders;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void InitItemSlots(TPItemSlotHolder[] slotHolders)
        {
            itemSlotHolders = slotHolders;
            if (itemSlotHolders != null)
            {
                int length = slotHolders.Length;
                TPItemSlot[] slots = new TPItemSlot[length];
                for (int i = 0; i < length; i++)
                {
                    slots[i] = slotHolders[i].Slot;
                }
                SetItemSlots(slots);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void InitEquipSlots(TPEquipSlotHolder[] slotHolders)
        {
            equipSlotsHolders = slotHolders;
            if (equipSlotsHolders != null)
            {
                int length = slotHolders.Length;
                TPEquipSlot[] slots = new TPEquipSlot[length];
                for (int i = 0; i < length; i++)
                {
                    slots[i] = slotHolders[i].Slot as TPEquipSlot;
                }
                SetEquipSlots(slots);
            }
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize() { }
        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            if (equipSlotsHolders != null)
            {
                int length = equipSlotsHolders.Length;
                EquipSlots = new TPEquipSlot[length];
                for (int i = 0; i < length; i++)
                {
                    EquipSlots[i] = equipSlotsHolders[i].Slot as TPEquipSlot;
                }
            }
            if (itemSlotHolders != null)
            {
                int length = itemSlotHolders.Length;
                ItemSlots = new TPItemSlot[length];
                for (int i = 0; i < length; i++)
                {
                    ItemSlots[i] = itemSlotHolders[i].Slot;
                }
            }
        }
    }
}
