﻿/**
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
        [SerializeField] private TPItemDatabase itemDatabase;
        [SerializeField] private TPItemSlotHolder[] itemSlotHolders;
        [SerializeField] private TPEquipSlotHolder[] equipSlotsHolders;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TPItemHolder GetItemHolder(int itemID)
        {
            return itemDatabase.GetItemHolder(itemID);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TPItemHolder GetItemHolder(Predicate<TPItemHolder> match)
        {
            return itemDatabase.GetItemHolder(match);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void InitItemDatabase(TPItemHolder[] itemHolders)
        {
            itemDatabase.InitDatabase(itemHolders);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void InitItemSlots(TPItemSlotHolder[] slotHolders)
        {
            itemSlotHolders = slotHolders;
            if (itemSlotHolders != null)
            {
                itemSlotHolders = InjectItemDatabase(itemSlotHolders);
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
                equipSlotsHolders = InjectItemDatabase(equipSlotsHolders);
                EquipSlots = LoadSlots(EquipSlots, equipSlotsHolders);
                SetEquipSlots(EquipSlots);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private T[] InjectItemDatabase<T>(T[] holders) where T : TPItemSlotHolder
        {
            int length = holders.Length;
            for (int i = 0; i < length; i++)
            {
                holders[i].itemDatabase = itemDatabase;
            }
            return holders;
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
