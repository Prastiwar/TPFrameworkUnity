/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System;
using System.Runtime.CompilerServices;
using TP.Framework.Unity.UI;
using UnityEngine;

namespace TP.Framework.Unity
{
    /* ---------------------------------------------------------------------- Achievement ---------------------------------------------------------------------- */

    [CreateAssetMenu(menuName = "TP/TPAchievement/Achievement", fileName = "Achievement")]
    public class AchievementScriptable : ScriptableObject, ITPAchievement<AchievementModel>
    {
        [SerializeField] private AchievementModel data;

        public Sprite Icon;
        public bool ShowNotifyOnComplete;
        public bool ShowNotifyOnProgress;
        public AchievementNotifyLayout TPNotify;

        public Action OnCompleted { get; set; }
        public AchievementModel Data { get { return data; } private set { data = value; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddPoints(float points = 1)
        {
            if (data.IsCompleted)
            {
                return;
            }

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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Complete()
        {
            data.IsCompleted = true;
            data.Points = data.ReachPoints;
            if (ShowNotifyOnComplete)
            {
                TPNotify.Show(this, true);
            }
            OnCompleted();
        }
    }
}
