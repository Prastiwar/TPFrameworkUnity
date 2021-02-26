/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TP.Framework.Unity
{
    [Serializable]
    public struct AlphaFadeActivator : IFadeActivator
    {
        public Sprite FadeTexture;
        public Color FadeColor;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void InitializeFade(FadeInfo fadeInfo, FadeLayout state)
        {
            state.Image.sprite = FadeTexture;
            state.Image.color = FadeColor;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Fade(float time, FadeInfo fadeInfo, FadeLayout state)
        {
            state.CanvasGrouup.alpha = TPMath.PingPong(time * 2, 1f);

            if (time >= 0.5f && !string.IsNullOrEmpty(fadeInfo.FadeToScene))
            {
                SceneManager.LoadScene(fadeInfo.FadeToScene);
            }
        }

        public void CleanUp(FadeInfo fadeInfo, FadeLayout state) { }
    }
}
