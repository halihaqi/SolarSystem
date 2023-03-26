using System.IO;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public static class ClearSave
    {
        [MenuItem("Tools/Saves/清空数据")]
        public static void ClearAllSave()
        {
            ClearFilesAndDirs(Application.persistentDataPath);
        }

        private static void ClearFilesAndDirs(string parentPath)
        {
            if (!Directory.Exists(parentPath)) return;
            
            foreach (string file in Directory.GetFiles(parentPath))
            {
                File.Delete(file);
            }
            foreach (string subFolder in Directory.GetDirectories(parentPath))
            {
                ClearFilesAndDirs(subFolder);
            }
            Directory.Delete(parentPath);
        }
    }
}