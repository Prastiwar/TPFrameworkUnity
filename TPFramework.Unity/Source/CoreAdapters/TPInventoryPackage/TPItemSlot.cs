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
    public class TPItemSlot : MonoBehaviour, ITPItemSlot<Core.TPItem>, ISerializationCallbackReceiver
    {
        private Core.TPItemSlot itemSlot;
        private Image itemImage;

        [SerializeField] private int type;
        [SerializeField] private TPItem storedItem;

        public int Type { get { throw new NotImplementedException(); } }

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
            return itemSlot.SwitchItem(item);
        }

        public bool MoveItem(ITPItemSlot<Core.TPItem> targetSlot)
        {
            return itemSlot.MoveItem(targetSlot);
        }

        public bool CanHoldItem(Core.TPItem item)
        {
            return itemSlot.CanHoldItem(item);
        }

        public bool TypeMatch(Core.TPItem item)
        {
            return itemSlot.TypeMatch(item);
        }

        public bool IsFull()
        {
            return itemSlot.IsFull();
        }

        public bool IsEmpty()
        {
            return itemSlot.IsEmpty();
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize() { }
        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            itemSlot = this;
        }

        public static implicit operator Core.TPItemSlot(TPItemSlot slot)
        {
            Core.TPItem item = slot.storedItem;
            return new Core.TPItemSlot(slot.type, item);
        }
    }
}
