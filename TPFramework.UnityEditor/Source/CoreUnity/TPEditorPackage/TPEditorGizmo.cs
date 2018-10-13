/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System.Runtime.CompilerServices;
using UnityEngine;

namespace TP.Framework.Unity.Editor
{
    public static class TPEditorGizmo
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawCube(Vector3 position, Quaternion rotation, Vector3 scale)
        {
            Matrix4x4 wasMatrix = Gizmos.matrix;
            Gizmos.matrix *= Matrix4x4.TRS(position, rotation, scale);
            Gizmos.DrawCube(Vector3.zero, Vector3.one);
            Gizmos.matrix = wasMatrix;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawWireCube(Vector3 position, Quaternion rotation, Vector3 scale)
        {
            Matrix4x4 wasMatrix = Gizmos.matrix;
            Gizmos.matrix *= Matrix4x4.TRS(position, rotation, scale);
            Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
            Gizmos.matrix = wasMatrix;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawCube(Vector3 position, Vector3 eulerRotation, Vector3 scale)
        {
            DrawCube(position, Quaternion.Euler(eulerRotation), scale);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawWireCube(Vector3 position, Vector3 eulerRotation, Vector3 scale)
        {
            DrawWireCube(position, Quaternion.Euler(eulerRotation), scale);
        }
    }
}
