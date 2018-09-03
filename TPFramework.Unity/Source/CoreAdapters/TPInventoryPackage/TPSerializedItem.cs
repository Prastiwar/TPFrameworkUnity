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
    [Serializable]
    public class TPSerializedItem
    {
        [SerializeField] private int id;
        [SerializeField] private int type;
        [SerializeField] private string itemName;
        [SerializeField] private string description;
        [SerializeField] private double worth;
        [SerializeField] private int amountStack;
        [SerializeField] private int maxStack;
        [SerializeField] private float weight;
        [SerializeField] private TPModifier[] modifiers;

        public static implicit operator TPItem(TPSerializedItem load)
        {
            return load == null ? null : new TPItem(load.id, load.type) {
                Name = load.itemName,
                Description = load.description,
                Worth = load.worth,
                AmountStack = load.amountStack,
                MaxStack = load.maxStack,
                Weight = load.weight,
                Modifiers = load.modifiers?.Cast<ITPModifier>().ToArray()
            };
        }

        public static implicit operator TPSerializedItem(TPItem save)
        {
            return save == null ? null : new TPSerializedItem {
                id = save.ID,
                type = save.Type,
                itemName = save.Name,
                description = save.Description,
                worth = save.Worth,
                amountStack = save.AmountStack,
                maxStack = save.MaxStack,
                weight = save.Weight,
                modifiers = save.Modifiers?.Cast<TPModifier>().ToArray()
            };
        }
    }
}
