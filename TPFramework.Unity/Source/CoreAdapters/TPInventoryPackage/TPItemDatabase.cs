/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace TPFramework.Unity
{
    [CreateAssetMenu(menuName = "TP/TPInventory/TPItemDatabase", fileName = "TPItemDatabase")]
    public class TPItemDatabase : ScriptableObject
    {
        [SerializeField] private UDictionaryIntTPItemHolder itemDatabaseMap;
        [SerializeField] private TPItemHolder[] itemDatabase;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void InitDatabase(TPItemHolder[] itemHolders)
        {
            itemDatabase = itemHolders;
            itemDatabaseMap = new UDictionaryIntTPItemHolder();
        }

        public void OnValidate()
        {
            if (itemDatabase != null)
            {
                int length = itemDatabase.Length;
                itemDatabaseMap = new UDictionaryIntTPItemHolder();
                for (int i = 0; i < length; i++)
                {
                    if (itemDatabase[i] != null)
                    {
                        int key = itemDatabase[i].Item.ID;
                        if (!itemDatabaseMap.ContainsKey(key))
                        {
                            itemDatabaseMap.Add(key, itemDatabase[i]);
                        }
                        else
                        {
                            throw new Exception("The item ID is duplicated: " + key);
                        }
                    }
                }
            }
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
    }
}
