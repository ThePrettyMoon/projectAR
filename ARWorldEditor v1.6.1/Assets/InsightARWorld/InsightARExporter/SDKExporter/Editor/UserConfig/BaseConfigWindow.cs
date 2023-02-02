using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace UserConfig
{
    public class BaseConfigWindow : EditorWindow
    {
        // protected static GUIStyle redStyle = new GUIStyle(EditorStyles.label);

		protected static void ConfigDialog()
		{
            // redStyle.normal.textColor = Color.red;
		}
		protected static string GetTempFilePrefix(string tempDictionary)
		{
			string scenePath = UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene().path;
			if (string.Empty == scenePath)
				return tempDictionary;
			else
			{
				string sceneGUID = AssetDatabase.AssetPathToGUID(scenePath);
				return Path.Combine(tempDictionary, sceneGUID);
			}
		}

		protected static void WriteFile(string filename, string content)
		{
			string dir = Path.GetDirectoryName(filename);
			if (!Directory.Exists(dir))
				Directory.CreateDirectory(dir);
			StreamWriter sw = new StreamWriter(filename);
			using (sw)
			{
				sw.WriteLine(content);
			}
		}

		protected static void WriteFile(string filename, byte[] content)
		{
			string dir = Path.GetDirectoryName(filename);
			if (!Directory.Exists(dir))
				Directory.CreateDirectory(dir);

			System.IO.File.WriteAllBytes(filename, content);
		}

    }
}
