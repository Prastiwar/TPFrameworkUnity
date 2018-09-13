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
    public class TPEditorAttribute : PropertyAttribute { }

    [AttributeUsage(AttributeTargets.Field, Inherited = true)]
    public class InspectorReadOnlyAttribute : TPEditorAttribute { }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class RequiredAttribute : TPEditorAttribute
    {
        public string Message { get; }

        public RequiredAttribute(string message = null)
        {
            Message = message;
        }
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class OnValueChangedAttribute : TPEditorAttribute
    {
        public string CallbackName { get; }

        public OnValueChangedAttribute(string callbackName)
        {
            CallbackName = callbackName;
        }
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class InspectorValidateAttribute : TPEditorAttribute
    {
        public string CallbackName { get; }
        public string Message { get; }

        public InspectorValidateAttribute(string callbackName, string message = null)
        {
            Message = message;
            CallbackName = callbackName;
        }
    }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    public class InspectorButtonAttribute : TPEditorAttribute
    {
        public string ButtonName { get; }

        public InspectorButtonAttribute(string buttonName = null)
        {
            ButtonName = buttonName;
        }
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class InspectorHideAttribute : TPEditorAttribute
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
