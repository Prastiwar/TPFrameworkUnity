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

        public TPItemDatabase ItemDatabase;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void InitItemSlots(TPItemSlotHolder[] slotHolders)
        {
            itemSlotHolders = slotHolders;
            if (itemSlotHolders != null)
            {
                ItemSlots = LoadSlots(ItemSlots, itemSlotHolders);
                SetItemSlots(ItemSlots);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void InitEquipSlots(TPEquipSlotHolder[] slotHolders)
        {
            equipSlotsHolders = slotHolders;
            if (equipSlotsHolders != null)
            {
                EquipSlots = LoadSlots(EquipSlots, equipSlotsHolders);
                SetEquipSlots(EquipSlots);
            }
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize() { }
        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            if (equipSlotsHolders != null)
            {
                equipSlotsHolders = AddOnItemChangedAction(equipSlotsHolders);
                EquipSlots = LoadSlots(EquipSlots, equipSlotsHolders);
            }
            if (itemSlotHolders != null)
            {
                itemSlotHolders = AddOnItemChangedAction(itemSlotHolders);
                ItemSlots = LoadSlots(ItemSlots, itemSlotHolders);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private T[] AddOnItemChangedAction<T>(T[] holder)
            where T : TPItemSlotHolder
        {
            int length = holder.Length;
            for (int i = 0; i < length; i++)
            {
                Debug.Log(holder[i].Slot); // null
                Debug.Log(holder[i].Slot as ITPItemSlot<TPItem>); // null because of null
                TPItem item = (holder[i].Slot as ITPItemSlot<TPItem>).StoredItem;
                if (item != null)
                {
                    holder[i].Slot.OnItemChanged += () => {
                        holder[i].itemHolder = ItemDatabase.GetItemHolder(item.ID);
                    };
                }
            }
            return holder;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private T[] LoadSlots<T, U>(T[] slots, U[] holder)
            where T : TPItemSlot
            where U : TPItemSlotHolder
        {
            int length = holder.Length;
            slots = new T[length];
            for (int i = 0; i < length; i++)
            {
                slots[i] = holder[i].Slot as T;
            }
            return slots;
        }
    }
}
