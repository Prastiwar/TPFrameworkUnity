/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System;
using System.Linq;
using UnityEngine;

namespace TP.Framework.Unity
{
    /// <summary> TPItem used for serialization </summary>
    [Serializable]
    public class SerializedItem
    {
        [SerializeField] private int id;
        [SerializeField] private int type;
        [SerializeField] private string itemName;
        [SerializeField] private string description;
        [SerializeField] private double worth;
        [SerializeField] private int amountStack;
        [SerializeField] private int maxStack;
        [SerializeField] private float weight;
        [SerializeField] private AttributeModifier[] modifiers;

        public static implicit operator ItemModel(SerializedItem load)
        {
            return load == null ? null : new ItemModel(load.id, load.type) {
                Name = load.itemName,
                Description = load.description,
                Worth = load.worth,
                AmountStack = load.amountStack,
                MaxStack = load.maxStack,
                Weight = load.weight,
                Modifiers = load.modifiers == null ? null : ToBaseModifiers(load.modifiers)
            };
        }

        public static implicit operator SerializedItem(ItemModel save)
        {
            return save == null ? null : new SerializedItem {
                id = save.ID,
                type = save.Type,
                itemName = save.Name,
                description = save.Description,
                worth = save.Worth,
                amountStack = save.AmountStack,
                maxStack = save.MaxStack,
                weight = save.Weight,
                modifiers = save.Modifiers == null ? null : ToModifiers(save.Modifiers)
            };
        }

        private static Framework.AttributeModifier[] ToBaseModifiers(AttributeModifier[] modifiers)
        {
            int length = modifiers.Length;
            Framework.AttributeModifier[] newModifiers = new Framework.AttributeModifier[length];
            for (int i = 0; i < length; i++)
            {
                newModifiers[i] = new Framework.AttributeModifier(modifiers[i].Type, modifiers[i].Value, modifiers[i].Priority, modifiers[i].Source);
            }
            return newModifiers;
        }

        private static AttributeModifier[] ToModifiers(Framework.AttributeModifier[] modifiers)
        {
            int length = modifiers.Length;
            AttributeModifier[] newModifiers = new AttributeModifier[length];
            for (int i = 0; i < length; i++)
            {
                newModifiers[i] = new AttributeModifier(modifiers[i].Type, modifiers[i].Value, modifiers[i].Priority, modifiers[i].Source);
            }
            return newModifiers;
        }
    }
}
