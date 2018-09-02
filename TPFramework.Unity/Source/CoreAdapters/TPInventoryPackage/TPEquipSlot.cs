/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System;
using TPFramework.Core;
using UnityEngine;
using UnityEngine.UI;

namespace TPFramework.Unity
{
    [Serializable]
    public class TPEquipSlot : MonoBehaviour, ITPEquipSlot<Core.TPItem>, ISerializationCallbackReceiver
    {
        private Core.TPEquipSlot equipSlot;
        private Image itemImage;

        [SerializeField] private int type;
        [SerializeField] private TPItem storedItem;

        public int Type {
            get { return equipSlot.Type; }
        }

        public Core.TPItem StoredItem { get { return storedItem; } set { storedItem = value; RefreshUI(); } }

        private void OnValidate()
        {
            Reset();
        }

        private void Reset()
        {
            itemImage = transform.GetChild(0).GetComponent<Image>();
            RefreshUI();
        }

        private void RefreshUI()
        {
            bool isNull = StoredItem is null;
            itemImage.enabled = !isNull;
            if (!isNull)
            {
                itemImage.SetSprite(storedItem.Icon);
            }
        }

        public Core.TPItem SwitchItem(Core.TPItem item)
        {
            return equipSlot.SwitchItem(item);
        }

        public bool MoveItem(ITPItemSlot<Core.TPItem> targetSlot)
        {
            return equipSlot.MoveItem(targetSlot);
        }

        public bool CanHoldItem(Core.TPItem item)
        {
            return equipSlot.CanHoldItem(item);
        }

        public bool TypeMatch(Core.TPItem item)
        {
            return equipSlot.TypeMatch(item);
        }

        public bool IsFull()
        {
            return equipSlot.IsFull();
        }

        public bool IsEmpty()
        {
            return equipSlot.IsEmpty();
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize() { }
        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            equipSlot = this;
        }

        public static implicit operator Core.TPEquipSlot(TPEquipSlot slot)
        {
            Core.TPItem item = slot.storedItem;
            return new Core.TPEquipSlot(slot.type, item);
        }
    }
}
