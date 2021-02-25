/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace TP.Framework.Unity
{
    public static partial class TPExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInLayerMask(this GameObject gameObject, LayerMask layerMask)
        {
            return ((layerMask.value & (1 << gameObject.layer)) > 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GameObject FindObjectWithLayer(this Object obj, int layerIndex)
        {
            GameObject[] objects = Object.FindObjectsOfType(typeof(GameObject)) as GameObject[];
            int length = objects.Length;
            for (int i = 0; i < length; i++)
            {
                if (objects[i].layer == layerIndex)
                {
                    return objects[i];
                }
            }
            return null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] GetComponentsOnlyInChildren<T>(this GameObject gameObject)
        {
            var components = new HashSet<T>(gameObject.GetComponentsInChildren<T>());
            components.Remove(gameObject.GetComponent<T>());
            return components.ToArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetComponentOnlyInChildren<T>(this GameObject gameObject)
        {
            int length = gameObject.transform.childCount;
            for (int i = 0; i < length; i++)
            {
                T comp = gameObject.transform.GetChild(i).GetComponent<T>();
                if (comp != null)
                {
                    return comp;
                }
            }
            return default(T);
        }
    }
}
