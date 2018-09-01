/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System.Linq;
using UnityEngine;

namespace TPFramework.Unity
{
    [CreateAssetMenu(menuName = "TP/TPInventory/TPItem", fileName = "TPItem")]
    public class TPItem : ScriptableObject, ISerializationCallbackReceiver
    {
        private Core.TPItem item;

        [SerializeField] private Sprite icon;
        [SerializeField] private int id;
        [SerializeField] private int type;
        [SerializeField] private string name;
        [SerializeField] private string description;
        [SerializeField] private double worth;
        [SerializeField] private int amountStack;
        [SerializeField] private int maxStack;
        [SerializeField] private float weight;
        [SerializeField] private TPModifier[] modifiers;

        public void Use()
        {
            item.Use();
        }

        public void Stack(int count = 1)
        {
            item.Stack(count);
        }
        
        void ISerializationCallbackReceiver.OnBeforeSerialize() { }
        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            item = this;
        }

        public static implicit operator Core.TPItem(TPItem itemSO)
        {
            return itemSO == null ? null : new Core.TPItem(itemSO.id, itemSO.type) {
                Name = itemSO.name,
                Description = itemSO.description,
                Worth = itemSO.worth,
                AmountStack = itemSO.amountStack,
                MaxStack = itemSO.maxStack,
                Weight = itemSO.weight,
                Modifiers = itemSO.modifiers.Cast<Core.ITPModifier>().ToArray()
            };
        }
    }
}
