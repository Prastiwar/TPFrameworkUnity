﻿/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System.Runtime.CompilerServices;
using UnityEngine;

namespace TP.Framework.Unity
{
    public static class RandomSystem
    {
        /// <summary> Returns a random point inside a box with radius 1 </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 InsideUnitBox()
        {
            float randX = Random.Range(-1f, 1f);
            float randY = Random.Range(-1f, 1f);
            float randZ = Random.Range(-1f, 1f);
            return new Vector3(randX, randY, randZ);
        }

        /// <summary> Returns a random point inside a square with radius 1 </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 InsideUnitSquare()
        {
            float randX = Random.Range(-1f, 1f);
            float randY = Random.Range(-1f, 1f);
            return new Vector2(randX, randY);
        }
    }
}
