/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using UnityEditor;

namespace TP.Framework.Unity.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(AchievementScriptable))]
    public class TPAchievementEditor : TPScriptlessEditor<AchievementScriptable> { }
}
