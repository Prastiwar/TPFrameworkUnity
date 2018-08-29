#if UNITY_EDITOR
using System;
using System.Linq;
using TPFramework.Core;
using UnityEditor;
using UnityEngine;

namespace TPFramework.Internal
{
    internal interface IOverridePackage
    {
        string Name { get; }
        Func<bool> OnReload { get; }
    }

    [InitializeOnLoad]
    internal class TPUnityPackageManager : TPPackageManager
    {
        internal const string MENU = "TPFramework/";

        private static readonly IOverridePackage[] overridePackages = new IOverridePackage[]{
            new TPSettingsPackage()
        };

        private static bool HasTMPro {
            get {
                return AppDomain.CurrentDomain.GetAssemblies().Any(assembly => assembly.GetTypes().Any(typ => typ.HasNamespace("TMPro")));
            }
        }

        static TPUnityPackageManager()
        {
            Manager = new TPPackageManager(new TPDefineManager(), null);
            Manager.InitializePackages(TPFrameworkInfo.GetExistingPackagePaths, false);
            OverridePackages();
            Reload();
            ((TPDefineManager)Manager.DefineManager).OnLoad();
        }

        public TPUnityPackageManager(ITPDefineManager defineManager, TPPackage[] packages) : base(defineManager, packages) { }

        [MenuItem(MENU + TPDefineInfo.MenuMessage.TPReload, priority = 0)]
        private static void Reload()
        {
            if (!HasTMPro)
            {
                Debug.LogError("You don't have TextMeshPro installed. You can download it from <color=cyan> https://assetstore.unity.com/packages/essentials/beta-projects/textmesh-pro-84126 </color>");
            }
            Manager.ReloadPackages();
        }

        private static void OverridePackages()
        {
            int length = overridePackages.Length;
            for (int i = 0; i < length; i++)
            {
                int index = Manager.Packages.FindIndex(x => x.FileName.Contains(overridePackages[i].Name));
                if (index >= 0)
                {
                    Manager.Packages[index] = new TPPackage(Manager.Packages[index].FileName, overridePackages[i].OnReload);
                }
            }
        }
    }



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

    internal class TPTooltipPackage
    {
        [MenuItem(TPUnityPackageManager.MENU + TPDefineInfo.MenuMessage.TPTooltipSafeChecks, priority = 120)]
        private static void ToggleSafeChecks() { TPUnityPackageManager.Manager.DefineManager.ToggleDefine(TPDefineInfo.TPTooltipSafety); }
    }

    internal class TPUIPackage
    {
        [MenuItem(TPUnityPackageManager.MENU + TPDefineInfo.MenuMessage.TPUISafeChecks, priority = 160)]
        private static void ToggleSafeChecks() { TPUnityPackageManager.Manager.DefineManager.ToggleDefine(TPDefineInfo.TPUISafety); }
    }
}
#endif
