/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace TP.Framework.Unity
{
    public static class FadeSystem
    {
        private static GameObject dispatcher = null;
        private static FadeLayout fadeLayout;
        private static bool isFading;
        private static Action StartFadeState = ()=> ChangeFadeState(true);
        private static Action EndFadeState = ()=> ChangeFadeState(false);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Fade<T>(Fader<T> fader) where T : IFadeActivator
        {
            if (dispatcher is null)
            {
                Init();
            }

            if (!isFading)
            {
                fader.FadeActivator.InitializeFade(fader.Info, fadeLayout);
                AnimSystem.Animate(fader.Info.FadeAnim,
                    (time) => fader.FadeActivator.Fade(time, fader.Info, fadeLayout), 
                    StartFadeState,
                    () => {
                        fader.FadeActivator.CleanUp(fader.Info, fadeLayout);
                        EndFadeState();
                    });
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CanLoadScene(bool readAnyKey)
        {
            return readAnyKey ? Input.anyKeyDown : true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CanLoadScene(KeyCode keyToRead)
        {
            return Input.GetKeyDown(keyToRead);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryLoadScene(KeyCode keyToRead, AsyncOperation asyncLoad)
        {
            if (CanLoadScene(keyToRead))
            {
                asyncLoad.allowSceneActivation = true;
            }
            return asyncLoad.allowSceneActivation;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryLoadScene(bool readAnyKey, AsyncOperation asyncLoad)
        {
            if (CanLoadScene(readAnyKey))
            {
                asyncLoad.allowSceneActivation = true;
            }
            return asyncLoad.allowSceneActivation;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Init()
        {
            dispatcher = new GameObject("TPFader");
            Canvas canvas = dispatcher.AddComponent<Canvas>();

            fadeLayout = new FadeLayout {
                Image = dispatcher.AddComponent<Image>(),
                CanvasGrouup = dispatcher.AddComponent<CanvasGroup>()
            };

            fadeLayout.Image.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
            fadeLayout.Image.raycastTarget = false;

            fadeLayout.CanvasGrouup.interactable = false;
            fadeLayout.CanvasGrouup.blocksRaycasts = false;
            fadeLayout.CanvasGrouup.ignoreParentGroups = true;
            fadeLayout.CanvasGrouup.alpha = 0;

            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            if (canvas.sortingOrder <= 1)
            {
                canvas.sortingOrder = 16;
            }
            UnityEngine.Object.DontDestroyOnLoad(dispatcher);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ChangeFadeState(bool start)
        {
            isFading = start;
            fadeLayout.Image.enabled = start;
            fadeLayout.CanvasGrouup.alpha = 0;
        }
    }
}
