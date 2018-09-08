/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TP.Framework.Unity
{
    using System.Runtime.CompilerServices;

    [System.Serializable]
    public struct TPProgressFade : ITPFade
    {
        private GameObject layout;
        private AsyncOperation asyncLoad;

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
        public void InitializeFade(TPFadeInfo fadeInfo, TPFadeLayout state)
        {
            if (!layout)
            {
                layout = Object.Instantiate(ProgressPrefab);

                LoadingBar = Object.Instantiate(LoadingBar, layout.transform);
                LoadingImage = Object.Instantiate(LoadingImage, layout.transform);
                LoadingProgressText = Object.Instantiate(LoadingProgressText, layout.transform);
                LoadingText = Object.Instantiate(LoadingText, layout.transform);

                asyncLoad = SceneManager.LoadSceneAsync(fadeInfo.FadeToScene);
                asyncLoad.allowSceneActivation = false;
            }
            layout.SetActive(true);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Fade(float time, TPFadeInfo fadeInfo, TPFadeLayout state)
        {
            if (!asyncLoad.isDone)
            {
                SetProgress(asyncLoad.progress);
                if (asyncLoad.progress >= 0.9f)
                {
                    SetProgress(1f);
                    if (TryLoadScene())
                    {
                        fadeInfo.FadeAnim.AllowBreak = true;
                    }
                }
            }
        }

        private bool TryLoadScene()
        {
            if (MustKeyToStart)
            {
                return LoadingAnyKeyToStart
                    ? TPFade.TryLoadScene(true, asyncLoad)
                    : TPFade.TryLoadScene(LoadingKeyToStart, asyncLoad);
            }
            return TPFade.TryLoadScene(false, asyncLoad);
        }

        private void SetProgress(float progress)
        {
            LoadingProgressText?.SetText((progress * 100).ToString("0") + "%");
            LoadingBar?.SetValue(progress);
            LoadingImage?.SetFill(progress * 100);

            if (progress >= 1f)
            {
                LoadingText?.SetText(LoadingTextString);
            }
        }

        public void CleanUp(TPFadeInfo fadeInfo, TPFadeLayout state)
        {
            layout.SetActive(false);
        }
    }
}
