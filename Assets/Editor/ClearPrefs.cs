using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class ClearPrefs
    {
        [MenuItem("Tools/Clear prefs")]
        public static void Clear()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            
            Debug.Log("Clear prefs");
        }
    }
}
