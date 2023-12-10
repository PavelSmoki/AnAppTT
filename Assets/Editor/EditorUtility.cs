using UnityEditor;
using UnityEngine;

namespace Editor.Editor
{
    public static class EditorUtility
    {
        [MenuItem("Game/Clear Data")]
        public static void ClearData()
        {
            Caching.ClearCache();
            PlayerPrefs.DeleteAll();
            FileUtil.DeleteFileOrDirectory(Application.persistentDataPath);
        }
    }
}
