/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace TP.Framework.Internal
{
    [InitializeOnLoad]
    internal class TPUnityPackageManager : TPPackageManager
    {
        internal const string MENU = "TPFramework/";

        private static readonly IOverridePackage[] overridePackages = new IOverridePackage[]{
            new TPSettingsPackage(),
        };

        private static bool HasTMPro {
            get {
                return AppDomain.CurrentDomain.GetAssemblies().Any(assembly => assembly.GetTypes().Any(typ => typ.HasNamespace("TMPro")));
            }
        }

        public static string[] GetAllTPPackageNames {
            get {
                List<string> names = new List<string>(TPFrameworkInfo.GetTPPackageNames);
                names.AddRange(TPUnityFrameworkInfo.GetTPPackageNames);
                return names.ToArray();
            }
        }

        public TPUnityPackageManager(IDefineManager defineManager, TPPackage[] packages) : base(defineManager, packages) { }

        static TPUnityPackageManager()
        {
            Init();
        }

        [MenuItem(MENU + DefineInfo.MenuMessage.TPReloadManager, priority = 0)]
        private static void Init()
        {
            Manager = new TPPackageManager(new DefineManager(), null);
            Manager.InitializePackages(GetAllTPPackageNames, false);
            OverridePackages();
            Reload();
        }

        [MenuItem(MENU + DefineInfo.MenuMessage.TPReloadPackages, priority = 0)]
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
            List<TPPackage> packagesList = new List<TPPackage>(Manager.Packages);
            int length = overridePackages.Length;
            for (int i = 0; i < length; i++)
            {
                TPPackage package = new TPPackage(overridePackages[i].Name, overridePackages[i].OnReload);
                int index = packagesList.FindIndex(x => x.FileName.Contains(overridePackages[i].Name));
                if (index >= 0)
                {
                    packagesList[index] = package;
                }
                else
                {
                    packagesList.Add(package);
                }
            }
            Manager.SetPackages(packagesList.ToArray());
        }
    }
}
