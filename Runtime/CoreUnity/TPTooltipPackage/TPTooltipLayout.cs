/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace TP.Framework.Unity.UI
{
    [Serializable]
    public class TPTooltipLayout : TPUILayout
    {
        internal float panelHalfHeight;
        internal float panelHalfWidth;

        public bool UseSharedLayout;
        public Vector2 DynamicOffset;
        public Transform StaticPosition;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void OnInitialized()
        {
            SafeCheck(UIWindow);

            CanvasGroup.alpha = 0;
            CanvasGroup.blocksRaycasts = false;

            Rect panelRect = LayoutRectTransform.rect;
            panelHalfWidth = panelRect.width / 2;
            panelHalfHeight = panelRect.height / 2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override bool LayoutSpawn(Transform parent = null)
        {
            if (UseSharedLayout)
            {
                UIWindow = TPTooltipSystem.ShareLayout(UIWindowPrefab, parent);
                return true;
            }
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Prepare(TPTooltipType type)
        {
            Initialize();

            if (!type.IsDynamic())
            {
                SetPositionToStatic();
            }

            if (type == TPTooltipType.StaticClick)
            {
                CanvasGroup.blocksRaycasts = true;
            }
            else
            {
                CanvasGroup.blocksRaycasts = false;
            }

        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SafeCheck(GameObject layout)
        {
            if (!layout.transform.GetComponent<CanvasGroup>())
                throw new Exception("Invalid Tooltip Layout! Prefab needs to have CanvasGroup component");
            else if (!LayoutTransform.GetComponent<Image>())
                throw new Exception("Invalid Tooltip Layout! LayoutTransform(child of canvas) needs to have Image component");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetPosition(Vector2 position, bool clampToScreen = true)
        {
            LayoutTransform.position = position;

            if (clampToScreen)
            {
                LayoutRectTransform.ClampToWindow(UIWindowRectTransform);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetPositionToStatic()
        {
            LayoutTransform.position = StaticPosition.position;
        }
    }
}
