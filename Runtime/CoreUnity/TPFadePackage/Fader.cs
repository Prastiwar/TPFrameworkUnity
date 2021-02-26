/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System;

namespace TP.Framework.Unity
{
    [Serializable]
    public class Fader<TFade>
        where TFade : IFadeActivator
    {
        public FadeInfo Info;
        public TFade FadeActivator;
    }
}
