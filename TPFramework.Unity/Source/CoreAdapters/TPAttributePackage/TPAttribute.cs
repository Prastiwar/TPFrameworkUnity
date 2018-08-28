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
    public class TPAttribute : TPAttribute<TPModifierList, TPModifier>, ISerializationCallbackReceiver
    {
        [SerializeField] private float baseValue;
        [SerializeField] private TPModifierList modifiers;
        
        public TPAttribute()
        {
            Modifiers = new TPModifierList(Recalculate); // we need to assign attribute to its modifier list
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            baseValue = BaseValue;
            modifiers = Modifiers;
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            BaseValue = baseValue;
            Modifiers = modifiers;
        }
    }
}
