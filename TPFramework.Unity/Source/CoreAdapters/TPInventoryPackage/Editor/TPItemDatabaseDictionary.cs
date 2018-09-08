/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System;

namespace TP.Framework.Unity
{
    [Serializable]
    internal class UDictionaryIntTPItemHolder : UDictionary<int, TPItemHolder>
    {
        public UDictionaryIntTPItemHolder() : base() { }
        public UDictionaryIntTPItemHolder(int capacity) : base(capacity) { }
    }
}
