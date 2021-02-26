/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System.Runtime.CompilerServices;
using UnityEngine;

namespace TP.Framework.Unity
{
    public class UnityPool<TObject> : ObjectPool<TObject>
        where TObject : Object
    {
        protected readonly TObject Prefab;

        public UnityPool(TObject prefab, int capacity = 4) : base(capacity)
        {
            Prefab = prefab;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override TObject CreateNewObject()
        {
            return Object.Instantiate(Prefab);
        }
    }
}
