using System.Collections.Generic;
using System.Linq;
using UnityEditor;

[InitializeOnLoad]
public class SceneLoader : EditorWindow
{
    static SceneLoader()
    {
        string[] scenesGUIDs = AssetDatabase.FindAssets("t:Scene");
        string[] scenesPaths = scenesGUIDs.Select(AssetDatabase.GUIDToAssetPath).ToArray();
        EditorBuildSettings.scenes = GetScenes(scenesPaths);
    }

    private static EditorBuildSettingsScene[] GetScenes(string[] scenePaths)
    {
        int length = scenePaths.Length;
        List<EditorBuildSettingsScene> scenes = new List<EditorBuildSettingsScene>(length);
        for (int i = 0; i < length; i++)
        {
            scenes.Add(new EditorBuildSettingsScene(scenePaths[i], true));
        }
        return scenes.ToArray();
    }
}
