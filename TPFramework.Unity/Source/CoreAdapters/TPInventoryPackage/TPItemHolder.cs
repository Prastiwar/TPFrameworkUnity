﻿/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System;
using System.Collections.Generic;
using UnityEngine;

namespace TP.Framework.Unity
{
    /// <summary> ScriptableObject to hold TPItem </summary>
    [CreateAssetMenu(menuName = "TP/TPInventory/TPItem", fileName = "TPItem")]
    public class TPItemHolder : ScriptableObject, ISerializationCallbackReceiver
    {
        public Sprite Icon;
        [NonSerialized] public TPItem Item;

        [SerializeField] private TPSerializedItem item;

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            item = Item;
            PreventNull();
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            PreventNull();
            Item = item;
        }

        private void PreventNull()
        {
            if (item == null)
            {
                item = new TPItem(0, 0);
            }
        }

        public override bool Equals(object other)
        {
            TPItemHolder otherHolder = other as TPItemHolder;
            return otherHolder != null 
                ? Item.ID == otherHolder.Item.ID 
                : this == null;
        }

        public override int GetHashCode()
        {
            var hashCode = 1816100322;
            hashCode = hashCode * -1521134295 + EqualityComparer<Sprite>.Default.GetHashCode(Icon);
            hashCode = hashCode * -1521134295 + EqualityComparer<TPItem>.Default.GetHashCode(Item);
            hashCode = hashCode * -1521134295 + EqualityComparer<TPSerializedItem>.Default.GetHashCode(item);
            return hashCode;
        }
    }
}
