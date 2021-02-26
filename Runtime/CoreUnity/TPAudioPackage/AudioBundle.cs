/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity 
*/

using UnityEngine;

namespace TP.Framework.Unity
{
    [CreateAssetMenu(menuName = "TP/TPAudio/Audio Bundle", fileName = "AudioBundle")]
    public class AudioBundle : ScriptableObject
    {
        public AudioModel[] AudioObjects;
    }
}
