/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace TP.Framework.Internal
{
    internal struct TPDefineInfo
    {
        public const string TPUISafety = "TPUISafeChecks";

        internal struct MenuMessage
        {
            public const string TPReloadManager = "Reload Framework Manager";
            public const string TPReloadPackages = "Reload Packages";
#if TPUISafeChecks
            public const string TPUISafeChecks = "Disable TPUI Safe Checks";
#else
            public const string TPUISafeChecks = "Enable TPUI Safe Checks";
#endif
        }
    }

    internal struct TPDefineManager : ITPDefineManager
    {
        private static BuildTargetGroup _TargetGroup { get { return EditorUserBuildSettings.selectedBuildTargetGroup; } }

        public void OnLoad()
        {
            if (EditorPrefs.GetBool("TP_IsFirstRun", true))
            {
                SetDefine(TPDefineInfo.TPUISafety, true);
                EditorPrefs.SetBool("TP_IsFirstRun", false);
            }
        }

        public void ToggleDefine(string define)
        {
            bool enabled = !EditorPrefs.GetBool(define, false);
            EditorPrefs.SetBool(define, enabled);
            SetDefine(define, enabled);
        }

        public void SetDefine(string define, bool enabled)
        {
            if (enabled)
            {
                TryAddDefine(define);
            }
            else
            {
                TryRemoveDefine(define);
            }
        }

        public bool IsDefined(string define)
        {
            return GetDefines().Contains(define);
        }

        private bool TryAddDefine(string define)
        {
            List<string> allDefines = GetDefines();
            if (!allDefines.Contains(define))
            {
                allDefines.Add(define);
                SetDefines(allDefines);
                return true;
            }
            return false;
        }

        private bool TryRemoveDefine(string define)
        {
            List<string> allDefines = GetDefines();
            if (allDefines.Contains(define))
            {
                allDefines.Remove(define);
                SetDefines(allDefines);
            }
            return false;
        }

        private void SetDefines(List<string> allDefines)
        {
            PlayerSettings.SetScriptingDefineSymbolsForGroup(_TargetGroup, string.Join(";", allDefines.ToArray()));
        }

        private List<string> GetDefines()
        {
            string defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(_TargetGroup);
            return defines.Split(';').ToList();
        }
    }
}
