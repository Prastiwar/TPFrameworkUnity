using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;

namespace TPFramework.Unity
{
    public static partial class GameObjectExtensions
    {
        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public static bool IsFrame(this int frameModulo)
        {
            return Time.frameCount % frameModulo == 0;
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public static void ToLog(this object obj, string label = null)
        {
            Debug.Log(label != null ? label + ": " + obj : obj);
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
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
