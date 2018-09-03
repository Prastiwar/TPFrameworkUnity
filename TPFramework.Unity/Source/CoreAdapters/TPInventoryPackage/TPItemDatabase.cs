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
    [Serializable]
    public class TPItemDatabase : ISerializationCallbackReceiver
    {
        private Dictionary<int, TPItemHolder> itemDatabaseMap;

        [SerializeField] private TPItemHolder[] itemDatabase;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TPItemHolder GetItemHolder(int id)
        {
            return itemDatabaseMap[id];
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            if (itemDatabaseMap != null)
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
            if (itemDatabase != null)
            {
                int length = itemDatabase.Length;
                itemDatabaseMap = new Dictionary<int, TPItemHolder>(length);
                for (int i = 0; i < length; i++)
                {
                    if (itemDatabase[i] != null)
                    {
                        try
                        {
                            itemDatabaseMap.Add(itemDatabase[i].Item.ID, itemDatabase[i]);
                        }
                        catch (Exception)
                        {
                            throw new Exception("" +itemDatabase[i].Item.ID);
                            throw;
                        }
                    }
                }
            }
        }
    }
}
