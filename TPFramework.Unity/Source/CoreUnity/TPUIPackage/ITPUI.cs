/**
*   Authored by Tomasz Piowczyk
*   License: https://github.com/Prastiwar/TPFramework/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFramework 
*/

namespace TPFramework.Unity
{
    public interface ITPUI
    {
        bool IsInitialized { get; }
        bool IsActive();

        void Initialize();
        void SetActive(bool enable);
    }
}
