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
                    (time) => OnNotifyActivation(time, notification.GetTransform()),
                    () => notification.SetActive(true),
                    () => OnEndShowNotification(notification));
            }
            else
            {
                notificationQueue.Enqueue(new KeyValuePair<TPAchievementNotify, TPAchievementData>(notification, notifyInfo));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static GameObject ShareLayout(GameObject layout, Transform parent = null)
        {
            return sharedLayouts.ShareObject(layout, parent);
        }

        private static void OnEndShowNotification(TPAchievementNotify notification)
        {
            notification.SetActive(false);
            isBusy = false;
            if (notificationQueue.Count > 0)
            {
                KeyValuePair<TPAchievementNotify, TPAchievementData> kvp = notificationQueue.Dequeue();
                ShowNotification(kvp.Key, kvp.Value);
            }
        }
    }
}
