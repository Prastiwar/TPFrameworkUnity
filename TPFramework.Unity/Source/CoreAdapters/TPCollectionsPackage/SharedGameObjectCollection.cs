/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace TPFramework.Unity
{
    [Serializable]
    public class SharedGameObjectCollection
    {
        public readonly Dictionary<int, GameObject> SharedObjects;

        public SharedGameObjectCollection(int capacity = 10)
        {
            SharedObjects = new Dictionary<int, GameObject>(capacity);
        }

        /// <summary> Returns shared object if exists, if no, instantiate it and return </summary>
        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public GameObject ShareObject(GameObject gameObject, Transform parent = null)
        {
            int id = gameObject.GetInstanceID();
            if (SharedObjects.TryGetValue(id, out GameObject sharedObject))
            {
                sharedObject = UnityEngine.Object.Instantiate(gameObject, parent);
                SharedObjects[id] = sharedObject;
            }
            return sharedObject;
        }
    }
}
