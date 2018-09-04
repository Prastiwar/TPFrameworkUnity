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
                itemSlotHolders = AddOnItemChangedAction(itemSlotHolders);
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
                equipSlotsHolders = AddOnItemChangedAction(equipSlotsHolders);
                EquipSlots = LoadSlots(EquipSlots, equipSlotsHolders);
                SetEquipSlots(EquipSlots);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private T[] AddOnItemChangedAction<T>(T[] holder)
            where T : TPItemSlotHolder
        {
            int length = holder.Length;
            for (int i = 0; i < length; i++)
            {
                int index = i;
                (holder[index] as ISerializationCallbackReceiver).OnAfterDeserialize();
                holder[index].Slot.OnItemChanged += () => {
                    holder[index].itemHolder = holder[index].Slot.HasItem() ? ItemDatabase.GetItemHolder(holder[index].Slot.StoredItem.ID) : null;
                    holder[index].RefreshUI();
                };
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

        void ISerializationCallbackReceiver.OnBeforeSerialize() { }
        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            InitEquipSlots(equipSlotsHolders);
            InitItemSlots(itemSlotHolders);
        }
    }
}
