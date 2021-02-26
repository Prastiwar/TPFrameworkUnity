/**
*   Authored by Tomasz Piowczyk
*   License: https://github.com/Prastiwar/TPFramework/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFramework 
*/

using System.Collections.Generic;
using TP.Framework.Unity.Editor;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace TP.Framework.Internal.Editor
{
    public class TPProjectManagerWindow : EditorWindow
    {
        private string projectGameName = "GameName";
        private Vector2 scrollPos;

        private List<EditorProjectFolder> folders;
        private ReorderableList reorderableFolders;

        [MenuItem(TPUnityPackageManager.MENU + "Open Project Structure Creator", priority = 0)]
        public static void OpenProjectStructureCreator()
        {
            TPProjectManagerWindow window = GetWindow<TPProjectManagerWindow>();
            Vector2 size = new Vector2(400, 250);
            window.minSize = size;
            window.Show();
        }

        private void OnEnable()
        {
            folders = new List<EditorProjectFolder>(EditorProjectFolder.CastFromBase(Internal.TPProjectManager.DefaultFolders));
            reorderableFolders = new ReorderableList(folders, typeof(EditorProjectFolder), false, false, true, true) {
                drawElementCallback = DrawFolderElement,
                elementHeightCallback = GetFolderElementHeight,
                onAddCallback = (list) => folders.Add(new EditorProjectFolder("New", null)),
                drawHeaderCallback = (rect) => { UnityEditor.EditorGUI.LabelField(rect, "Folders structure: "); }
            };
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("How should be your project folder named?", Unity.Editor.EditorStyles.BoldCenterLabel, null);
            projectGameName = EditorGUILayout.TextField(projectGameName, options: null);

            EditorGUILayout.Space();

            Unity.Editor.EditorGUI.OnButton("Generate Folders", () => {
                Internal.TPProjectManager.CreateProjectStructure(folders.ToArray(), projectGameName, Application.dataPath);
                AssetDatabase.Refresh();
            }, null, null);
            Unity.Editor.EditorGUI.OnButton("Load default", this.OnEnable, null, null);

            EditorGUILayout.Space();

            Unity.Editor.EditorGUI.ScrollView(ref scrollPos, reorderableFolders.DoLayoutList, false, false, null);
        }

        private float GetFolderElementHeight(int index)
        {
            return folders[index].ReorderableChildNames.GetHeight() + 10;
        }

        private void DrawFolderElement(Rect rect, int index, bool isActive, bool isFocused)
        {
            int prev = UnityEditor.EditorGUI.indentLevel;
            UnityEditor.EditorGUI.indentLevel++;
            folders[index].ReorderableChildNames.DoList(rect);
            UnityEditor.EditorGUI.indentLevel = prev;
        }
    }
}
