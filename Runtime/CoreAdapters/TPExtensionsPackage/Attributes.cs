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
    public class EditorAttribute : PropertyAttribute { }

    public class CallbackAttribute : EditorAttribute
    {
        public string CallbackName { get; }

        public CallbackAttribute(string callbackName)
        {
            CallbackName = callbackName;
        }
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class InspectorReadOnlyAttribute : EditorAttribute { }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class InspectorBackgroundAttribute : EditorAttribute
    {
        public Color Color { get; }

        public InspectorBackgroundAttribute(float r, float g, float b, float a = 1)
        {
            Color = new Color(r, g, b, a);
        }
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class RequiredAttribute : EditorAttribute
    {
        public string Message { get; }

        public RequiredAttribute(string message = null)
        {
            Message = message;
        }
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class OnValueChangedAttribute : CallbackAttribute
    {
        public OnValueChangedAttribute(string callbackName) : base(callbackName) { }
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class InspectorValidateAttribute : CallbackAttribute
    {
        public string Message { get; }

        public InspectorValidateAttribute(string callbackName, string message = null) : base(callbackName)
        {
            Message = message;
        }
    }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    public class InspectorButtonAttribute : EditorAttribute
    {
        public string ButtonName { get; }

        public InspectorButtonAttribute(string buttonName = null)
        {
            ButtonName = buttonName;
        }
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class InspectorHideAttribute : EditorAttribute
    {
        public bool DisableOnly { get; }

        public string[] TrueConditions { get; }
        public string[] FalseConditions { get; }

        public InspectorHideAttribute(string boolStatementsString, bool disableOnly = false)
        {
            DisableOnly = disableOnly;

            List<string> trueConditions = new List<string>(1);
            List<string> falseConditions = new List<string>(1);
            string[] boolTexts = boolStatementsString.Split(',');

            int length = boolTexts.Length;
            for (int i = 0; i < length; i++)
            {
                string boolText = boolTexts[i].Trim(null);
                if (boolText[0] == '!')
                {
                    falseConditions.Add(boolText.Remove(0, 1));
                }
                else
                {
                    trueConditions.Add(boolText);
                }
            }
            TrueConditions = trueConditions.ToArray();
            FalseConditions = falseConditions.ToArray();
        }
    }
}
