﻿/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

namespace TP.Framework.Internal
{
    public struct TPUnityFrameworkInfo
    {
        public const int PackagesLength = 7;
        public const string TPCoreNamespace = "TP.Framework.Unity";

        public static string[] GetTPPackageNames {
            get {
                return new string[PackagesLength]{
                    "TPSettingsPackage",
                    "TPTooltipPackage",
                    "TPEditorPackage",
                    "TPAudioPackage",
                    "TPAnimPackage",
                    "TPFadePackage",
                    "TPUIPackage",
                };
            }
        }
    }
}
