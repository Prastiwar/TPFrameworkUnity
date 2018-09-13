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
    [CustomPropertyDrawer(typeof(InspectorValidateAttribute))]
    public class InspectorValidatePropertyDrawer : PropertyDrawer
    {
    }
}
