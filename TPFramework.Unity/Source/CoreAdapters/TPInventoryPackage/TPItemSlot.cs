///**
//*   Authored by Tomasz Piowczyk
//*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
//*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
//*/

//using System;
//using System.Runtime.CompilerServices;
//using TPFramework.Core;
//using UnityEngine;
//using UnityEngine.UI;

//namespace TPFramework.Unity
//{
//    [Serializable]
//    public class TPItemSlot : MonoDragger<TPItemSlot>, ITPItemSlot<TPItem>, ISerializationCallbackReceiver
//    {
//        protected Image slotImage;
//        protected Image itemImage;
//        protected Canvas itemCanvas;

//        protected int type;
//        protected TPItem storedItem;
//        [SerializeField] public Core.TPItemSlot Slot;

//        protected TPItem StoredItem {
//            get { return storedItem; }
//            set {
//                storedItem?.OnUsed.Remove(ShouldRemove);
//                storedItem = value;
//                storedItem?.OnUsed.Add(ShouldRemove);
//                RefreshUI();
//            }
//        }

//        public int Type { get; protected set; }

//        TPItem ITPItemSlot<TPItem>.StoredItem {
//            get { return StoredItem; }
//            set { StoredItem = value; }
//        }

//        private void ShouldRemove()
//        {
//            if (StoredItem.AmountStack <= 0)
//            {
//                StoredItem = null;
//            }
//        }

//        public TPItemSlot(int type, TPItem storeItem = null)
//        {
//            Type = type;
//            StoredItem = storeItem;
//        }

//        /// <summary> Holds given item and returns the old one </summary>
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public TPItem SwitchItem(TPItem item)
//        {
//            TPItem returnItem = storedItem ?? null;
//            StoredItem = item;
//            return returnItem;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public bool UseItem()
//        {
//            return StoredItem != null ? StoredItem.Use() : false;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public bool StackItem(int count = 1)
//        {
//            return StoredItem != null ? StoredItem.Stack(count) : false;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public bool IsFull()
//        {
//            return StoredItem != null && StoredItem.AmountStack >= StoredItem.MaxStack;
//        }

//        /// <summary> Is type of item same as slot type? </summary>
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public bool TypeMatch(TPItem item)
//        {
//            return item.Type == Type || Type == 0;
//        }

//        /// <summary> Checks if given slot is opposite of this slot </summary>
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public virtual bool IsSlotOpposite(ITPItemSlot<TPItem> slot)
//        {
//            return slot is ITPEquipSlot<TPItem>;
//        }

//        /// <summary> Checks for place in stack and type match </summary>
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public virtual bool CanHoldItem(TPItem item)
//        {
//            return !IsFull() && TypeMatch(item);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public virtual bool IsEmpty()
//        {
//            return StoredItem == null;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public virtual bool MoveItem(ITPItemSlot<TPItem> targetSlot)
//        {
//            if (targetSlot.CanHoldItem(StoredItem))
//            {
//                StoredItem.OnMoved?.Invoke();
//                StoredItem = targetSlot.SwitchItem(StoredItem);
//                StoredItem?.OnMoved?.Invoke();
//                return true;
//            }
//            StoredItem?.OnFailMoved?.Invoke();
//            return false;
//        }

//        private void OnValidate()
//        {
//            Reset();
//        }

//        private void Reset()
//        {
//#if TPUISafeChecks
//            SafeCheck();
//#endif
//            slotImage = transform.GetComponent<Image>();
//            itemImage = transform.GetChild(0).GetComponent<Image>();
//            itemCanvas = itemImage.GetComponent<Canvas>();
//            RefreshUI();
//        }

//#if TPUISafeChecks

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        private void SafeCheck()
//        {
//            if (transform.childCount <= 0 || transform.GetChild(0).GetComponent<Image>() == null)
//                throw new Exception("Slot need to have first child image for Item usage");
//            else if (transform.GetChild(0).GetComponent<Canvas>() == null)
//                throw new Exception("Item image need to have Canvas component to override sorting while dragging");
//        }
//#endif

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        private void RefreshUI()
//        {
//            bool hasItem = !(StoredItem is null);
//            itemImage.enabled = hasItem;
//            if (hasItem)
//            {
//                itemImage.SetSprite(storedItem.Icon);
//            }
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        protected override void OnDragStarted()
//        {
//            itemCanvas.overrideSorting = true;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        protected override Transform GetDragTransform()
//        {
//            return itemImage.transform;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        protected override bool CanDrag()
//        {
//            return !(StoredItem is null);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        protected override void OnEndDragTarget(ITPItemSlot<Core.TPItem> slot)
//        {
//            itemCanvas.overrideSorting = false;
//            //MoveItem(slot);
//        }

//        void ISerializationCallbackReceiver.OnBeforeSerialize() { }
//        void ISerializationCallbackReceiver.OnAfterDeserialize()
//        {
//            //itemSlot = this;
//        }

//        public static implicit operator Core.TPItemSlot(TPItemSlot slot)
//        {
//            Core.TPItem item = slot.storedItem;
//            return new Core.TPItemSlot(slot.type, item);
//        }
//    }
//}
