/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System;
using System.Runtime.CompilerServices;
using TPFramework.Core;
using UnityEngine;
using UnityEngine.UI;

namespace TPFramework.Unity
{
    [Serializable]
    public class TPItemSlot : MonoDragger, ITPItemSlot<Core.TPItem>, ISerializationCallbackReceiver
    {
        protected Core.TPItemSlot itemSlot;
        protected Image slotImage;
        protected Image itemImage;
        protected Canvas itemCanvas;

        [SerializeField] protected int type;
        [SerializeField] protected TPItem storedItem;

        public int Type { get { return itemSlot.Type; } }

        public Core.TPItem StoredItem {
            get { return storedItem; }
            set {
                if (value is null)
                {
                    storedItem = null;
                }
                else
                {
                    storedItem?.Set(value);
                }
                RefreshUI();
            }
        }

        private void OnValidate()
        {
            Reset();
        }

        private void Reset()
        {
#if TPUISafeChecks
            SafeCheck();
#endif
            slotImage = transform.GetComponent<Image>();
            itemImage = transform.GetChild(0).GetComponent<Image>();
            itemCanvas = itemImage.GetComponent<Canvas>();
            RefreshUI();
        }

#if TPUISafeChecks

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SafeCheck()
        {
            if (transform.childCount <= 0 || transform.GetChild(0).GetComponent<Image>() == null)
                throw new Exception("Slot need to have first child image for Item usage");
            else if (transform.GetChild(0).GetComponent<Canvas>() == null)
                throw new Exception("Item image need to have Canvas component to override sorting while dragging");
        }
#endif

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void RefreshUI()
        {
            bool hasItem = !(StoredItem is null);
            itemImage.enabled = hasItem;
            if (hasItem)
            {
                itemImage.SetSprite(storedItem.Icon);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Core.TPItem SwitchItem(Core.TPItem item)
        {
            if (item == null)
            {
                storedItem = null;
            }
            else
            {
                storedItem?.Set(item);
            }
            return itemSlot.SwitchItem(item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveItem(ITPItemSlot<Core.TPItem> targetSlot)
        {
            Debug.Log("target can hold: " + targetSlot.CanHoldItem(StoredItem));
            bool moved = itemSlot.MoveItem(targetSlot);
            Debug.Log("itemSlot moved to target: " + moved);
            if (moved)
            {
                Core.TPItem newItem = ((ITPItemSlot<Core.TPItem>)itemSlot).StoredItem;
                if (newItem == null)
                {
                    storedItem = null;
                }
                else
                {
                    storedItem.Set(newItem);
                }
                RefreshUI();
                return true;
            }
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool CanHoldItem(Core.TPItem item)
        {
            return itemSlot.CanHoldItem(item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TypeMatch(Core.TPItem item)
        {
            return itemSlot.TypeMatch(item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsFull()
        {
            return itemSlot.IsFull();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty()
        {
            return itemSlot.IsEmpty();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void OnDragStarted()
        {
            itemCanvas.overrideSorting = true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override Transform GetDragTransform()
        {
            return itemImage.transform;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override bool CanDrag()
        {
            return !(StoredItem is null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void OnEndDragTarget(ITPItemSlot<Core.TPItem> slot)
        {
            itemCanvas.overrideSorting = false;
            MoveItem(slot);
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
