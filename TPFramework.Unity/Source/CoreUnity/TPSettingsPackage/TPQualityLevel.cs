using System;
using UnityEngine;

namespace TPFramework.Unity
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
