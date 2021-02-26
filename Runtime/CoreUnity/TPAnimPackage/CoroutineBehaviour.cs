/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace TP.Framework.Unity
{
    public sealed class CoroutineBehaviour : MonoBehaviour
    {
        private static CoroutineBehaviour instance;

        public static YieldInstruction WaitOneFrame { get { return null; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RunCoroutine(IEnumerator routine)
        {
            if (instance is null)
            {
                Init();
            }
            // TODO: Implementation
            instance.StartCoroutine(routine); // temporary use Unity's solution
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Init()
        {
            instance = new GameObject("CoroutineDispatcher").AddComponent<CoroutineBehaviour>();
            DontDestroyOnLoad(instance);
        }
    }
}
