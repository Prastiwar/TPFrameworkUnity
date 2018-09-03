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
    public class TPItemSlotHolder : MonoDragger<TPItemSlotHolder>, ISerializationCallbackReceiver
    {
        public TPItemSlot Slot;

        protected Image slotImage;
        protected Image itemImage;
        protected Canvas itemCanvas;

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
            itemImage.enabled = Slot.HasItem();
            if (Slot.HasItem())
            {
                //itemImage.SetSprite((Slot as ITPItemSlot<TPItemHolder>).StoredItem.Icon);
            }
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
            return Slot.HasItem();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void OnEndDragTarget(TPItemSlotHolder slotholder)
        {
            itemCanvas.overrideSorting = false;
            Slot.MoveItem(slotholder.Slot);
        }

        [SerializeField, HideInInspector] private int type;
        [SerializeField, HideInInspector] public TPItemHolder ItemHolder;
        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            if (Slot != null)
            {
                type = Slot.Type;
                //item = Slot.;
            }
        }
        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            if (Slot != null)
            {
                Slot = new TPItemSlot(type, ItemHolder?.Item);
            }
        }
    }
}
