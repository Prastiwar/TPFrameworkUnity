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
    public class TPEquipSlot : MonoBehaviour, ISerializationCallbackReceiver
    {
        private Core.TPItemEquipSlot equipSlot;

        [SerializeField] private int type;
        [SerializeField] private TPItem storedItem;

        void ISerializationCallbackReceiver.OnBeforeSerialize() { }
        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            equipSlot = this;
        }

        public static implicit operator Core.TPItemEquipSlot(TPEquipSlot slot)
        {
            Core.TPItem item = slot.storedItem;
            return new Core.TPItemEquipSlot(slot.type, item);
        }
    }
}
