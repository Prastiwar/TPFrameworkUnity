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
    public struct TPModifier : ITPModifier
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

        public TPModifier(ModifierType type, float value, object source) : this(type, value, (int)type, source) { }
        public TPModifier(ModifierType tye, float value, int priority) : this(tye, value, priority, null) { }
        public TPModifier(ModifierType type, float value) : this(type, value, (int)type, null) { }
        public TPModifier(ModifierType type, float value, int priority, object source)
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

        public static bool operator ==(TPModifier c1, TPModifier c2)
        {
            return c1.Value == c2.Value && c1.Type == c2.Type && c1.Priority == c2.Priority && c1.Source == c2.Source;
        }

        public static bool operator !=(TPModifier c1, TPModifier c2)
        {
            return !(c1 == c2);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
