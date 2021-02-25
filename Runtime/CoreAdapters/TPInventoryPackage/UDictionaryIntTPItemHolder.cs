/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/


namespace TP.Framework.Unity
{
    public class UDictionaryIntTPItemHolder : UDictionary<int, TPItemHolder>
    {
        public UDictionaryIntTPItemHolder() { }
        public UDictionaryIntTPItemHolder(int capacity) : base(capacity) { }
    }
}
