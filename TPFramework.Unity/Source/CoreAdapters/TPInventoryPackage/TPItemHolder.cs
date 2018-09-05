/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System;
using TPFramework.Core;
using UnityEngine;

namespace TPFramework.Unity
{
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
            if (otherHolder != null)
            {
                return Item.ID == otherHolder.Item.ID;
            }
            return this == null;
        }
    }
}
