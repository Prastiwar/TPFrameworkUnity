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
    [AttributeUsage(AttributeTargets.Field, Inherited = true)]
    public class InspectorReadOnlyAttribute : PropertyAttribute { }

    [AttributeUsage(AttributeTargets.Field, Inherited = true)]
    public class InspectorHideAttribute : PropertyAttribute
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
