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
    public class TPItemDatabase : ScriptableObject, ISerializationCallbackReceiver
    {
        private Dictionary<int, TPItemHolder> itemDatabaseMap;

        [SerializeField] private TPItemHolder[] itemDatabase;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void InitDatabase(TPItemHolder[] itemHolders)
        {
            itemDatabase = itemHolders;
            InitializeMap();
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
        private void InitializeMap()
        {
            int length = itemDatabase.Length;
            itemDatabaseMap = new Dictionary<int, TPItemHolder>(length);
            for (int i = 0; i < length; i++)
            {
                if (itemDatabase[i] != null)
                {
                    (itemDatabase[i] as ISerializationCallbackReceiver).OnAfterDeserialize();
                    itemDatabaseMap.Add(itemDatabase[i].Item.ID, itemDatabase[i]);
                }
            }
            itemDatabase = null;
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            if (itemDatabaseMap != null && (itemDatabase == null || itemDatabase.Length > itemDatabaseMap.Count))
            {
                int i = 0;
                int length = itemDatabaseMap.Count;
                itemDatabase = new TPItemHolder[length];
                foreach (var kvp in itemDatabaseMap)
                {
                    itemDatabase[i] = kvp.Value;
                    i++;
                }
            }
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            if (itemDatabase != null && itemDatabaseMap == null)
            {
                InitializeMap();
            }
        }
    }
}
