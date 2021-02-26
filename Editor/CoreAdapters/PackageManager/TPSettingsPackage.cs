/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System;
using System.Linq;
using UnityEngine;

namespace TP.Framework.Internal
{
    internal class TPSettingsPackage : IOverridePackage
    {
        public string Name { get { return "TPSettingsPackage"; } }
        public Func<bool> OnReload { get { return Reload; } }

        private bool Reload()
        {
            bool IsLoaded = true;
            if (!QualitySettings.names.Any(x => x == "Custom"))
            {
                Debug.LogError("No 'Custom' quality level found. Create one in Edit -> Project Settings -> Quality -> Add Quality Level");
            }
            return IsLoaded;
        }
    }
}
