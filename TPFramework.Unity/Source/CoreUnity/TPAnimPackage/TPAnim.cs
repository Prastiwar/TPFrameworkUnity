/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System;
using System.Collections;
using System.Runtime.CompilerServices;
using TPFramework.Core;
using UnityEngine;

namespace TPFramework.Unity
{
    [Serializable]
    public class TPAnimation
    {
        public AnimationCurve Curve;
        public float Speed;
        public bool AllowBreak;
    }

    public static class TPAnim
    {
        public delegate void OnAnimActivationHandler(float time, Transform transform);

        /// <summary> Returns normalized value (0 to 1) when evaluatedTime is 0 - 0,5 and normalized value (1 to 0) when 0,5 - 1 </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ReflectNormalizedCurveTime(float evaluatedTime)
        {
            return TPMath.PingPong(evaluatedTime * 2, 1f);
            //return evaluatedTime <= 0.5f
            //        ? (2 * evaluatedTime)         // grow from 0 to 1 when evaluate is from 0 to 0.5f 
            //        : (2 - (2 * evaluatedTime));  // decrease from 1 to 0 when evaluate is from 0.5f to 1f
        }

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
