/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using UnityEngine;

namespace TPFramework.Unity
{
    public class TPGameObjectPool : TPUnityPool<GameObject>
    {
        public TPGameObjectPool(GameObject prefab, int capacity = 4) : base(prefab, capacity) { }

        protected override void OnPush(GameObject obj)
        {
            obj.SetActive(false);
        }
    }
}
