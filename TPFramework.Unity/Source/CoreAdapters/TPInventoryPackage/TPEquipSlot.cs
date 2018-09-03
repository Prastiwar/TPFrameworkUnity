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
    [Serializable]
    public class TPEquipSlot : TPItemSlot, ITPEquipSlot<Core.TPItem>, ISerializationCallbackReceiver
    {

        void ISerializationCallbackReceiver.OnBeforeSerialize() { }
        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            itemSlot = this;
        }

        public static implicit operator Core.TPEquipSlot(TPEquipSlot slot)
        {
            Core.TPItem item = slot.storedItem;
            return new Core.TPEquipSlot(slot.type, item);
        }

        public static implicit operator Core.TPItemSlot(TPEquipSlot slot)
        {
            Core.TPItem item = slot.storedItem;
            return new Core.TPItemSlot(slot.type, item);
        }
    }
}
