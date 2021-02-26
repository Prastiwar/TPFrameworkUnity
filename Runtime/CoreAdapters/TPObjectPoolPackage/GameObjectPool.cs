/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using UnityEngine;

namespace TP.Framework.Unity
{
    public class GameObjectPool : UnityPool<GameObject>
    {
        public GameObjectPool(GameObject prefab, int capacity = 4) : base(prefab, capacity) { }

        protected override void OnPush(GameObject obj)
        {
            obj.SetActive(false);
        }
    }
}
