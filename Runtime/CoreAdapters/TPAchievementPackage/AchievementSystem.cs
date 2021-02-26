/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TP.Framework.Collections.Unity;
using TP.Framework.Unity.UI;
using UnityEngine;

namespace TP.Framework.Unity
{
    public static class AchievementSystem
    {
        public static AnimSystem.OnAnimActivationHandler OnNotifyActivation = delegate { };

        private static bool isBusy;
        private static SharedGameObjectCollection sharedLayouts = new SharedGameObjectCollection(2);
        private static Queue<KeyValuePair<AchievementNotifyLayout, AchievementModel>> notificationQueue = new Queue<KeyValuePair<AchievementNotifyLayout, AchievementModel>>(4);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void ShowNotification(AchievementNotifyLayout notification, AchievementModel notifyInfo)
        {
            if (!isBusy)
            {
                isBusy = true;
                notification.FillNotify(notifyInfo);
                AnimSystem.Animate(notification.NotifyAnim,
                    (time) => OnNotifyActivation(time, notification.GetTransform()),
                    () => notification.SetActive(true),
                    () => OnEndShowNotification(notification));
            }
            else
            {
                notificationQueue.Enqueue(new KeyValuePair<AchievementNotifyLayout, AchievementModel>(notification, notifyInfo));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static GameObject ShareLayout(GameObject layout, Transform parent = null)
        {
            return sharedLayouts.ShareObject(layout, parent);
        }

        private static void OnEndShowNotification(AchievementNotifyLayout notification)
        {
            notification.SetActive(false);
            isBusy = false;
            if (notificationQueue.Count > 0)
            {
                KeyValuePair<AchievementNotifyLayout, AchievementModel> kvp = notificationQueue.Dequeue();
                ShowNotification(kvp.Key, kvp.Value);
            }
        }
    }
}
