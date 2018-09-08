/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace TP.Framework.Unity
{
    public static class TPAnim
    {
        public delegate void OnAnimActivationHandler(float time, Transform transform);

        /// <summary> Runs coroutine that will call onAnimation every frame till evaluated time of anim.Curve will be 1.0f </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Animate(TPAnimation anim, Action<float> onAnimation, Action onStart = null, Action onEnd = null)
        {
            TPCoroutine.RunCoroutine(IEAnimate(anim, onAnimation, onStart, onEnd));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IEnumerator IEAnimate(TPAnimation anim, Action<float> onAnimation, Action onStart = null, Action onEnd = null)
        {
            onStart?.Invoke();
            float percentage = 0.0f;
            while (percentage <= 1.0f && anim.AllowBreak)
            {
                float time = Mathf.Clamp01(anim.Curve.Evaluate(percentage));
                onAnimation(time);
                percentage += Time.deltaTime * anim.Speed;
                yield return null;
            }
            onEnd?.Invoke();
        }
    }
}
