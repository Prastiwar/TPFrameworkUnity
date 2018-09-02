/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System.Runtime.CompilerServices;
using UnityEngine;

namespace TPFramework.Unity
{
    public static partial class TPExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GameObject FindObjectWithLayer(this Object obj, int layer)
        {
            GameObject[] objects = Object.FindObjectsOfType(typeof(GameObject)) as GameObject[];
            int length = objects.Length;
            for (int i = 0; i < length; i++)
            {
                if (objects[i].layer == layer)
                {
                    return objects[i];
                }
            }
            return null;
        }
    }
}
