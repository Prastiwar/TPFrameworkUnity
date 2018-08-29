using System.IO;
using System.Threading.Tasks;
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
        Parallel.ForEach(files, x => File.Delete(x));
    }

    private static void CopyFiles(string[] files, string destinationPath)
    {
        Parallel.ForEach(files, x => File.Copy(x, Path.Combine(destinationPath, Path.GetFileName(x))));
    }
}
