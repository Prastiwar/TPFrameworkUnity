using System;
using UnityEditor;

namespace TPFramework.Unity
{
    [Serializable]
    public class UDictionaryIntTPItemHolder : UDictionary<int, TPItemHolder>
    {
        public UDictionaryIntTPItemHolder() : base() { }
        public UDictionaryIntTPItemHolder(int capacity) : base(capacity) { }
    }

    [CustomPropertyDrawer(typeof(UDictionaryIntTPItemHolder))]
    public class TPItemDatabaseDrawer : UDictionaryReadKeyDrawer { }
}
