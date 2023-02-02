using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ExporterSceneUtility
{
    /// <summary>
    /// Deletes the directory files.
    /// </summary>
    /// <param name="directory">Directory.</param>
    public static void DeleteDirectoryFiles(string directory)
    {
        string[] files = Directory.GetFiles(directory);
        foreach (string fn in files)
        {
            File.Delete(directory + fn);
        } 
    }

}
