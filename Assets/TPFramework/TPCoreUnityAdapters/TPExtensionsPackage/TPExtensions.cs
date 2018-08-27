/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFramework/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFramework 
*/

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TPFramework.Core;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace TPFramework.Unity
{
    public static class TPExtensions
    {
        private static readonly ReusableList<Vector3> reusableVector3 = new ReusableList<Vector3>();
        private static readonly ReusableList<Transform> reusableTransform = new ReusableList<Transform>();
        private static readonly char[] resolutionSeparators = new char[] { ' ', 'x', '@', 'H', 'z' };

        /* --------------------------------------------------------------- Utility --------------------------------------------------------------- */

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public static bool IsFrame(this int frameModulo)
        {
            return Time.frameCount % frameModulo == 0;
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public static void ToLog(this object obj, string label = null)
        {
            Debug.Log(label != null ? label + ": " + obj : obj);
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public static float GetFloat(this AudioMixer audioMixer, string paramName)
        {
            float value;
            bool result = audioMixer.GetFloat(paramName, out value);
            if (result)
                return value;
            else
                return 0f;
        }

        /* --------------------------------------------------------------- Image --------------------------------------------------------------- */

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public static void SetAlpha(this Image image, float alpha)
        {
            Color color = image.color;
            color.a = alpha;
            image.color = color;
        }

        /* --------------------------------------------------------------- Rect --------------------------------------------------------------- */

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public static bool IsInside(this Rect thisRect, Rect rect)
        {
            return thisRect.xMin <= rect.xMin
                && thisRect.xMax >= rect.xMax
                && thisRect.yMin <= rect.yMin
                && thisRect.yMax >= rect.yMax;
        }

        /* --------------------------------------------------------------- Object --------------------------------------------------------------- */

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public static GameObject FindObjectWithLayer(this UnityEngine.Object obj, int layer)
        {
            GameObject[] objects = UnityEngine.Object.FindObjectsOfType(typeof(GameObject)) as GameObject[];
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

        /* --------------------------------------------------------------- Transform --------------------------------------------------------------- */

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public static void SetPosX(this Transform transform, float x)
        {
            transform.position.Set(x, transform.position.y, transform.position.z);
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public static void SetPosY(this Transform transform, float y)
        {
            transform.position.Set(transform.position.x, y, transform.position.z);
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public static void SetPosZ(this Transform transform, float z)
        {
            transform.position.Set(transform.position.x, transform.position.y, z);
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public static Vector3[] GetChildrenPositions(this Transform parent)
        {
            int length = parent.childCount;
            List<Vector3> positions = reusableVector3.CleanList;
            for (int i = 0; i < length; i++)
                positions.Add(parent.GetChild(i).position);
            return positions.ToArray();
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public static Transform[] GetChilds(this Transform parent)
        {
            int length = parent.childCount;
            List<Transform> transforms = reusableTransform.CleanList;
            for (int i = 0; i < length; i++)
                transforms.Add(parent.GetChild(i));
            return transforms.ToArray();
        }

        /* --------------------------------------------------------------- Vectors --------------------------------------------------------------- */

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public static bool IsEqualTo(this Vector3 vector, Vector3 equalVector)
        {
            return vector.x == equalVector.x
                && vector.y == equalVector.y
                && vector.z == equalVector.z;
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public static bool IsEqualTo(this Vector3 vector, Vector2 equalVector)
        {
            return vector.x == equalVector.x
                && vector.y == equalVector.y;
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public static bool IsEqualTo(this Vector2 vector, Vector3 equalVector)
        {
            return vector.x == equalVector.x
                && vector.y == equalVector.y;
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public static bool IsGreaterThan(this Vector3 vector, Vector3 equalVector)
        {
            return vector.x > equalVector.x
                && vector.y > equalVector.y
                && vector.z > equalVector.z;
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public static bool IsGreaterThan(this Vector3 vector, Vector2 equalVector)
        {
            return vector.x > equalVector.x
                && vector.y > equalVector.y;
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public static bool IsGreaterThan(this Vector2 vector, Vector3 equalVector)
        {
            return vector.x > equalVector.x
                && vector.y > equalVector.y;
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public static bool IsLessThan(this Vector3 vector, Vector3 equalVector)
        {
            return vector.x < equalVector.x
                && vector.y < equalVector.y
                && vector.z < equalVector.z;
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public static bool IsLessThan(this Vector3 vector, Vector2 equalVector)
        {
            return vector.x < equalVector.x
                && vector.y < equalVector.y;
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public static bool IsLessThan(this Vector2 vector, Vector3 equalVector)
        {
            return vector.x < equalVector.x
                && vector.y < equalVector.y;
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public static bool IsLessEqualThan(this Vector3 vector, Vector3 equalVector)
        {
            return vector.IsEqualTo(equalVector) || vector.IsLessThan(equalVector);
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public static bool IsLessEqualThan(this Vector3 vector, Vector2 equalVector)
        {
            return vector.IsEqualTo(equalVector) || vector.IsLessThan(equalVector);
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public static bool IsLessEqualThan(this Vector2 vector, Vector3 equalVector)
        {
            return vector.IsEqualTo(equalVector) || vector.IsLessThan(equalVector);
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public static bool IsGreaterEqualThan(this Vector3 vector, Vector3 equalVector)
        {
            return vector.IsEqualTo(equalVector) || vector.IsGreaterThan(equalVector);
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public static bool IsGreaterEqualThan(this Vector3 vector, Vector2 equalVector)
        {
            return vector.IsEqualTo(equalVector) || vector.IsGreaterThan(equalVector);
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public static bool IsGreaterEqualThan(this Vector2 vector, Vector3 equalVector)
        {
            return vector.IsEqualTo(equalVector) || vector.IsGreaterThan(equalVector);
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public static Vector2 Subtraction(this Vector2 vector, float subtraction)
        {
            return new Vector3(vector.x - subtraction, vector.y - subtraction);
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public static Vector3 Subtraction(this Vector3 vector, float subtraction)
        {
            return new Vector3(vector.x - subtraction, vector.y - subtraction, vector.z - subtraction);
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public static Vector2 Addition(this Vector2 vector, float addition)
        {
            return new Vector3(vector.x + addition, vector.y + addition);
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public static Vector3 Addition(this Vector3 vector, float addition)
        {
            return new Vector3(vector.x + addition, vector.y + addition, vector.z + addition);
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public static Vector3 Equal(this Vector2 vector, float equal)
        {
            return new Vector3(equal, equal);
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public static Vector3 Equal(this Vector3 vector, float equal)
        {
            return new Vector3(equal, equal, equal);
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
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

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
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

        /* --------------------------------------------------------------- Strings --------------------------------------------------------------- */

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public static string ToStringWithoutHZ(this Resolution resolution)
        {
            return resolution.width + " x " + resolution.height;
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public static string[] ToStringWithoutHZ(this Resolution[] resolutions)
        {
            int length = resolutions.Length;
            string[] resolutionsString = new string[length];
            for (int i = 0; i < length; i++)
                resolutionsString[i] = resolutions[i].ToStringWithoutHZ();
            return resolutionsString;
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        public static string[] ToStringWithtHZ(this Resolution[] resolutions)
        {
            int length = resolutions.Length;
            string[] resolutionsString = new string[length];
            for (int i = 0; i < length; i++)
                resolutionsString[i] = resolutions[i].ToString();
            return resolutionsString;
        }

        /// <summary> resolutionText should be formatted as: "320 x 200 @ 60Hz" or "320 x 200" </summary>
        [MethodImpl((MethodImplOptions)0x100)] // agressive inline        
        public static Resolution ToResolution(this string resolutionText)
        {
            string[] strings = resolutionText.Split(resolutionSeparators, StringSplitOptions.RemoveEmptyEntries);
            return new Resolution() {
                width = int.Parse(strings[0]),
                height = int.Parse(strings[1]),
                refreshRate = strings.Length >= 3 ? int.Parse(strings[2]) : 0
            };
        }
    }
}
