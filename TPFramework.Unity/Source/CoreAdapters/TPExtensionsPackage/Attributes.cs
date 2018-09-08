﻿/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System;
using UnityEngine;

namespace TP.Framework.Unity
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true)]
    public class InspectorReadOnlyAttribute : PropertyAttribute { }
}
