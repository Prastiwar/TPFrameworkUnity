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

namespace TP.Framework.Unity.UI
{
    [Serializable]
    public sealed class AchievementNotifyLayout : UILayout
    {
        public AnimationModel NotifyAnim;

        private Image iconImage;
        private TextMeshProUGUI pointsText;
        private TextMeshProUGUI reachPointsText;
        private TextMeshProUGUI titleText;
        private TextMeshProUGUI descriptionText;
        private Sprite achievementIcon;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void OnInitialized()
        {
            iconImage = Images[0];
            pointsText = Texts[0];
            reachPointsText = Texts[1];
            titleText = Texts[2];
            descriptionText = Texts[3];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override bool LayoutSpawn(Transform parent = null)
        {
            UIWindow = AchievementSystem.ShareLayout(UIWindowPrefab, parent);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Show(AchievementScriptable achievement, bool showDescription = false)
        {
            AchievementModel fillInfo = achievement.Data;
            if (!showDescription)
            {
                fillInfo.Description = string.Empty;
            }
            achievementIcon = achievement.Icon;
            Initialize();
            AchievementSystem.ShowNotification(this, fillInfo);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void FillNotify(AchievementModel fillInfo)
        {
            iconImage.sprite = achievementIcon;
            titleText.text = fillInfo.Title;
            descriptionText.text = fillInfo.Description;
            pointsText.text = fillInfo.Points.ToString();
            reachPointsText.text = fillInfo.ReachPoints.ToString();
        }
    }
}
