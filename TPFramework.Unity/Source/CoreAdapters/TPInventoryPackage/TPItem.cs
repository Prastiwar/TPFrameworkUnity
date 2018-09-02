/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System;
using System.Linq;
using TPFramework.Core;
using UnityEngine;

namespace TPFramework.Unity
{
    [CreateAssetMenu(menuName = "TP/TPInventory/TPItem", fileName = "TPItem")]
    public class TPItem : ScriptableObject, ITPItem, ISerializationCallbackReceiver
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

        public Action OnUsed { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }
        public Action OnFailUsed { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }
        public Action OnMoved { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }
        public Action OnFailMoved { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }
        public Action OnEquipped { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }
        public Action OnUnEquipped { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }

        public Sprite Icon { get { return icon; } }
        public int ID { get { return id; } }
        public int Type { get { return type; } }
        public string Name { get { return name; } }
        public string Description { get { throw new NotImplementedException(); } }
        public double Worth { get { throw new NotImplementedException(); } }
        public int AmountStack { get { throw new NotImplementedException(); } }
        public int MaxStack { get { throw new NotImplementedException(); } }
        public float Weight { get { throw new NotImplementedException(); } }
        public ITPModifier[] Modifiers { get { throw new NotImplementedException(); } }

        public bool Use()
        {
            return item.Use();
        }

        public bool Stack(int count = 1)
        {
            return item.Stack(count);
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
                Modifiers = itemSO.modifiers.Cast<ITPModifier>().ToArray()
            };
        }

        public static implicit operator TPItem(Core.TPItem item)
        {
            return item ?? null;
        }
    }
}
