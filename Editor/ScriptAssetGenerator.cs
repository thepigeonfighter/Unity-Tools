using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
namespace GeorgeFabish.Editor
{
    public class ScriptAssetGenerator
    {
        private static readonly string _templateFolder = Path.Combine("Packages", "com.george.fabish.tools", "Editor", "ScriptTemplates");
        private static readonly string _emptyClassTemplatePath = Path.Combine(_templateFolder, "EmptyClass.txt");
        private static readonly string _emptyInterfaceTemplatePath = Path.Combine(_templateFolder, "Interface.txt");
        private static readonly string _singletonTemplatePath = Path.Combine(_templateFolder, "Singleton.txt");
#if UNITY_EDITOR
        [MenuItem("Assets/Script Templates/C# Empty Class")]
        public static void CreateEmptyClass()
        {
            string filePath = GetFilePathFromUser("Create Empty C# Class", "EmptyClass");
            CreateFile(filePath, _emptyClassTemplatePath);

        }
        [MenuItem("Assets/Script Templates/C# Interface")]
        public static void CreateEmptyInterface()
        {
            string filePath = GetFilePathFromUser("Create Empty C# Interface", "IEmpty");
            CreateFile(filePath, _emptyInterfaceTemplatePath);

        }
        [MenuItem("Assets/Script Templates/Singleton")]
        public static void CreateSingleton()
        {
            string filePath = GetFilePathFromUser("Create Singleton", "Manager");
            CreateFile(filePath, _singletonTemplatePath);
        }

        private static void CreateFile(string filePath, string templateFilePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                string template = File.ReadAllText(templateFilePath);
                template = template.Replace("$className", Path.GetFileNameWithoutExtension(filePath));
                File.WriteAllText(filePath, template);
                AssetDatabase.Refresh();
            }
        }
        private static string GetCurrentEditorFilePath()
        {
            var path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (File.Exists(path))
                path = Path.GetDirectoryName(path);
            if (string.IsNullOrEmpty(path)) path = "Assets/";
            return path;
        }
        private static string GetFilePathFromUser(string popupTitle, string defaultFileName)
        {
            string currentDir = GetCurrentEditorFilePath();
            string filePath = EditorUtility.SaveFilePanel(popupTitle, currentDir, defaultFileName, "cs");
            return filePath;
        }


#endif
    }
}