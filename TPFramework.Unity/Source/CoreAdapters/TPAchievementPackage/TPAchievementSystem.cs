/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using TPFramework.Core;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace TPFramework.Unity
{
    public static class TPAchievementSystem
    {
        public static TPAnim.OnAnimActivationHandler OnNotifyActivation = delegate { };

        private static bool isBusy;
        private static SharedGameObjectCollection sharedLayouts = new SharedGameObjectCollection(2);
        private static Queue<KeyValuePair<TPAchievementNotify, TPAchievementData>> notificationQueue = new Queue<KeyValuePair<TPAchievementNotify, TPAchievementData>>(4);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void ShowNotification(TPAchievementNotify notification, TPAchievementData notifyInfo)
        {
            if (!isBusy)
            {
                isBusy = true;
                notification.FillNotify(notifyInfo);
                TPAnim.Animate(notification.NotifyAnim,
                    (time) => OnNotifyActivation(time, notification.LayoutTransform),
                    () => notification.SetActive(true),
                    () => OnEndShowNotification(notification));
            }
            else
            {
                notificationQueue.Enqueue(new KeyValuePair<TPAchievementNotify, TPAchievementData>(notification, notifyInfo));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GameObject ShareLayout(GameObject layout, Transform parent = null)
        {
            return sharedLayouts.ShareObject(layout, parent);
        }

        private static void OnEndShowNotification(TPAchievementNotify notification)
        {
            notification.SetActive(false);
            isBusy = false;
            if (notificationQueue.Count > 0)
            {
                var pair = notificationQueue.Dequeue();
                ShowNotification(pair.Key, pair.Value);
            }
        }
    }
}
