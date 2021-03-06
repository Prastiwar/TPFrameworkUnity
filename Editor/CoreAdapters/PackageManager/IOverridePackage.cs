﻿/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System;

namespace TP.Framework.Internal
{
    internal interface IOverridePackage
    {
        string Name { get; }
        Func<bool> OnReload { get; }
    }
}
