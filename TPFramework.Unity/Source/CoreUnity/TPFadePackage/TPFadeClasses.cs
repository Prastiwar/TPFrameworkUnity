/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System;
using UnityEngine;
using UnityEngine.UI;

namespace TP.Framework.Unity
{
    public interface ITPFade
    {
        void InitializeFade(TPFadeInfo fadeInfo, TPFadeLayout state);
        void Fade(float time, TPFadeInfo fadeInfo, TPFadeLayout state);
        void CleanUp(TPFadeInfo fadeInfo, TPFadeLayout state);
    }

    [Serializable]
    public class TPFadeInfo
    {
        public string FadeToScene;
        public TPAnimation FadeAnim;
    }

    [Serializable]
    public class TPFader<TFade>
        where TFade : ITPFade
    {
        public TPFadeInfo Info;
        public TFade Fader;
    }

    [Serializable]
    public class TPFadeLayout
    {
        public Image Image;
        public CanvasGroup CanvasGrouup;
    }
}
