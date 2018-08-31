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
        public static void OnPointerEnter(PointerEventData eventData)
        {
            observer = eventData.pointerEnter.GetComponent<TPTooltip>();
            _eventData = eventData;

            OnObserverEnter(observer);

            if (observer.TooltipType.IsDynamic())
            {
                observer.StartCoroutine(ToolTipPositioning());
            }
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
                Vector2 pointPos = _eventData.position + observer.TooltipLayout.DynamicOffset;
                //pointPos.Set(Mathf.Clamp(pointPos.x, observer.TooltipLayout.panelHalfWidth, Screen.width - observer.TooltipLayout.panelHalfWidth),
                //             Mathf.Clamp(pointPos.y, observer.TooltipLayout.panelHalfHeight, Screen.height - observer.TooltipLayout.panelHalfHeight));
                observer.TooltipLayout.SetPosition(pointPos);
                RectTransform t = observer.TooltipLayout.LayoutTransform.GetComponent<RectTransform>();
                t.anchorMin = new Vector2(0, 0);
                t.anchorMax = new Vector2(1, 1);
                Transform tra = observer.TooltipLayout.LayoutTransform;
                tra.localPosition = new Vector3(
                Mathf.Clamp(tra.localPosition.x, -t.sizeDelta.x, t.sizeDelta.x),
                Mathf.Clamp(tra.localPosition.y, -t.sizeDelta.y, t.sizeDelta.y));
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
