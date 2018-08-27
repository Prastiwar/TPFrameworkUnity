/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFramework/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFramework
*/

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using TPFramework.Core;
using TMPro;

namespace TPFramework.Unity
{
    /* ---------------------------------------------------------------------- Achievement ---------------------------------------------------------------------- */

    [CreateAssetMenu(menuName = "TP/TPAchievement/Achievement", fileName = "Achievement")]
    public class TPAchievemente : ScriptableObject, ITPAchievement<TPAchievementData>
    {
        [SerializeField] private TPAchievementData data;

        public Sprite Icon;
        public TPAchievementData Data { get { return data; } private set { data = value; } }
        public Action OnComplete { get; set; }

        public bool ShowNotifyOnComplete;
        public bool ShowNotifyOnProgress;
        public TPAchievementNotify TPNotify;

        public void AddPoints(float points)
        {
            if (data.IsCompleted)
                return;
            data.Points += points;

            if (data.Points >= data.ReachPoints)
            {
                data.Points = data.ReachPoints;
                Complete();
            }
            else if (ShowNotifyOnProgress)
            {
                TPNotify.Show(this);
            }
        }

        public void Complete()
        {
            data.IsCompleted = true;
            data.Points = data.ReachPoints;
            if (ShowNotifyOnComplete)
                TPNotify.Show(this, true);
            OnComplete();
        }
    }

    /* ---------------------------------------------------------------------- Notification ---------------------------------------------------------------------- */

    [Serializable]
    public sealed class TPAchievementNotify : TPUILayout
    {
        public TPAnimation NotifyAnim;

        private Image iconImage;
        private TextMeshProUGUI pointsText;
        private TextMeshProUGUI reachPointsText;
        private TextMeshProUGUI titleText;
        private TextMeshProUGUI descriptionText;
        private Sprite achievementIcon;

        protected override void OnInitialized()
        {
            iconImage = Images[0];
            pointsText = Texts[0];
            reachPointsText = Texts[1];
            titleText = Texts[2];
            descriptionText = Texts[3];
        }

        public void Show(TPAchievement achievement, bool showDescription = false)
        {
            TPAchievementData fillInfo = achievement.Data;
            if (!showDescription)
                fillInfo.Description = string.Empty;
            achievementIcon = achievement.Icon;
            Initialize();
            TPAchievementManager.ShowNotification(this, fillInfo);
        }

        protected override bool LayoutSpawn(Transform parent = null)
        {
            TPLayout = TPAchievementManager.ShareLayout(LayoutPrefab, parent);
            return true;
        }

        public void FillNotify(TPAchievementData fillInfo)
        {
            iconImage.sprite = achievementIcon;
            titleText.text = fillInfo.Title;
            descriptionText.text = fillInfo.Description;
            pointsText.text = fillInfo.Points.ToString();
            reachPointsText.text = fillInfo.ReachPoints.ToString();
        }
    }
}
