using System;
using TPFramework.Unity;
using UnityEditor;

[Serializable]
internal class UDictionaryItemHolder : UDictionary<int, TPItemHolder> { }

[CustomPropertyDrawer(typeof(UDictionaryItemHolder))]
internal class UDictionaryDrawers : UDictionaryDrawer { }
