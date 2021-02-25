/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace TP.Framework.Unity
{
    [CreateAssetMenu(menuName = "TP/TPInventory/TPItemDatabase", fileName = "TPItemDatabase")]
    public class TPItemDatabase : ScriptableObject
    {
        private UDictionaryIntTPItemHolder itemDatabaseMap;
        [SerializeField] private TPItemHolder[] itemDatabase;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void InitDatabase(TPItemHolder[] itemHolders)
        {
            itemDatabase = itemHolders;
            OnValidate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TPItemHolder GetItemHolder(int id)
        {
            return itemDatabaseMap[id];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TPItemHolder GetItemHolder(Predicate<TPItemHolder> match)
        {
            foreach (var kvp in itemDatabaseMap)
            {
                TPItemHolder holder = kvp.Value;
                if (match(holder))
                {
                    return holder;
                }
            }
            return null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void OnEnable()
        {
            if (itemDatabaseMap == null)
            {
                itemDatabaseMap = new UDictionaryIntTPItemHolder();
            }
            OnValidate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void OnValidate()
        {
            if (itemDatabase != null)
            {
                int length = itemDatabase.Length;
                itemDatabaseMap = new UDictionaryIntTPItemHolder();
                for (int i = 0; i < length; i++)
                {
                    if (itemDatabase[i] != null)
                    {
                        (itemDatabase[i] as ISerializationCallbackReceiver).OnAfterDeserialize();
                        int key = itemDatabase[i].Item.ID;
                        if (!itemDatabaseMap.ContainsKey(key))
                        {
                            itemDatabaseMap.Add(key, itemDatabase[i]);
                        }
                    }
                }
            }
        }
    }
}
