/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System;
using UnityEngine;

namespace TP.Framework.Unity
{
    /// <summary> Struct holds all settings that can be changed </summary>
    [Serializable]
    public struct TPQualityLevel
    {
        public bool VSync;
        public bool FullScreen;
        public int Antialiasing;
        public int MasterTextureLimit;
        public ShadowQuality ShadowQuality;
        public ShadowResolution ShadowResolution;
        public AnisotropicFiltering AnisotropicFiltering;
        public Resolution Resolution;
    }
}
