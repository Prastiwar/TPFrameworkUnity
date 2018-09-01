/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace TPFramework.Unity
{
    public static partial class GameObjectExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetAlpha(this Image image, float alpha)
        {
            Color color = image.color;
            color.a = alpha;
            image.color = color;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetFill(this Image image, float fillAmount)
        {
            image.fillAmount = fillAmount;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetValue(this Slider slider, float value)
        {
            slider.value = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInside(this Rect thisRect, Rect rect)
        {
            return thisRect.xMin <= rect.xMin
                && thisRect.xMax >= rect.xMax
                && thisRect.yMin <= rect.yMin
                && thisRect.yMax >= rect.yMax;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ClampToWindow(this RectTransform panelRectTransform, RectTransform parentRectTransform)
        {
            Vector3 pos = panelRectTransform.localPosition;

            Vector2 halfScreen = new Vector2(parentRectTransform.rect.width / 2, parentRectTransform.rect.height / 2);
            Vector2 halfPanel = panelRectTransform.sizeDelta / 2;
            Vector2 maxPos = new Vector2(halfScreen.x - halfPanel.x, halfScreen.y - halfPanel.y);
            Vector2 minPos = -maxPos;
            
            pos.x = Mathf.Clamp(pos.x, minPos.x, maxPos.x);
            pos.y = Mathf.Clamp(pos.y, minPos.y, maxPos.y);

            panelRectTransform.localPosition = pos;
        }
    }
}
