/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

namespace TP.Framework.Unity.UI
{
    public interface IUIActivator
    {
        bool IsInitialized { get; }
        bool IsActive();

        void Initialize();
        void SetActive(bool enable);
    }
}
