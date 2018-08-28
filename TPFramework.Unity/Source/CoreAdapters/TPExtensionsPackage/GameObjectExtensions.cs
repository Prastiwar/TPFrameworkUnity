/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFramework/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFramework 
*/

using System.Runtime.CompilerServices;
using UnityEngine;

namespace TPFramework.Unity
{
    public static partial class GameObjectExtensions
    {
        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
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
