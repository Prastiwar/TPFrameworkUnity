/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TP.Framework.Unity
{
    public abstract class DragBehaviour<TTarget> : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler
        where TTarget : MonoBehaviour
    {
        private Vector2 cachedPosition;

        protected Transform DragTransform;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!CanDrag())
            {
                return;
            }
            cachedPosition = DragTransform.position;
            OnDragStarted();
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
            if (!CanDrag())
            {
                return;
            }
            TTarget slotEntered = eventData.pointerEnter?.GetComponent<TTarget>();
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
        protected virtual bool CanDrag()
        {
            return true;
        }

        protected virtual void Awake()
        {
            Cache();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void Cache()
        {
            DragTransform = GetDragTransform();
            cachedPosition = DragTransform.position;
        }

        /// <summary> Returns DragTransform to its cached position </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void DisableDrag()
        {
            DragTransform.position = cachedPosition;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual void OnPointerEnter(PointerEventData eventData) { }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual void OnPointerClick(PointerEventData eventData) { }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void OnDragStarted() { }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract void OnEndDragTarget(TTarget slot);
    }
}
