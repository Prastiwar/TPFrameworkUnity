using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace TPFramework.Internal
{
    internal struct TPDefineInfo
    {
        public const string TPTooltipSafety = "TPTooltipSafeChecks";
        public const string TPUISafety = "TPUISafeChecks";

        internal struct MenuMessage
        {
            public const string TPReloadManager = "Reload Framework Manager";
            public const string TPReloadPackages = "Reload Packages";
#if TPTooltipSafeChecks
            public const string TPTooltipSafeChecks = "Disable TPTooltip Safe Checks";
#else
            public const string TPTooltipSafeChecks = "Enable TPTooltip Safe Checks";
#endif
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
                SetDefine(TPDefineInfo.TPTooltipSafety, true);
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
