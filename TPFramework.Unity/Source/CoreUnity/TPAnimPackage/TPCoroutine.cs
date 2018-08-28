/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace TPFramework.Unity
{
    public sealed class TPCoroutine : MonoBehaviour
    {
        private static TPCoroutine instance;

        public static YieldInstruction WaitOneFrame { get { return null; } }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnScene()
        {
            if (instance == null)
            {
                instance = new GameObject("TPCoroutineDispatcher").AddComponent<TPCoroutine>();
                DontDestroyOnLoad(instance);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RunCoroutine(IEnumerator routine)
        {
            // TODO: Implementation
            instance.StartCoroutine(routine); // temporary use Unity's solution
        }
    }
}
