/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TP.Framework.Collections;
using UnityEngine;

namespace TP.Framework.Unity
{
    public static partial class TPExtensions
    {
        private static readonly ReusableList<Transform> reusableTransform = new ReusableList<Transform>();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyChildMatch(this Transform transform, Predicate<Transform> match)
        {
            int length = transform.childCount;
            for (int i = 0; i < length; i++)
            {
                if (match(transform.GetChild(i)))
                {
                    return true;
                }
            }
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Transform GetChildWithLayer(this Transform parent, int layerIndex)
        {
            int length = parent.childCount;
            for (int i = 0; i < length; i++)
            {
                Transform child = parent.GetChild(i);
                if (child.gameObject.layer == layerIndex)
                {
                    return child;
                }
            }
            return null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Transform GetChildWithLayer(this Transform parent, string layerName)
        {
            return GetChildWithLayer(parent, LayerMask.NameToLayer(layerName));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Transform GetChildWithLayer(this Transform parent, LayerMask layerMask)
        {
            int length = parent.childCount;
            for (int i = 0; i < length; i++)
            {
                Transform child = parent.GetChild(i);
                if (child.gameObject.IsInLayerMask(layerMask))
                {
                    return child;
                }
            }
            return null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Transform GetChildWithTag(this Transform parent, string tag)
        {
            int length = parent.childCount;
            for (int i = 0; i < length; i++)
            {
                Transform child = parent.GetChild(i);
                if (child.CompareTag(tag))
                {
                    return child;
                }
            }
            return null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DestroyChildren(this Transform transform)
        {
            int length = transform.childCount;
            for (int i = 0; i < length; i++)
            {
                UnityEngine.Object.DestroyImmediate(transform.GetChild(0).gameObject);
            }
        }

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
