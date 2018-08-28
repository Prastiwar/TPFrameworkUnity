/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TPFramework.Unity
{
    [Serializable]
    public struct TPProgressFade : ITPFade
    {
        public GameObject ProgressPrefab;
        public Slider LoadingBar;
        public Image LoadingImage;
        public TextMeshProUGUI LoadingText;
        public TextMeshProUGUI LoadingProgressText;
        public string LoadingTextString;

        public float ProgressFadeSpeed;

        public bool MustKeyToStart;
        public bool LoadingAnyKeyToStart;
        public KeyCode LoadingKeyToStart;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void InitializeFade(TPFadeLayout state)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Fade(float time, TPFadeInfo fadeInfo, TPFadeLayout state)
        {
        }
    }
}
