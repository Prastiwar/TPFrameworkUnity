/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System;
using UnityEngine;

namespace TPFramework.Unity
{
    [Serializable]
    public class TPItemSlot : MonoBehaviour, ISerializationCallbackReceiver
    {
        private Core.TPItemSlot itemSlot;

        [SerializeField] private int type;
        [SerializeField] private TPItem storedItem;

        void ISerializationCallbackReceiver.OnBeforeSerialize() { }
        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            itemSlot = this;
        }

        public static implicit operator Core.TPItemSlot(TPItemSlot slot)
        {
            Core.TPItem item = slot.storedItem;
            return new Core.TPItemEquipSlot(slot.type, item);
        }
    }
}
