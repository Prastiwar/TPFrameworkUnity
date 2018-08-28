/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity 
*/

using System;
using System.Collections.Generic;
using TPFramework.Core;
using UnityEngine;

namespace TPFramework.Unity
{
    [Serializable]
    public class TPModifierList : TPModifierList<TPModifier>, ISerializationCallbackReceiver
    {
        [SerializeField] private List<TPModifier> modifiers;

        public TPModifierList(Action onChanged, int capacity = 10) : base(onChanged, capacity) { }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            modifiers = Modifiers;
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            Modifiers = modifiers;
        }
    }
}
