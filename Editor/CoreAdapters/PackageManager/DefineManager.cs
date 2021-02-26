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
    public struct DefineManager : IDefineManager
    {
        private static BuildTargetGroup _TargetGroup { get { return EditorUserBuildSettings.selectedBuildTargetGroup; } }

        public static void ToggleDefine(string define)
        {
            bool enabled = !EditorPrefs.GetBool(define, false);
            EditorPrefs.SetBool(define, enabled);
            SetDefine(define, enabled);
        }

        public static void SetDefine(string define, bool enabled)
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

        public static bool IsDefined(string define)
        {
            return GetDefines().Contains(define);
        }

        void IDefineManager.ToggleDefine(string define)
        {
            ToggleDefine(define);
        }

        void IDefineManager.SetDefine(string define, bool enabled)
        {
            SetDefine(define, enabled);
        }

        bool IDefineManager.IsDefined(string define)
        {
            return IsDefined(define);
        }

        private static bool TryAddDefine(string define)
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

        private static bool TryRemoveDefine(string define)
        {
            List<string> allDefines = GetDefines();
            if (allDefines.Contains(define))
            {
                allDefines.Remove(define);
                SetDefines(allDefines);
            }
            return false;
        }

        private static void SetDefines(List<string> allDefines)
        {
            PlayerSettings.SetScriptingDefineSymbolsForGroup(_TargetGroup, string.Join(";", allDefines.ToArray()));
        }

        private static List<string> GetDefines()
        {
            string defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(_TargetGroup);
            return defines.Split(';').ToList();
        }
    }
}
