/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity 
*/

using System;
using UnityEngine;

namespace TP.Framework.Unity
{
    [Serializable]
    public class TPAttribute : TPAttribute<TPModifierList, TPModifier>, ISerializationCallbackReceiver
    {
        [SerializeField] private float startValue;
        [SerializeField] private TPModifierList modifiers;

        public TPAttribute()
        {
            modifiers = new TPModifierList(Recalculate);
            Modifiers = modifiers;
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize() { }
        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            BaseValue = startValue;
            Modifiers = modifiers;
        }
    }
}
