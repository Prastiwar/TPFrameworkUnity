using System.Runtime.CompilerServices;
using TPFramework.Core;
/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using UnityEngine;
using UnityEngine.EventSystems;

namespace TPFramework.Unity
{
    public abstract class MonoDragger : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler
    {
        private Vector2 cachePosition;

        protected Transform DragTransform;

        private void Awake()
        {
            DragTransform = GetDragTransform();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void DisableDrag()
        {
            //canvasGroup.blocksRaycasts = true;
            DragTransform.position = cachePosition;
            //cachePosition = Vector2.zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void OnPointerEnter(PointerEventData eventData)
        {

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void OnPointerClick(PointerEventData eventData)
        {

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!CanDrag())
            {
                return;
            }
            cachePosition = DragTransform.position;
            OnDragStarted();
            //canvasGroup.blocksRaycast = false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void OnDrag(PointerEventData eventData)
        {
            if (!CanDrag())
            {
                return;
            }
            DragTransform.position = eventData.position;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void OnEndDrag(PointerEventData eventData)
        {
            ITPItemSlot<Core.TPItem> slotEntered = eventData.pointerEnter?.GetComponent<ITPItemSlot<Core.TPItem>>();
            if (slotEntered != null && slotEntered != this)
            {
                OnEndDragTarget(slotEntered);
            }
            DisableDrag();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual Transform GetDragTransform()
        {
            return transform;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual bool CanDrag() { return true; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void OnDragStarted() { }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract void OnEndDragTarget(ITPItemSlot<Core.TPItem> slot);
    }
}
