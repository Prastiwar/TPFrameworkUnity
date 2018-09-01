/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TPFramework.Unity
{
    public enum TPTooltipType
    {
        DynamicEnter, // moves with cursor - show on pointer enter
        DynamicClick, // moves with cursor - show on click
        StaticEnter,  // doesn't move with cursor - show on pointer enter
        StaticClick   // doesn't move with cursor - show on click
    }
    
    public class TPTooltip : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public TPTooltipType TooltipType;
        public bool IsObserving = true;
        public TPTooltipLayout TooltipLayout;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void OnPointerClick(PointerEventData eventData)
        {
            if (!CanRaycast(eventData))
                return;

            if (TooltipType.IsClickable())
            {
                TooltipLayout.Prepare(TooltipType);
                TPTooltipSystem.OnTooltipPointerClick(eventData);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!CanRaycast(eventData))
                return;

            if (!TooltipType.IsClickable())
            {
                TooltipLayout.Prepare(TooltipType);
                TPTooltipSystem.OnTooltipPointerEnter(eventData);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void OnPointerExit(PointerEventData eventData)
        {
            if (!CanRaycast(eventData))
                return;

            if (TooltipType != TPTooltipType.StaticClick)
            {
                TPTooltipSystem.OnTooltipPointerExit(eventData);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool CanRaycast(PointerEventData eventData)
        {
            return IsObserving && eventData != null;
        }
    }
}
