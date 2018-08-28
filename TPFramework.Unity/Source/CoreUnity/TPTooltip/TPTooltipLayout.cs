/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace TPFramework.Unity
{
    [Serializable]
    public class TPTooltipLayout : TPUILayout
    {
        internal float panelHalfHeight;
        internal float panelHalfWidth;

        public Vector2 DynamicOffset;
        public bool UseSharedLayout;
        [SerializeField] private Transform staticPosition;

        protected override void OnInitialized()
        {
#if TPTooltipSafeChecks
            SafeCheck(TPLayout);
#endif
            CanvasGroup.alpha = 0;
            CanvasGroup.blocksRaycasts = false;

            Rect panelRect = LayoutTransform.GetComponent<Image>().rectTransform.rect;
            panelHalfWidth = panelRect.width / 2;
            panelHalfHeight = panelRect.height / 2;
        }

        protected override bool LayoutSpawn(Transform parent = null)
        {
            if (UseSharedLayout)
            {
                TPLayout = TPTooltipSystem.ShareLayout(LayoutPrefab, parent);
                return true;
            }
            return false;
        }

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

#if TPTooltipSafeChecks
        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        private void SafeCheck(GameObject layout)
        {
            if (!layout.transform.GetComponent<CanvasGroup>())
                throw new Exception("Invalid Tooltip Layout! Prefab needs to have CanvasGroup component");
            else if (!LayoutTransform.GetComponent<Image>())
                throw new Exception("Invalid Tooltip Layout! LayoutTransform(child of canvas) needs to have Image component");
        }
#endif

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public void SetPosition(Vector2 position)
        {
            LayoutTransform.position = position;
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public void SetPositionToStatic()
        {
            LayoutTransform.position = staticPosition.position;
        }
    }
}
