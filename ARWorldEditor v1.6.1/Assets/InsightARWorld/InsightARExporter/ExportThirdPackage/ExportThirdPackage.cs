
#if UNITY_EDITOR
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;

public class ExportThirdPackageWindow : EditorWindow
{
    static string rootDirectory = "Assets/InsightARWorld/InsightARExporter";
    static string utilityDirectory = "Assets/InsightARWorld/InsightARExporter/Utility";
    static string dotfuscatorPath = "Assets/InsightARWorld/InsightARExporter/ExportThirdPackage/dotfuscatorCLI.exe";
    static string compiledDllFileName = "InsightEditor.dll";

    static string RootPath=> Application.dataPath + "/../" + rootDirectory;
    static string UtilityPath => Application.dataPath + "/../" + utilityDirectory;


    static string CompiledDllPath
    {
        get
        {
            return Application.dataPath + "/../Temp/";
        }
    }

    public string outputPath = @"C:\Users\ezxr.wanghaishan\Desktop\outEditor";

    [MenuItem("Exporter/ExportThirdPackage",false,-100)]
    public static void ShowWindow()
    {
        ExportThirdPackageWindow window = (ExportThirdPackageWindow)EditorWindow.GetWindow(typeof(ExportThirdPackageWindow), false, "ExportThirdPackage", true);//创建窗口
        window.Show();
    }

    void OnGUI()
    {
        SerializedObject serialObj = new SerializedObject(this);

        SerializedProperty outputPathProp = serialObj.FindProperty("outputPath");

        EditorGUILayout.PropertyField(outputPathProp, true);

        serialObj.ApplyModifiedProperties();

        if (GUILayout.Button("CreatePackage"))
        {
            CreatePackage();
        }
    }

    void CreatePackage()
    {
        if (Directory.Exists(outputPath + "/" + rootDirectory))
        {
            Directory.Delete(outputPath + "/" + rootDirectory, true);
        }

        DirectoryCopy(rootDirectory, outputPath + "/" + rootDirectory, true, ".cs");

        CompileDllAndCopy();
    }

    bool DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs, params string[] filteredExtensions)
    {
        // Get the subdirectories for the specified directory.
        DirectoryInfo dir = new DirectoryInfo(sourceDirName);

        if (!dir.Exists)
        {
            throw new DirectoryNotFoundException(
                "Source directory does not exist or could not be found: "
                + sourceDirName);
        }

        DirectoryInfo[] dirs = dir.GetDirectories();


        bool hasRootFile = false;

        // Get the files in the directory and copy them to the new location.
        FileInfo[] files = dir.GetFiles();
        foreach (FileInfo file in files)
        {
            if (file.Extension == ".meta")
            {
                continue;
            }

            bool filteredFile = false;
            foreach (string filteredExtension in filteredExtensions)
            {
                if (file.Name.EndsWith(filteredExtension))
                {
                    filteredFile = true;
                    break;
                }
            }

            if (!filteredFile)
            {
                if (!Directory.Exists(destDirName))
                {
                    Directory.CreateDirectory(destDirName);
                }

                string temppath = Path.Combine(destDirName, file.Name);
                File.Copy(file.FullName, temppath);
                File.Copy(file.FullName + ".meta", temppath + ".meta");

                hasRootFile = true;
            }
        }


        bool hasChildFile = false;

        // If copying subdirectories, copy them and their contents to new location.
        if (copySubDirs)
        {
            foreach (DirectoryInfo subdir in dirs)
            {
                string temppath = Path.Combine(destDirName, subdir.Name);
                if(DirectoryCopy(subdir.FullName, temppath, copySubDirs, filteredExtensions))
                    hasChildFile = true;
            }
        }

        bool hasFile = hasRootFile || hasChildFile;
        if(hasFile)
        {
            File.Copy(sourceDirName + ".meta", destDirName + ".meta",true);
            //Debug.Log("copy meta " + destDirName + ".meta");
        }

        return hasFile;
    }


    void CompileDllAndCopy()
    {

        if (!Directory.Exists(RootPath))
        {
            EditorUtility.DisplayDialog("Compile InsightEditor Failed", "InsightEditor source folder not exist", "ok");
        }

        List<string> references = new List<string> {
            // Path.Combine(EditorApplication.applicationContentsPath, "Managed/UnityEngine.dll"),
            // Path.Combine(EditorApplication.applicationContentsPath, "Managed/UnityEditor.dll"),
            //Path.Combine(EditorApplication.applicationContentsPath, "UnityExtensions/Unity/GUISystem/UnityEngine.UI.dll")
            Application.dataPath + "/../Library/ScriptAssemblies/UnityEngine.UI.dll",
            Path.Combine(EditorApplication.applicationContentsPath, "MonoBleedingEdge/lib/mono/4.7.1-api/mscorlib.dll"),
            Path.Combine(EditorApplication.applicationContentsPath, "MonoBleedingEdge/lib/mono/4.7.1-api/System.Xml.dll"),
            Path.Combine(EditorApplication.applicationContentsPath, "MonoBleedingEdge/lib/mono/4.7.1-api/System.Core.dll"),
            Path.Combine(EditorApplication.applicationContentsPath, "MonoBleedingEdge/lib/mono/4.7.1-api/System.Drawing.dll"),
            Path.Combine(EditorApplication.applicationContentsPath, "MonoBleedingEdge/lib/mono/4.7.1-api/System.dll"),
            Path.Combine(EditorApplication.applicationContentsPath, "MonoBleedingEdge/lib/mono/4.7.1-api/System.IO.Compression.dll"),
            Path.Combine(EditorApplication.applicationContentsPath, "MonoBleedingEdge/lib/mono/4.7.1-api/System.IO.Compression.FileSystem.dll"),
            Path.Combine(EditorApplication.applicationContentsPath, "MonoBleedingEdge/lib/mono/4.7.1-api/Facades/System.Text.Encoding.dll"),
            Path.Combine(EditorApplication.applicationContentsPath, "MonoBleedingEdge/lib/mono/4.7.1-api/Facades/netstandard.dll"),//System.IO.Compression
        };

        string unityDllPath = Path.Combine(EditorApplication.applicationContentsPath, "Managed/UnityEngine/");
        foreach (var p in Directory.GetFiles( unityDllPath ))
        {
            if (Path.GetExtension(p).Equals(".dll"))
            {
                // Debug.Log("Find reference file: " + p);
                references.Add(p);
            }
        }

        foreach (var p in Directory.GetFiles(RootPath, "*", SearchOption.AllDirectories))
        {
            if (Path.GetExtension(p).Equals(".dll") && !Path.GetFileName(p).Equals(compiledDllFileName))
            {
                string dllPath = rootDirectory + p.Substring(RootPath.Length);

                PluginImporter pluginImporter = AssetImporter.GetAtPath(dllPath) as PluginImporter;
                if(!pluginImporter.isNativePlugin)
                {
                    //Debug.Log("Find reference file: " + p);
                    references.Add(p);
                }
            }
        }

        string[] defines = new string[] {
            "UNITY_EDITOR"
        };

        List<string> sources = new List<string>();
        foreach (var p in Directory.GetFiles(RootPath, "*", SearchOption.AllDirectories))
        {
            if (Path.GetExtension(p).Equals(".cs"))
            {
                //Debug.Log("Find source file: " + p);
                sources.Add(p);
            }
        }
        foreach (var p in Directory.GetFiles(utilityDirectory, "*", SearchOption.AllDirectories))
        {
            if (Path.GetExtension(p).Equals(".cs"))
            {
                Debug.Log("Find source file: " + p);
                sources.Add(p);
            }
        }

        Debug.Log("Began compile InsightEditor ....");

        if(!Directory.Exists(CompiledDllPath))
        {
            Directory.CreateDirectory(CompiledDllPath);
        }

        //Debug.LogError("CompiledDllPath+ compiledDllFileName:"+ (CompiledDllPath + compiledDllFileName));
        string[] msgs = EditorUtility.CompileCSharp(sources.ToArray(), references.ToArray(), defines, CompiledDllPath+ compiledDllFileName);
        foreach (var msg in msgs)
        {
            if (msg.Contains("error"))
            {
                Debug.LogError(msg);
            }
        }

        foreach (var reference in references)
        {
            File.Copy(reference, CompiledDllPath + Path.GetFileName(reference),true);
        }

        string commandPath = Application.dataPath + "/../" + dotfuscatorPath;
        string inputDllPath = CompiledDllPath + compiledDllFileName;
        string outputDllPath = outputPath + "/" + rootDirectory + "/" + compiledDllFileName;
        string outputDirectory = outputPath + "/" + rootDirectory;

        if(File.Exists(outputDllPath))
        {
            File.Delete(outputDllPath);
        }
        //Debug.LogError("commandPath:" + commandPath);
        //Debug.LogError("inputDllPath:"+ inputDllPath);
        Debug.LogError("outputDirectory:" + outputDirectory);

        ExecuteCmd(RectifyPath(commandPath), string.Format("/in:{0} /out:{1}", RectifyPath(inputDllPath), RectifyPath(outputDirectory)));

        Debug.Log("Compile Finished");

        AssetDatabase.Refresh();
    }

    string RectifyPath(string path)
    {
        if ('\\' == Path.DirectorySeparatorChar)
            path = path.Replace("/", "\\");
        else
            path = path.Replace("\\", "/");

        return path;
    }


    void ExecuteCmd(string command, string argument)
    {
        System.Diagnostics.Process proc = new System.Diagnostics.Process();
        proc.StartInfo.FileName = command;
        proc.StartInfo.Arguments = argument;
        proc.Start();
        StringBuilder ret = new StringBuilder();
        while (!proc.HasExited)
        {

        }
        proc.WaitForExit();
    }
}
#endif