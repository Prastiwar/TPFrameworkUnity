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
    public class ModifiableAttribute : Framework.ModifiableAttribute<ModifierList, AttributeModifier>, ISerializationCallbackReceiver
    {
        [SerializeField] private float startValue;
        [SerializeField] private ModifierList modifiers;

        public ModifiableAttribute()
        {
            modifiers = new ModifierList(Recalculate);
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
