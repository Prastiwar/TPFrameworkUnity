/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System.Runtime.CompilerServices;
using TPFramework.Core;
using UnityEngine;

namespace TPFramework.Unity
{
    public class TPUnityPool<TObject> : TPObjectPool<TObject>
        where TObject : Object
    {
        private readonly TObject prefab;

        public TPUnityPool(TObject prefab, int capacity = 4) : base(capacity)
        {
            this.prefab = prefab;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override TObject CreateNewObject()
        {
            return Object.Instantiate(prefab);
        }
    }
}
