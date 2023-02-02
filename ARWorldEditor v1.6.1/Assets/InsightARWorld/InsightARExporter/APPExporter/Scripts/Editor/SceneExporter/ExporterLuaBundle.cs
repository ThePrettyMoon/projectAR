using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using System.Diagnostics;

public class ExporterLuaBundle : MonoBehaviour
{
    static List<AssetBundleBuild> maps = new List<AssetBundleBuild>();
    static List<string> paths = new List<string>();
    static List<string> files = new List<string>();

    public static void ExportLuaBundles(string directory, BuildTarget target, bool bundle)
    {
        string desDirectory = directory + "/scene/ui";
        if (Directory.Exists(desDirectory))
        {
            Directory.Delete(desDirectory, true); 
        }
        Directory.CreateDirectory(desDirectory);
        AssetDatabase.Refresh();

        HandleLuaBundle(directory, bundle);
   
        BuildAssetBundleOptions options = BuildAssetBundleOptions.DeterministicAssetBundle |
                                          BuildAssetBundleOptions.UncompressedAssetBundle;
        BuildPipeline.BuildAssetBundles(desDirectory, maps.ToArray(), options, target);

        string dataDir = Application.dataPath + "/Lua/";
        if (Directory.Exists(dataDir))
            Directory.Delete(dataDir, true);
        
        AssetDatabase.Refresh();
        maps.Clear();
        paths.Clear();
        files.Clear();
    }

    /// <summary>
    /// 处理Lua代码包
    /// </summary>
    public  static void HandleLuaBundle(string luaPath, bool bundle)
    {
        luaPath = Application.dataPath;
        string luaDirectory = luaPath + "/Lua/";
        string[] srcDirs = { luaPath + "/LuaFramework/Lua/",  luaPath + "/LuaFramework/" + "/ToLua/Lua" };
//        string[] srcDirs = {luaPath + "/" + "APPExporter/LuaFramework/Lua/", luaPath + "/" + "APPExporter/LuaFramework/ToLua/Lua" };
        for (int i = 0; i < srcDirs.Length; i++)
        {
            CopyLuaBytesFiles(srcDirs[i], luaDirectory);
        }
        string[] dirs = Directory.GetDirectories(luaDirectory, "*", SearchOption.AllDirectories);
        for (int i = 0; i < dirs.Length; i++)
        {
            string name = dirs[i].Replace(luaDirectory, string.Empty);
            name = name.Replace('\\', '_').Replace('/', '_');
            name = "lua/lua_" + name.ToLower() + ".unity3d";

            string path = "Assets/InsightARWorld" + dirs[i].Replace(luaPath, "");
            AddBuildMap(name, "*.bytes", path);

        }
        AddBuildMap("lua/lua.unity3d", "*.bytes", "Assets/InsightARWorld/Lua/");

        //-------------------------------处理非Lua文件----------------------------------
        luaPath = luaPath + "/lua/";
        for (int i = 0; i < srcDirs.Length; i++)
        { 
            paths.Clear();
            files.Clear();
            string luaDataPath = srcDirs[i].ToLower();
            Recursive(luaDataPath);
            foreach (string f in files)
            {
                if (f.EndsWith(".meta") || f.EndsWith(".lua"))
                    continue;
                string newfile = f.Replace(luaDataPath, "");
                string path = Path.GetDirectoryName(luaPath + newfile);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                     
                string destfile = path + "/" + Path.GetFileName(f);
                File.Copy(f, destfile, true);
            }
        }
        AssetDatabase.Refresh();
    }

    public static void CopyLuaBytesFiles(string sourceDir, string destDir, bool appendext = true, string searchPattern = "*.lua", SearchOption option = SearchOption.AllDirectories)
    {
        if (!Directory.Exists(sourceDir))
        {
            return;
        }

        string[] files = Directory.GetFiles(sourceDir, searchPattern, option);
        int len = sourceDir.Length;

        if (sourceDir[len - 1] == '/' || sourceDir[len - 1] == '\\')
        {
            --len;
        }         

        for (int i = 0; i < files.Length; i++)
        {
            string str = files[i].Remove(0, len);
            string dest = destDir + "/" + str;
            if (appendext)
                dest += ".bytes";
            string dir = Path.GetDirectoryName(dest);
            Directory.CreateDirectory(dir);
            File.Copy(files[i], dest, true);
        }
    }

    static void AddBuildMap(string bundleName, string pattern, string path)
    {
        string[] patterns = pattern.Split(new char[] { ' ' });
        List<string> lstFiles = new List<string>();
        for (int i = 0; i < patterns.Length; i++)
        {
            string tmp = patterns[i];
            lstFiles.AddRange(Directory.GetFiles(path, tmp));
        }

        string[] files = lstFiles.ToArray();
        if (files.Length == 0)
            return;

        for (int i = 0; i < files.Length; i++)
        {
            files[i] = files[i].Replace('\\', '/');
            //UnityEngine.Debug.Log("Unity Call Files [i] " + i + " " + files[i] + "  " + bundleName);
        }
        AssetBundleBuild build = new AssetBundleBuild();
        build.assetBundleName = bundleName;
        build.assetNames = files;

        maps.Add(build); 
    }


    /// <summary>
    /// 遍历目录及其子目录
    /// </summary>
    public  static void Recursive(string path)
    {
        string[] names = Directory.GetFiles(path);
        string[] dirs = Directory.GetDirectories(path);
        foreach (string filename in names)
        {
            string ext = Path.GetExtension(filename);
            if (ext.Equals(".meta"))
                continue;
            files.Add(filename.Replace('\\', '/'));
        }
        foreach (string dir in dirs)
        {
            paths.Add(dir.Replace('\\', '/'));
            Recursive(dir);
        }
    }
}
