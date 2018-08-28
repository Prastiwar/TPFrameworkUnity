/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TPFramework.Core;
using UnityEngine;

namespace TPFramework.Unity
{
    public static partial class GameObjectExtensions
    {
        private static readonly ReusableList<Transform> reusableTransform = new ReusableList<Transform>();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPosX(this Transform transform, float x)
        {
            transform.position.Set(x, transform.position.y, transform.position.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPosY(this Transform transform, float y)
        {
            transform.position.Set(transform.position.x, y, transform.position.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPosZ(this Transform transform, float z)
        {
            transform.position.Set(transform.position.x, transform.position.y, z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3[] GetChildrenPositions(this Transform parent)
        {
            int length = parent.childCount;
            List<Vector3> positions = reusableVector3.CleanList;
            for (int i = 0; i < length; i++)
                positions.Add(parent.GetChild(i).position);
            return positions.ToArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Transform[] GetChilds(this Transform parent)
        {
            int length = parent.childCount;
            List<Transform> transforms = reusableTransform.CleanList;
            for (int i = 0; i < length; i++)
                transforms.Add(parent.GetChild(i));
            return transforms.ToArray();
        }
    }
}
