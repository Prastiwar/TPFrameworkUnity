/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

namespace TP.Framework.Unity
{
    public interface IFadeActivator
    {
        void InitializeFade(FadeInfo fadeInfo, FadeLayout state);
        void Fade(float time, FadeInfo fadeInfo, FadeLayout state);
        void CleanUp(FadeInfo fadeInfo, FadeLayout state);
    }
}
