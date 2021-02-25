/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System;
using UnityEngine;

namespace TP.Framework.Unity
{
    [Serializable]
    public class TPAnimation
    {
        public AnimationCurve Curve;
        public float Speed = 1;
        public bool AllowBreak = true;
    }
}
