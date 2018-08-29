using System.IO;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class ImportDLL : EditorWindow
{
    private const string unityPluginsFolder = "TPFramework/Plugins";
    private const string dllFolder = "TPFramework.Unity/bin/Release";
    private const string dllPattern = "TPFramework.*.dll";

    private static string[] dllFiles = null;
    private static string[] pluginsFiles = null;

    private static string repoPath;
    private static string dllPath;
    private static string pluginsPath;
    
    [MenuItem("ImportDLL/Reimport")]
    private static void Import()
    {
        RemoveFiles(pluginsFiles);
        CopyFiles(dllFiles, pluginsPath);
        AssetDatabase.Refresh();
    }

    static ImportDLL()
    {
        Init();
    }

    private static void Init()
    {
        repoPath = Application.dataPath.Substring(0, Application.dataPath.IndexOf("/Assets"));
        repoPath = Application.dataPath.Substring(0, repoPath.LastIndexOf("/") + 1);

        dllPath = Path.Combine(repoPath, dllFolder);
        pluginsPath = Path.Combine(Application.dataPath, unityPluginsFolder);

        dllFiles = Directory.GetFiles(dllPath, dllPattern);
        pluginsFiles = Directory.GetFiles(pluginsPath, dllPattern);
    }

    private static void RemoveFiles(string[] files)
    {
        int length = files.Length;
        for (int i = 0; i < length; i++)
        {
            File.Delete(files[i]);
        }
    }

    private static void CopyFiles(string[] files, string destinationPath)
    {
        int length = files.Length;
        for (int i = 0; i < length; i++)
        {
            string destFileName = Path.Combine(destinationPath, Path.GetFileName(files[i]));
            File.Copy(files[i], destFileName);
        }
    }
}
