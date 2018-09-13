/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace TP.Framework.Unity.Editor
{
    internal struct InspectorMethodButton
    {
        public InspectorButtonAttribute Att;
        public MethodInfo Method;

        public InspectorMethodButton(InspectorButtonAttribute att, MethodInfo method)
        {
            Att = att;
            Method = method;
        }
    }

    [CustomEditor(typeof(MonoBehaviour), true)]
    public class InspectorButtonDrawer : TPEditor<MonoBehaviour>
    {
        private InspectorMethodButton[] kvpMethods;

        private void OnEnable()
        {
            var methodsList = new List<InspectorMethodButton>();
            MethodInfo[] allMethods = Target.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            int length = allMethods.Length;
            for (int i = 0; i < length; i++)
            {
                InspectorButtonAttribute buttonAtt = allMethods[i].GetSingleCustomAttribute<InspectorButtonAttribute>(true);
                if (buttonAtt != null)
                {
                    methodsList.Add(new InspectorMethodButton(buttonAtt, allMethods[i]));
                }
            }
            kvpMethods = methodsList.ToArray();
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            int length = kvpMethods.Length;
            for (int i = 0; i < length; i++)
            {
                if (GUILayout.Button(kvpMethods[i].Att.ButtonName ?? kvpMethods[i].Method.Name, GUI.skin.button, null))
                {
                    kvpMethods[i].Method.Invoke(Target, null);
                }
            }
        }
    }
}
