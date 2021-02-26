/**
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
    public class ItemScriptable : ScriptableObject, ISerializationCallbackReceiver
    {
        public Sprite Icon;
        [NonSerialized] public ItemModel Item;

        [SerializeField] private SerializedItem item;

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
                item = new ItemModel(0, 0);
            }
        }

        public override bool Equals(object other)
        {
            ItemScriptable otherHolder = other as ItemScriptable;
            return otherHolder != null 
                ? Item.ID == otherHolder.Item.ID 
                : this == null;
        }

        public override int GetHashCode()
        {
            var hashCode = 1816100322;
            hashCode = hashCode * -1521134295 + EqualityComparer<Sprite>.Default.GetHashCode(Icon);
            hashCode = hashCode * -1521134295 + EqualityComparer<ItemModel>.Default.GetHashCode(Item);
            hashCode = hashCode * -1521134295 + EqualityComparer<SerializedItem>.Default.GetHashCode(item);
            return hashCode;
        }
    }
}
