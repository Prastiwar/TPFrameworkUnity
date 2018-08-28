/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TPFramework.Unity
{
    [Serializable]
    public struct TPAlphaFade : ITPFade
    {
        public Sprite FadeTexture;
        public Color FadeColor;

        public void InitializeFade(TPFadeLayout state)
        {
            state.Image.sprite = FadeTexture;
            state.Image.color = FadeColor;
        }

        public void Fade(float time, TPFadeInfo fadeInfo, TPFadeLayout state)
        {
            state.CanvasGrouup.alpha = TPAnim.ReflectNormalizedCurveTime(time);

            if (time >= 0.5f && !string.IsNullOrEmpty(fadeInfo.FadeToScene))
            {
                SceneManager.LoadScene(fadeInfo.FadeToScene);
            }
        }
    }
}
