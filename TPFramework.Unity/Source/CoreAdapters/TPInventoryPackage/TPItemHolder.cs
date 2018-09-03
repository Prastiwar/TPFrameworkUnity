/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System.Runtime.CompilerServices;
using TPFramework.Core;
using UnityEngine;

namespace TPFramework.Unity
{
    [CreateAssetMenu(menuName = "TP/TPInventory/TPItem", fileName = "TPItem")]
    public class TPItemHolder : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] private Sprite icon;

        public TPItem Item;

        //[SerializeField] private TPModifier[] modifiers;

        public Sprite Icon { get { return icon; } }
        //public ITPModifier[] Modifiers { get { return null; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Use()
        {
            return Item.Use();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Stack(int count = 1)
        {
            return Item.Stack(count);
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize() { }
        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            Item = this;
        }

        public static implicit operator TPItem(TPItemHolder itemSO)
        {
            return itemSO?.Item;
        }
    }
}
