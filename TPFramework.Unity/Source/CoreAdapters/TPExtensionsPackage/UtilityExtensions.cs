/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;

namespace TPFramework.Unity
{
    public static partial class GameObjectExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsFrame(this int frameModulo)
        {
            return Time.frameCount % frameModulo == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToLog(this object obj, string label = null)
        {
            Debug.Log(label != null ? label + ": " + obj : obj);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AllToLog(this object[] objs, string label = null)
        {
            int length = objs.Length;
            for (int i = 0; i < length; i++)
            {
                Debug.Log(label != null ? label + ": " + objs[i] : objs[i]);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AllToLog<T>(this T[] objs, string label = null)
        {
            int length = objs.Length;
            for (int i = 0; i < length; i++)
            {
                if (label != null)
                {
                    Debug.Log(label + ": " + objs[i]);
                }
                else
                {
                    Debug.Log(objs[i]);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float GetFloat(this AudioMixer audioMixer, string paramName)
        {
            bool result = audioMixer.GetFloat(paramName, out float value);
            if (result)
                return value;
            else
                return 0f;
        }
    }
}
