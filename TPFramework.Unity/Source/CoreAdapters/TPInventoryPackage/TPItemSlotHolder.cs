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
    public class TPItemSlotHolder : TPDragger<TPItemSlotHolder>, ISerializationCallbackReceiver
    {
        protected Image itemImage;
        protected Canvas itemCanvas;

        [SerializeField] protected int type;
        [SerializeField, InspectorReadOnly] internal TPItemHolder itemHolder;
        [SerializeField, HideInInspector] internal TPItemDatabase itemDatabase;

        [NonSerialized] public TPItemSlot Slot;

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
        protected override void OnDragStarted()
        {
            itemCanvas.overrideSorting = true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void OnEndDragTarget(TPItemSlotHolder slotholder)
        {
            itemCanvas.overrideSorting = false;
            if (Slot.MoveItem(slotholder.Slot))
            {
                slotholder.RefreshUI();
                RefreshUI();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void RefreshUI()
        {
            itemImage.enabled = Slot.HasItem();
            if (Slot.HasItem())
            {
                itemImage.SetSprite(itemHolder.Icon);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void OnValidate()
        {
            if (transform.childCount <= 0)
            {
                throw new ArgumentNullException("Child image with canvas component not found " + Slot);
            }
            else
            {
                itemImage = transform.GetChild(0).GetComponent<Image>();
                itemCanvas = itemImage.GetComponent<Canvas>();
                if (itemImage == null || itemCanvas == null)
                {
                    throw new ArgumentNullException("Child image with canvas component not found " + Slot);
                }
            }
            RefreshUI();
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            type = Slot.Type;
            (itemHolder as ISerializationCallbackReceiver)?.OnBeforeSerialize();
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            (itemHolder as ISerializationCallbackReceiver)?.OnAfterDeserialize();
            Slot = new TPItemSlot(type, itemHolder?.Item) {
                OnItemChanged = () => {
                    itemHolder = Slot.HasItem() ? itemDatabase.GetItemHolder(Slot.StoredItem.ID) : null;
                    RefreshUI();
                }
            };
        }
    }
}
