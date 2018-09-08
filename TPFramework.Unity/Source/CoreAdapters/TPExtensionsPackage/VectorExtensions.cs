/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TP.Framework.Collections;
using UnityEngine;

namespace TP.Framework.Unity
{
    public static partial class TPExtensions
    {
        private static readonly ReusableList<Vector3> reusableVector3 = new ReusableList<Vector3>();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEqualTo(this Vector3 vector, Vector3 equalVector)
        {
            return vector.x == equalVector.x
                && vector.y == equalVector.y
                && vector.z == equalVector.z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEqualTo(this Vector3 vector, Vector2 equalVector)
        {
            return vector.x == equalVector.x
                && vector.y == equalVector.y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEqualTo(this Vector2 vector, Vector3 equalVector)
        {
            return vector.x == equalVector.x
                && vector.y == equalVector.y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsGreaterThan(this Vector3 vector, Vector3 equalVector)
        {
            return vector.x > equalVector.x
                && vector.y > equalVector.y
                && vector.z > equalVector.z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsGreaterThan(this Vector3 vector, Vector2 equalVector)
        {
            return vector.x > equalVector.x
                && vector.y > equalVector.y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsGreaterThan(this Vector2 vector, Vector3 equalVector)
        {
            return vector.x > equalVector.x
                && vector.y > equalVector.y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsLessThan(this Vector3 vector, Vector3 equalVector)
        {
            return vector.x < equalVector.x
                && vector.y < equalVector.y
                && vector.z < equalVector.z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsLessThan(this Vector3 vector, Vector2 equalVector)
        {
            return vector.x < equalVector.x
                && vector.y < equalVector.y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsLessThan(this Vector2 vector, Vector3 equalVector)
        {
            return vector.x < equalVector.x
                && vector.y < equalVector.y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsLessEqualThan(this Vector3 vector, Vector3 equalVector)
        {
            return vector.IsEqualTo(equalVector) || vector.IsLessThan(equalVector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsLessEqualThan(this Vector3 vector, Vector2 equalVector)
        {
            return vector.IsEqualTo(equalVector) || vector.IsLessThan(equalVector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsLessEqualThan(this Vector2 vector, Vector3 equalVector)
        {
            return vector.IsEqualTo(equalVector) || vector.IsLessThan(equalVector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsGreaterEqualThan(this Vector3 vector, Vector3 equalVector)
        {
            return vector.IsEqualTo(equalVector) || vector.IsGreaterThan(equalVector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsGreaterEqualThan(this Vector3 vector, Vector2 equalVector)
        {
            return vector.IsEqualTo(equalVector) || vector.IsGreaterThan(equalVector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsGreaterEqualThan(this Vector2 vector, Vector3 equalVector)
        {
            return vector.IsEqualTo(equalVector) || vector.IsGreaterThan(equalVector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Subtraction(this Vector2 vector, float subtraction)
        {
            return new Vector3(vector.x - subtraction, vector.y - subtraction);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Subtraction(this Vector3 vector, float subtraction)
        {
            return new Vector3(vector.x - subtraction, vector.y - subtraction, vector.z - subtraction);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Add(this Vector2 vector, float addition)
        {
            return new Vector3(vector.x + addition, vector.y + addition);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Add(this Vector3 vector, float addition)
        {
            return new Vector3(vector.x + addition, vector.y + addition, vector.z + addition);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Set(this Vector2 vector, float equal)
        {
            return new Vector3(equal, equal);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Set(this Vector3 vector, float equal)
        {
            return new Vector3(equal, equal, equal);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3[] SequenceBoxPositions(int width, int height, int layers)
        {
            List<Vector3> vectors = reusableVector3.CleanList;
            int length = width * height * layers;
            for (int i = 0; i < length; i++)
            {
                int x = i / (width * layers);
                int y = i - x * height * layers / layers;
                int z = i - x * width * layers - y * layers;
                vectors.Add(new Vector3(x, y, z));
            }
            return vectors.ToArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3[] SequenceSquarePositions(int width, int height)
        {
            List<Vector3> vectors = reusableVector3.CleanList;
            int length = width * height;
            for (int i = 0; i < length; i++)
            {
                int x = i / width;
                int y = i % height;
                vectors.Add(new Vector2(x, y));
            }
            return vectors.ToArray();
        }
    }
}
