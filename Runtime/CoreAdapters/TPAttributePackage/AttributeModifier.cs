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
    public struct AttributeModifier : ITPModifier
    {
        [SerializeField] private float value;
        [SerializeField] private ModifierType type;
        [SerializeField] private int priority;

        public object Source { get; set; }

        public float Value {
            get { return value; }
            private set { this.value = value; }
        }

        public ModifierType Type {
            get { return type; }
            private set { type = value; }
        }

        public int Priority {
            get { return priority; }
            private set { priority = value; }
        }

        public AttributeModifier(ModifierType type, float value, object source) : this(type, value, (int)type, source) { }
        public AttributeModifier(ModifierType tye, float value, int priority) : this(tye, value, priority, null) { }
        public AttributeModifier(ModifierType type, float value) : this(type, value, (int)type, null) { }
        public AttributeModifier(ModifierType type, float value, int priority, object source)
        {
            this.priority = priority;
            this.value = value;
            this.type = type;
            Source = source;
        }

        public override string ToString()
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder(32);
            builder.Append("TPModifier { ");
            builder.Append("Type: ");
            builder.Append(Type);
            builder.Append("; Priority: ");
            builder.Append(Priority);
            builder.Append("; Value: ");
            builder.Append(Value);
            builder.Append("; Source: ");
            builder.Append(Source);
            builder.Append("; }");
            return builder.ToString();
        }

        public override bool Equals(object obj)
        {
            return obj is AttributeModifier mod ? mod == this : false;
        }

        public override int GetHashCode()
        {
            var hashCode = 328636640;
            hashCode = hashCode * -1521134295 + System.Collections.Generic.EqualityComparer<object>.Default.GetHashCode(Source);
            hashCode = hashCode * -1521134295 + Value.GetHashCode();
            hashCode = hashCode * -1521134295 + Priority.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(AttributeModifier c1, AttributeModifier c2)
        {
            return c1.Value == c2.Value && c1.Type == c2.Type && c1.Priority == c2.Priority && c1.Source == c2.Source;
        }

        public static bool operator !=(AttributeModifier c1, AttributeModifier c2)
        {
            return !(c1 == c2);
        }

        public static implicit operator Framework.TPModifier(AttributeModifier mod)
        {
            return new Framework.TPModifier(mod.type, mod.value, mod.priority, mod.Source);
        }

        public static implicit operator AttributeModifier(Framework.TPModifier coreMod)
        {
            return new AttributeModifier(coreMod.Type, coreMod.Value, coreMod.Priority, coreMod.Source);
        }
    }
}
