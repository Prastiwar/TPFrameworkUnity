/**
*   Authored by Tomasz Piowczyk
*   License: https://github.com/Prastiwar/TPFramework/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFramework 
*/

using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace TP.Framework.Internal.Editor
{
    public class EditorProjectFolder : TPProjectFolder
    {
        public ReorderableList ReorderableChildNames;

        public EditorProjectFolder(string rootName, params string[] childNames) : base(rootName, childNames)
        {
            ReorderableChildNames = new ReorderableList(ChildNames, typeof(string), false, true, true, true) {
                onAddCallback = Add,
                drawHeaderCallback = DrawHeader,
                drawElementCallback = DrawElement
            };
        }

        private void DrawElement(Rect rect, int index, bool isActive, bool isFocused)
        {
            float widthGap = 10;
            rect.y += 1;
            rect.x += widthGap;
            rect.size -= new Vector2(widthGap, 5);
            ChildNames[index] = EditorGUI.TextField(rect, ChildNames[index]);
        }

        private void DrawHeader(Rect rect)
        {
            EditorGUI.LabelField(rect, "Subfolders of: ");
            rect.x += 100;
            RootName = EditorGUI.TextField(rect, RootName);
        }

        private void Add(ReorderableList list)
        {
            ChildNames.Add("New");
        }

        public static EditorProjectFolder[] CastFromBase(TPProjectFolder[] baseFolders)
        {
            int length = baseFolders.Length;
            List<EditorProjectFolder> folders = new List<EditorProjectFolder>(length);
            for (int i = 0; i < length; i++)
            {
                folders.Add(new EditorProjectFolder(baseFolders[i].RootName, baseFolders[i].ChildNames.ToArray()));
            }
            return folders.ToArray();
        }
    }
}
