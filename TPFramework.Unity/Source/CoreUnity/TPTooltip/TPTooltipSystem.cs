/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TPFramework.Unity
{
    public static class TPTooltipSystem
    {
        private static TPTooltip observer;
        private static PointerEventData _eventData;
        private static readonly SharedGameObjectCollection sharedLayouts = new SharedGameObjectCollection(2);

        public static Action<TPTooltip> OnObserverEnter = delegate { observer.TooltipLayout.SetActive(true); };
        public static Action<TPTooltip> OnObserverExit = delegate { observer.TooltipLayout.SetActive(false); };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GameObject ShareLayout(GameObject layout, Transform parent = null)
        {
            return sharedLayouts.ShareObject(layout, parent);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void OnPointerClick(PointerEventData eventData)
        {
            observer = eventData.pointerEnter.GetComponent<TPTooltip>();
            _eventData = eventData;

            if (!observer.TooltipLayout.IsActive())
            {
                OnObserverEnter(observer);
                if (observer.TooltipType.IsDynamic())
                    observer.StartCoroutine(ToolTipPositioning());
            }
            else
            {
                OnObserverExit(observer);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void OnPointerEnter(PointerEventData eventData)
        {
            observer = eventData.pointerEnter.GetComponent<TPTooltip>();
            _eventData = eventData;

            OnObserverEnter(observer);

            if (observer.TooltipType.IsDynamic())
                observer.StartCoroutine(ToolTipPositioning());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void OnPointerExit(PointerEventData eventData)
        {
            if (!observer)
                return;

            OnObserverExit(observer);
            observer = null;
            _eventData = null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IEnumerator ToolTipPositioning()
        {
            while (_eventData != null)
            {
                Vector2 panelHalfVector = _eventData.position + observer.TooltipLayout.DynamicOffset;
                panelHalfVector.Set(Mathf.Clamp(panelHalfVector.x, observer.TooltipLayout.panelHalfWidth, Screen.width - observer.TooltipLayout.panelHalfWidth),
                                    Mathf.Clamp(panelHalfVector.y, observer.TooltipLayout.panelHalfHeight, Screen.height - observer.TooltipLayout.panelHalfHeight));
                observer.TooltipLayout.SetPosition(panelHalfVector);
                yield return null;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsClickable(this TPTooltipType tooltipType)
        {
            return tooltipType == TPTooltipType.DynamicClick || tooltipType == TPTooltipType.StaticClick;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsDynamic(this TPTooltipType tooltipType)
        {
            return tooltipType == TPTooltipType.DynamicClick || tooltipType == TPTooltipType.DynamicEnter;
        }
    }
}
