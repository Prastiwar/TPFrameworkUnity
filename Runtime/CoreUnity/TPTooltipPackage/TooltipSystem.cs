/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System;
using System.Collections;
using System.Runtime.CompilerServices;
using TP.Framework.Collections.Unity;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TP.Framework.Unity
{
    public static class TooltipSystem
    {
        private static TooltipBehaviour observer;
        private static PointerEventData _eventData;
        private static readonly SharedGameObjectCollection sharedLayouts = new SharedGameObjectCollection(2);

        public static Action<TooltipBehaviour> OnObserverEnter = delegate { observer.TooltipLayout.SetActive(true); };
        public static Action<TooltipBehaviour> OnObserverExit = delegate { observer.TooltipLayout.SetActive(false); };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GameObject ShareLayout(GameObject layout, Transform parent = null)
        {
            return sharedLayouts.ShareObject(layout, parent);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void OnTooltipPointerClick(PointerEventData eventData)
        {
            observer = eventData.pointerEnter.GetComponent<TooltipBehaviour>();
            _eventData = eventData;

            if (!observer.TooltipLayout.IsActive())
            {
                OnObserverEnter(observer);
                if (observer.TooltipType.IsDynamic())
                {
                    observer.StartCoroutine(ToolTipPositioning());
                }
            }
            else
            {
                OnObserverExit(observer);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void OnTooltipPointerEnter(PointerEventData eventData)
        {
            observer = eventData.pointerEnter.GetComponent<TooltipBehaviour>();
            _eventData = eventData;

            OnObserverEnter(observer);
            if (observer.TooltipType.IsDynamic())
            {
                observer.StartCoroutine(ToolTipPositioning());
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void OnTooltipPointerExit(PointerEventData eventData)
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
                Vector2 pointPos = _eventData.position + observer.TooltipLayout.DynamicOffset;
                observer.TooltipLayout.SetPosition(pointPos);
                yield return null;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsClickable(this TooltipType tooltipType)
        {
            return tooltipType == TooltipType.DynamicClick || tooltipType == TooltipType.StaticClick;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsDynamic(this TooltipType tooltipType)
        {
            return tooltipType == TooltipType.DynamicClick || tooltipType == TooltipType.DynamicEnter;
        }
    }
}
