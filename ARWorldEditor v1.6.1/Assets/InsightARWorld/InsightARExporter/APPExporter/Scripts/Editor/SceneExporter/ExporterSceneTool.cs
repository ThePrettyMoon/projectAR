#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using Newtonsoft.Json;
using UnityEditor;
using System.Text;
using ARWorldEditor;
using UnityEditor.Build.Reporting;

/// <summary>
/// 导出场景信息 
/// </summary>
public class ExporterSceneTool 
{
    #region package
    [MenuItem("EZXR/账号登录",false, 19)]
    static void Login()
    {
        LoginSceneWindow.ShowWindow();
    }

    [MenuItem("EZXR/账号登录", true, 19)]
    static bool ValidLogin()
    {
        return !UserController.UserLogin;
    }

    [MenuItem("EZXR/发布内容", false, 50)]
    private static void ExportAPPScene()
    {
        ARSession.ExportSceneWindow.ShowWindow();
    }

    [MenuItem("EZXR/发布内容 &4", true, 50)]
    static bool ValidateExportAPPScene()
    {
        return UserController.UserLogin;
    }

    public static bool ExportSceneiOS(out string bundlePath,bool bRevealInfinder = false)
    {
      return  ExportSceneByBuildingTarget(BuildTarget.iOS,out bundlePath,bRevealInfinder);
    }

    [MenuItem("EZXR/导出为洞见Unity资源/Bundle - iOS", false, 90)]
    static void ExportAPPSceneiOS()
    {
        string bundlePath = "";
        ExportSceneByBuildingTarget(BuildTarget.iOS, out bundlePath, true);
    }

    [MenuItem("EZXR/导出为洞见Unity资源/Bundle - iOS &2", true, 90)]
    static bool ValidateExportSceneiOS()
    {
        if (UserController.UserLogin)
        {
           return ARSession.ExportSceneWindow.LoadMyContent().SelectDevicesType == (int)ContentsDevicesType.Mobile;
        }

        return UserController.UserLogin;
    }

    [MenuItem("EZXR/导出为洞见Unity资源/Bundle - Android", false, 89)]
    static void ExportAPPSceneAndroid()
    {
        string bundlePath = "";
        ExportSceneByBuildingTarget(BuildTarget.Android, out bundlePath, true);
    }

    [MenuItem("EZXR/导出为洞见Unity资源/Bundle - SimGlass", false, 90)]
    static void ExportAPPSceneSimGlass()
    {
        string bundlePath = "";
        ExportSimGlassSceneByBuildingTarget(BuildTarget.Android, out bundlePath, true);
    }

    [MenuItem("EZXR/导出为洞见Unity资源/Bundle - SimGlass", true, 90)]
    static bool ValidateExportSceneSimGlass()
    {
        if (UserController.UserLogin)
        {
            return ARSession.ExportSceneWindow.LoadMyContent().SelectDevicesType == (int)ContentsDevicesType.SimGlass;
        }
        return UserController.UserLogin;
    }

    [MenuItem("EZXR/导出为洞见Unity资源", true, 90)]
    static bool ValidateExportSceneAPP()
    {
        return false;
    }


    [MenuItem("EZXR/导出为洞见Unity资源/Bundle - Android &1", true, 89)]
    static bool ValidateExportSceneAndroid()
    {
        if (UserController.UserLogin)
        {
            return ARSession.ExportSceneWindow.LoadMyContent().SelectDevicesType == (int)ContentsDevicesType.Mobile;
        }
        return UserController.UserLogin;
    }

    /*[MenuItem("EZXR/导出为洞见Unity资源1", false, 105)]
    static void ExportSceneAPP()
    {
        Menu.SetChecked("EZXR/导出为洞见Unity资源1", false);
    }

    [MenuItem("EZXR/导出为洞见Unity资源", true, 105)]
    static bool ValidateExportSceneAPP1()
    {
        return ARSession.UserController.UserLogin;
    }*/

    public static bool ExportSceneAndroid(out string bundlePath, bool bRevealInfinder = false)
    {
        return ExportSceneByBuildingTarget(BuildTarget.Android, out bundlePath, bRevealInfinder);
    }

    public static bool ExportSceneSimGlass(out string bundlePath, bool bRevealInfinder = false)
    {
        return ExportSimGlassSceneByBuildingTarget(BuildTarget.Android, out bundlePath, bRevealInfinder);
    }

    [MenuItem("EZXR/登出", false, 1000)]
    static void Logout()
    {
        ARSession.LogoutScene.Logout();
    }


    [MenuItem("EZXR/登出", true, 1000)]
    static bool ValidateLogout()
    {
        return UserController.UserLogin;
    }


    //[MenuItem("EZXR/Copy Lua To Framework")]
    static void GenerateLuaPath()
    {
        CopyLuaToFramework();
    }

    /// <summary>
    /// 选中多物体分别打包 
    /// </summary>
    //[MenuItem("EZXR/Export Selected Objects - iOS")]
    static void ExportedSelectedObjectsiOS()
    {
        ExportSelectedObjectsByBuildingTarget(BuildTarget.iOS); 
    }

    //[MenuItem("EZXR/Export Selected Objects - Android")]
    static void ExportedSelectedObjectsAndroid()
    {
        ExportSelectedObjectsByBuildingTarget(BuildTarget.Android); 
    }

    #endregion

    #region utility

    /// <summary>
    /// Exports the scene by target.
    /// </summary>
    /// <param name="target">Target.</param>
    private static bool ExportSceneByBuildingTarget(BuildTarget target, out string bundlePath, bool bRevealInfinder, string directory = "")
    {
        Debug.Log("场景正在打包..." + target);
       
        bundlePath = "";

        Scene activeScene = SceneManager.GetActiveScene();

        //添加没有保存场景提示
        if (string.IsNullOrEmpty(activeScene.path))
        {
            EditorUtility.DisplayDialog("提示", "打包前请先保存场景", "OK");
            ExportProgressEditorWindow.SetCurrentProgressState(ExportProgressEditorWindow.Error_NeedSaveScene, 0f);
            return false;
        }

        GameObject[] realityScenes = GameObject.FindGameObjectsWithTag("RealityScene");
        if (realityScenes != null && realityScenes.Length > 0)
        {
            foreach (var realityScene in realityScenes)
            {
                MeshRenderer meshRender = realityScene.GetComponent<MeshRenderer>();
                if (meshRender != null)
                {
                    Material realitySceneMaterial = realityScene.GetComponent<MeshRenderer>().sharedMaterial;
                    if (realitySceneMaterial != null)
                    {
                        realitySceneMaterial.SetFloat("_Alpha", 0);
                    }
                }
            }
        }

        bool hasPLY = false;
        GameObject[] rootObjects = activeScene.GetRootGameObjects();
        string meshPrefabPath = Path.Combine(Application.dataPath, ConfigGlobal.HIGH_POINT_TEMP_FOLDER, ConfigGlobal.HIGH_POINT_MODEL_NAME + ".prefab");
        
        for (int i = 0; i < rootObjects.Length; i++)
        {
            if (rootObjects[i].name == "World")
            {
                rootObjects[i].SetActive(false);

                //确认是否有PLY存在
                var worldTrans = rootObjects[i].transform;
                for (int index = 0; index < worldTrans.childCount; index++)
                {
                    if (worldTrans.GetChild(index).name == ConfigGlobal.HIGH_POINT_MODEL_NAME)
                    {
                        hasPLY = true;
                        Directory.CreateDirectory(Path.Combine(Application.dataPath, ConfigGlobal.HIGH_POINT_TEMP_FOLDER));
                        var landPLY = worldTrans.GetChild(index);
                        PrefabUtility.SaveAsPrefabAsset(landPLY.gameObject, meshPrefabPath);
                        GameObject.DestroyImmediate(landPLY.gameObject);
                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();
                        break;
                    }
                }
            }


            if (rootObjects[i].name == "toolman")
            {
                rootObjects[i].SetActive(false);
            }
        }


        if (string.IsNullOrEmpty(directory))
        {
            directory = Application.dataPath + "/../U3DExporter/" + System.DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss_") + target;
        }

        //string sceneDirectory = directory + "/scene";
        string sceneDirectory = directory;
        if (Directory.Exists(sceneDirectory))
        {
            Directory.Delete(sceneDirectory, true);
        }
        Directory.CreateDirectory(sceneDirectory);

        // export scene config
        ExporterSceneJson.ExportSceneJson(activeScene, sceneDirectory);

        //主相机需要修改为solidcolor
        bool isCameraSkybox = false;
        if (Camera.main != null)
        {
            isCameraSkybox = Camera.main.clearFlags == CameraClearFlags.Skybox;
            if (isCameraSkybox)
            {
                Camera.main.clearFlags = CameraClearFlags.SolidColor;
            }
        }

        List<string> lst = new List<string>();
        lst.Add(activeScene.path);
        string sceneName = activeScene.name + ".unity3d";
        string scenePath = sceneDirectory + "/" + sceneName;
        string[] levels = lst.ToArray();

        BuildReport buildReport =  BuildPipeline.BuildPlayer(levels, scenePath, target, BuildOptions.BuildAdditionalStreamedScenes);
        ExportProgressEditorWindow.SetCurrentProgressState(buildReport,0.2f);
        //打包失败
        if(buildReport.summary.result != BuildResult.Succeeded)
        {
            return false;
        }
        AssetDatabase.Refresh();
        string zipFilePath = string.Format("{0}.zip", directory);
        ARWorldEditor.ZipUtility.Zip(directory, zipFilePath);
        bundlePath = zipFilePath;
        Debug.Log("场景打包完毕");

        //还原VideoPlayer
        string filePath = sceneDirectory + "/" + "scenedesc.json";
         ExporterSceneJson.RecoverGameObjectVideoPlayers(filePath);
        AssetDatabase.Refresh();


        //还原相机
        if (Camera.main != null)
        {
            if (isCameraSkybox)
            {
                Camera.main.clearFlags = CameraClearFlags.Skybox;
            }
        }

        rootObjects = activeScene.GetRootGameObjects();
        for (int i = 0; i < rootObjects.Length; i++)
        {
            if (rootObjects[i].name == "World")
            {
                rootObjects[i].SetActive(true);
                var worldTrans = rootObjects[i].transform;
                if (hasPLY)
                {
                    //还原ply
                    var ply = AssetDatabase.LoadAssetAtPath<GameObject>("Assets" + ConfigGlobal.HIGH_POINT_TEMP_FOLDER + "/" + ConfigGlobal.HIGH_POINT_MODEL_NAME + ".prefab");
                    var clone = GameObject.Instantiate(ply, worldTrans);
                    clone.name = ConfigGlobal.HIGH_POINT_MODEL_NAME;
                    Directory.Delete(Application.dataPath + "/" + ConfigGlobal.HIGH_POINT_TEMP_FOLDER, true);
                }
            }

            if (rootObjects[i].name == "toolman")
            {
                rootObjects[i].SetActive(true);
            }
        }

        realityScenes = GameObject.FindGameObjectsWithTag("RealityScene");
        if (realityScenes != null && realityScenes.Length > 0)
        {
            foreach (var realityScene in realityScenes)
            {
                MeshRenderer meshRender = realityScene.GetComponent<MeshRenderer>();
                if (meshRender != null)
                {
                    Material realitySceneMaterial = realityScene.GetComponent<MeshRenderer>().sharedMaterial;
                    if (realitySceneMaterial != null)
                    {
                        realitySceneMaterial.SetFloat("_Alpha", 1);
                    }
                }
            }
        }

        //添加打包完自动跳转所在目录功能
        if (bRevealInfinder)
        {
            EditorUtility.RevealInFinder(zipFilePath);
            ExportProgressEditorWindow.SetCurrentProgressState(ExportProgressEditorWindow.ContentTurnToFolder, 1f);
        }
        return true;
    }

    private static bool ExportSimGlassSceneByBuildingTarget(BuildTarget target, out string bundlePath, bool bRevealInfinder, string directory = "")
    {
        Debug.Log("场景正在打包..." + target+ "-Glass-SIM");

        bundlePath = "";

        Scene activeScene = SceneManager.GetActiveScene();

        //添加没有保存场景提示
        if (string.IsNullOrEmpty(activeScene.path))
        {
            EditorUtility.DisplayDialog("提示", "打包前请先保存场景", "OK");
            ExportProgressEditorWindow.SetCurrentProgressState(ExportProgressEditorWindow.Error_NeedSaveScene, 0f);
            return false;
        }

        GameObject[] realityScenes = GameObject.FindGameObjectsWithTag("RealityScene");
        if (realityScenes != null && realityScenes.Length > 0)
        {
            foreach (var realityScene in realityScenes)
            {
                MeshRenderer meshRender = realityScene.GetComponent<MeshRenderer>();
                if (meshRender != null)
                {
                    Material realitySceneMaterial = realityScene.GetComponent<MeshRenderer>().sharedMaterial;
                    if (realitySceneMaterial != null)
                    {
                        realitySceneMaterial.SetFloat("_Alpha", 0);
                    }
                }
            }
        }

        bool hasPLY = false;
        GameObject[] rootObjects = activeScene.GetRootGameObjects();
        string meshPrefabPath = Path.Combine(Application.dataPath, ConfigGlobal.HIGH_POINT_TEMP_FOLDER, ConfigGlobal.HIGH_POINT_MODEL_NAME + ".prefab");

        for (int i = 0; i < rootObjects.Length; i++)
        {
            if (rootObjects[i].name == "World")
            {
                rootObjects[i].SetActive(false);

                //确认是否有PLY存在
                var worldTrans = rootObjects[i].transform;
                for (int index = 0; index < worldTrans.childCount; index++)
                {
                    if (worldTrans.GetChild(index).name == ConfigGlobal.HIGH_POINT_MODEL_NAME)
                    {
                        hasPLY = true;
                        Directory.CreateDirectory(Path.Combine(Application.dataPath, ConfigGlobal.HIGH_POINT_TEMP_FOLDER));
                        var landPLY = worldTrans.GetChild(index);
                        PrefabUtility.SaveAsPrefabAsset(landPLY.gameObject, meshPrefabPath);
                        GameObject.DestroyImmediate(landPLY.gameObject);
                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();
                        break;
                    }
                }
            }


            if (rootObjects[i].name == "toolman")
            {
                rootObjects[i].SetActive(false);
            }
        }


        if (string.IsNullOrEmpty(directory))
        {
            directory = Application.dataPath + "/../U3DExporter/" + System.DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss_") + "Glass-SIM";
        }

        //string sceneDirectory = directory + "/scene";
        string sceneDirectory = directory;
        if (Directory.Exists(sceneDirectory))
        {
            Directory.Delete(sceneDirectory, true);
        }
        Directory.CreateDirectory(sceneDirectory);

        // export scene config
        ExporterSceneJson.ExportSceneJson(activeScene, sceneDirectory);

        //主相机需要修改为solidcolor
        bool isCameraSkybox = false;
        if (Camera.main != null)
        {
            isCameraSkybox = Camera.main.clearFlags == CameraClearFlags.Skybox;
            if (isCameraSkybox)
            {
                Camera.main.clearFlags = CameraClearFlags.SolidColor;
            }
        }

        List<string> lst = new List<string>();
        lst.Add(activeScene.path);
        string sceneName = activeScene.name + ".unity3d";
        string scenePath = sceneDirectory + "/" + sceneName;
        string[] levels = lst.ToArray();

        BuildReport buildReport = BuildPipeline.BuildPlayer(levels, scenePath, target, BuildOptions.BuildAdditionalStreamedScenes);
        ExportProgressEditorWindow.SetCurrentProgressState(buildReport, 0.2f);
        //打包失败
        if (buildReport.summary.result != BuildResult.Succeeded)
        {
            return false;
        }
        AssetDatabase.Refresh();
        string zipFilePath = string.Format("{0}.zip", directory);
        ARWorldEditor.ZipUtility.Zip(directory, zipFilePath);
        bundlePath = zipFilePath;
        Debug.Log("场景打包完毕");

        //还原VideoPlayer
        string filePath = sceneDirectory + "/" + "scenedesc.json";
        ExporterSceneJson.RecoverGameObjectVideoPlayers(filePath);
        AssetDatabase.Refresh();


        //还原相机
        if (Camera.main != null)
        {
            if (isCameraSkybox)
            {
                Camera.main.clearFlags = CameraClearFlags.Skybox;
            }
        }

        rootObjects = activeScene.GetRootGameObjects();
        for (int i = 0; i < rootObjects.Length; i++)
        {
            if (rootObjects[i].name == "World")
            {
                rootObjects[i].SetActive(true);
                var worldTrans = rootObjects[i].transform;
                if (hasPLY)
                {
                    //还原ply
                    var ply = AssetDatabase.LoadAssetAtPath<GameObject>("Assets" + ConfigGlobal.HIGH_POINT_TEMP_FOLDER + "/" + ConfigGlobal.HIGH_POINT_MODEL_NAME + ".prefab");
                    var clone = GameObject.Instantiate(ply, worldTrans);
                    clone.name = ConfigGlobal.HIGH_POINT_MODEL_NAME;
                    Directory.Delete(Application.dataPath + "/" + ConfigGlobal.HIGH_POINT_TEMP_FOLDER, true);
                }
            }

            if (rootObjects[i].name == "toolman")
            {
                rootObjects[i].SetActive(true);
            }
        }

        realityScenes = GameObject.FindGameObjectsWithTag("RealityScene");
        if (realityScenes != null && realityScenes.Length > 0)
        {
            foreach (var realityScene in realityScenes)
            {
                MeshRenderer meshRender = realityScene.GetComponent<MeshRenderer>();
                if (meshRender != null)
                {
                    Material realitySceneMaterial = realityScene.GetComponent<MeshRenderer>().sharedMaterial;
                    if (realitySceneMaterial != null)
                    {
                        realitySceneMaterial.SetFloat("_Alpha", 1);
                    }
                }
            }
        }

        //添加打包完自动跳转所在目录功能
        if (bRevealInfinder)
        {
            EditorUtility.RevealInFinder(zipFilePath);
            ExportProgressEditorWindow.SetCurrentProgressState(ExportProgressEditorWindow.ContentTurnToFolder, 1f);
        }
        return true;
    }

    public static void Copy(string sourceDirectory, string targetDirectory)
    {
        DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
        DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);

        CopyAll(diSource, diTarget);
    }

    /// <summary>
    /// copy directory
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
    {
        Directory.CreateDirectory(target.FullName);

        // Copy each file into the new directory.
        foreach (FileInfo fi in source.GetFiles())
        {
            fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
        }

        // Copy each subdirectory using recursion.
        foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
        {
            DirectoryInfo nextTargetSubDir =
                target.CreateSubdirectory(diSourceSubDir.Name);
            CopyAll(diSourceSubDir, nextTargetSubDir);
        }
    }

    /// <summary>
    /// export object
    /// </summary>
    /// <param name="target">Target.</param>
    public static void ExportSelectedObjectsByBuildingTarget(BuildTarget target)
    {
        string exportDirectory = Application.streamingAssetsPath + "/models";
        if (Directory.Exists(exportDirectory))
        {
            Directory.Delete(exportDirectory, true); 
        }
        Directory.CreateDirectory(exportDirectory);
        AssetDatabase.Refresh();

        UnityEngine.Object[] selectedAsset = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.DeepAssets);
     
        BuildAssetBundleOptions options = BuildAssetBundleOptions.DeterministicAssetBundle |
                                          BuildAssetBundleOptions.UncompressedAssetBundle;
        
        List<AssetBundleBuild> maps = new List<AssetBundleBuild>();

        foreach (UnityEngine.Object obj in selectedAsset)
        {
            AssetBundleBuild build = new AssetBundleBuild();
            build.assetBundleName = obj.name + ".unity3d"; 
            build.assetNames = new string[]{ AssetDatabase.GetAssetPath(obj).Replace("\\", "/") }; 
            maps.Add(build);
        }
            
        BuildPipeline.BuildAssetBundles(exportDirectory, maps.ToArray(), options, target);

        AssetDatabase.Refresh();  
    }

    /// <summary>
    /// Exports the selected objects by building target.
    /// </summary>
    /// <param name="target">Target.</param>
    public static void ExportSelectedDirectoriesByBuildingTarget(BuildTarget target)
    {
        string exportDirectory = Application.streamingAssetsPath + "/directories";
        if (Directory.Exists(exportDirectory))
        {
            Directory.Delete(exportDirectory, true); 
        }
        Directory.CreateDirectory(exportDirectory);
        AssetDatabase.Refresh();

        string modelPath = "Assets/InsightARWorld/Prefabs/Model";
        string[] dirs = Directory.GetDirectories(modelPath, "*", SearchOption.AllDirectories);
        List<AssetBundleBuild> maps = new List<AssetBundleBuild>();

        for (int i = 0; i < dirs.Length; i++)
        {
            string name = dirs[i].Replace(modelPath, string.Empty);
            name = name.Replace('\\', '_').Replace('/', '_');
            name = "model" + name.ToLower() + ".unity3d";

            string path = "Assets/InsightARWorld" + dirs[i].Replace(Application.dataPath, "");
            List<string> lstFiles = new List<string>();
            lstFiles.AddRange(Directory.GetFiles(dirs[i]));
            string[] files = lstFiles.ToArray();
            if (files.Length == 0)
                continue;

            for (int j = 0; j < files.Length; j++)
            {
                files[j] = files[j].Replace('\\', '/');
            }
            AssetBundleBuild build = new AssetBundleBuild();
            build.assetBundleName = name;
            build.assetNames = files; 
            maps.Add(build); 
        }

        BuildAssetBundleOptions options = BuildAssetBundleOptions.DeterministicAssetBundle |
                                          BuildAssetBundleOptions.UncompressedAssetBundle;

        BuildPipeline.BuildAssetBundles(exportDirectory, maps.ToArray(), options, target);

        AssetDatabase.Refresh();  
    }

    private static List<string> files = new List<string>();

    /// <summary>
    /// 拷贝lua to streaming assets framework
    /// </summary>
    private static void CopyLuaToFramework()
    {
        string srcRoot = Application.dataPath + "/LuaFramework";
        string[] srcDirectories = new string[] { srcRoot + "/Lua",
        srcRoot +"/ToLua/Lua"};
        string destDir = Application.streamingAssetsPath + "/luaframework";
        string fileName = "files.txt";
        string filePath =Path.Combine( destDir,fileName);
        if (!Directory.Exists(destDir)) Directory.CreateDirectory(destDir);
        if (File.Exists(filePath)) File.Delete(filePath);
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < srcDirectories.Length; i++)
        {
            files.Clear();
            string srcDir = srcDirectories[i];
            Recursive(srcDir);

            for (int j = 0; j < files.Count; j++)
            {
                string destFilePath = files[j].Replace(srcRoot, destDir);
                string destFileDir = Path.GetDirectoryName(destFilePath);
                if (!Directory.Exists(destFileDir)) Directory.CreateDirectory(destFileDir);

                File.Copy(files[j], destFilePath,true);
                sb.AppendLine(destFilePath.Replace(destDir,""));
            }
        }
        File.WriteAllText(filePath, sb.ToString());
        Debug.Log("copy luaframe work done !");
    }

    /// <summary>
    /// 递归遍历
    /// </summary>
    /// <param name="srcDir"></param>
    private static void Recursive(string srcDir)
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(srcDir);
        if (null != directoryInfo && directoryInfo.Exists)
        {
            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                if (file.Name.EndsWith(".meta") || file.Name.EndsWith(".DS_Store")) continue;
                files.Add(file.FullName.Replace("\\", "/")) ;
            }
            foreach (DirectoryInfo dir in directoryInfo.GetDirectories())
            {
               Recursive(dir.FullName);
            }
        }
    }

#endregion

  
}
#endif
