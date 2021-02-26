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
    [Serializable]
    public class ModifierList : TPModifierList<AttributeModifier>, ISerializationCallbackReceiver
    {
        [SerializeField] private List<AttributeModifier> modifiers;

        public ModifierList(Action onChanged, int capacity = 10) : base(onChanged, capacity)
        {
            modifiers = new List<AttributeModifier>();
            Modifiers = modifiers;
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize() { }
        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            Modifiers = modifiers;
        }
    }
}
