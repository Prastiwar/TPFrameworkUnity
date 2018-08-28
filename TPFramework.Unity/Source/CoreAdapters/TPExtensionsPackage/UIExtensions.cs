/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System.Runtime.CompilerServices;
using UnityEngine.UI;
using UnityEngine;

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
        public static bool IsInside(this Rect thisRect, Rect rect)
        {
            return thisRect.xMin <= rect.xMin
                && thisRect.xMax >= rect.xMax
                && thisRect.yMin <= rect.yMin
                && thisRect.yMax >= rect.yMax;
        }
    }
}
