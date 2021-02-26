/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace TP.Framework.Unity
{
    public static partial class Extensions
    {
        private static readonly char[] resolutionSeparators = new char[] { ' ', 'x', '@', 'H', 'z' };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToStringWithoutHZ(this Resolution resolution)
        {
            return $"{resolution.width} x {resolution.height}";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string[] ToStringWithoutHZ(this Resolution[] resolutions)
        {
            int length = resolutions.Length;
            string[] resolutionsString = new string[length];
            for (int i = 0; i < length; i++)
                resolutionsString[i] = resolutions[i].ToStringWithoutHZ();
            return resolutionsString;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string[] ToStringWithHZ(this Resolution[] resolutions)
        {
            int length = resolutions.Length;
            string[] resolutionsString = new string[length];
            for (int i = 0; i < length; i++)
                resolutionsString[i] = resolutions[i].ToString();
            return resolutionsString;
        }

        /// <summary> resolutionText should be formatted as: "320 x 200 @ 60Hz" or "320 x 200" </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]        
        public static Resolution ToResolution(this string resolutionText)
        {
            string[] strings = resolutionText.Split(resolutionSeparators, StringSplitOptions.RemoveEmptyEntries);
            return new Resolution() {
                width = int.Parse(strings[0]),
                height = int.Parse(strings[1]),
                refreshRate = strings.Length >= 3 ? int.Parse(strings[2]) : 0
            };
        }
    }
}
