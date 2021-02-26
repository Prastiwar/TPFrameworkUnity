/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System.Runtime.CompilerServices;
using TP.Framework.Unity.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TP.Framework.Unity
{
    public class TooltipBehaviour : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public TooltipType TooltipType;
        public bool IsObserving = true;
        public TooltipLayout TooltipLayout;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void OnPointerClick(PointerEventData eventData)
        {
            if (!CanRaycast(eventData))
                return;

            if (TooltipType.IsClickable())
            {
                TooltipLayout.Prepare(TooltipType);
                TooltipSystem.OnTooltipPointerClick(eventData);
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
                TooltipSystem.OnTooltipPointerEnter(eventData);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void OnPointerExit(PointerEventData eventData)
        {
            if (!CanRaycast(eventData))
                return;

            if (TooltipType != TooltipType.StaticClick)
            {
                TooltipSystem.OnTooltipPointerExit(eventData);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool CanRaycast(PointerEventData eventData)
        {
            return IsObserving && eventData != null;
        }
    }
}
