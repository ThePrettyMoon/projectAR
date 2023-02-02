#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text.RegularExpressions;
using ARWorldEditor;

namespace RenderEngine
{
    class ExporterMenu
    {
        public static bool AlgExportSuccess = true;

        [MenuItem("EZXR/我的内容", false, 30)]
        static void CreateARScene()
        {
            ARSceneCreator.ShowWindow();
        }

        [MenuItem("EZXR/我的内容 &3", true,30)]
        static bool ValidateCreateARScene()
        {
            return UserController.UserLogin;
        }

        [MenuItem("EZXR/绘制模拟路径", false, 70)]
        static void CreateCameraPath()
        {
            CameraPathCreateFactory.Create();
        }
        [MenuItem("EZXR/绘制模拟路径", true, 70)]
        static bool ValidateCreateCameraPath()
        {
            return UserController.UserLogin;
        }

        [MenuItem("EZXR/导出为洞见Native资源/快速导出（仅调试用）/Scene - iOS %e", false, 100)]
         static bool ExportSceneiOSSketch()
         {
             if (!CheckEnvironment()) return false;
             return DoExport(ExporterConfig.REPlatform.iOS
                 , true // sketch?
                 , false); // bundle?
         }

         [MenuItem("EZXR/导出为洞见Native资源/快速导出（仅调试用）/Scene - iOS %e", true, 100)]
         static bool ValidateExportSceneiOSSketch()
         {
             return UserController.UserLogin;
         }


         public static bool ExportSceneiOSSketch(string path, Texture2D shareLogo = null)
         {
             if (!CheckEnvironment()) return false;
             return DoExport(ExporterConfig.REPlatform.iOS
                 , true // sketch?
                 , false // bundle?
                 , path
                 , shareLogo);
         }

         [MenuItem("EZXR/导出为洞见Native资源/快速导出（仅调试用）/Scene - Android %t", false, 100)]
         static bool ExportSceneAndroidSketch()
         {
             if (!CheckEnvironment()) return false;
             return DoExport(ExporterConfig.REPlatform.Android
                 , true // sketch?
                 , false); // bundle?
         }

         [MenuItem("EZXR/导出为洞见Native资源/快速导出（仅调试用）/Scene - Android %t", true, 100)]
         static bool ValidateExportSceneAndroidSketch()
         {
             return UserController.UserLogin;
         }


         public static bool ExportSceneAndroidSketch(string path, Texture2D shareLogo = null)
         {
             if (!CheckEnvironment()) return false;
             return DoExport(ExporterConfig.REPlatform.Android
                 , true // sketch?
                 , false // bundle?
                 , path
                 , shareLogo);
         }


         [MenuItem("EZXR/导出为洞见Native资源/快速导出（仅调试用）/Bundle - iOS", false, 100)]
         static bool ExportInsightBundleiOSSketch()
         {
             if (!CheckEnvironment()) return false;
             return DoExport(ExporterConfig.REPlatform.iOS
                 , true // sketch?
                 , true); // bundle?
         }

         [MenuItem("EZXR/导出为洞见Native资源/快速导出（仅调试用）/Bundle - iOS", true, 100)]
         static bool ValidateExportInsightBundleiOSSketch()
         {
             return UserController.UserLogin;
         }

         [MenuItem("EZXR/导出为洞见Native资源/快速导出（仅调试用）/Bundle - Android", false, 100)]
         static bool ExportInsightBundleAndroidSketch()
         {
             if (!CheckEnvironment()) return false;
             return DoExport(ExporterConfig.REPlatform.Android
                 , true // sketch?
                 , true); // bundle?
         }

         [MenuItem("EZXR/导出为洞见Native资源/快速导出（仅调试用）/Bundle - Android", true, 100)]
         static bool ValidateExportInsightBundleAndroidSketch()
         {
             return UserController.UserLogin;
         }

         [MenuItem("EZXR/导出为洞见Native资源/标准导出/Scene - iOS", false, 100)]
         static bool ExportSceneiOSProduct()
         {
             if (!CheckEnvironment()) return false;
             return DoExport(ExporterConfig.REPlatform.iOS
                 , false // sketch?
                 , false); // bundle?
         }

         [MenuItem("EZXR/导出为洞见Native资源/标准导出/Scene - iOS", true, 100)]
         static bool ValidateExportSceneiOSProduct()
         {
             return UserController.UserLogin;
         }

         // FOR AT
         public static bool ExportSceneiOSProduct(string path, Texture2D shareLogo = null)
         {
             if (!CheckEnvironment()) return false;
             return DoExport(ExporterConfig.REPlatform.iOS
                 , false // sketch?
                 , false // bundle?
                 , path
                 , shareLogo);
         }

         [MenuItem("EZXR/导出为洞见Native资源/标准导出/Scene - Android", false, 100)]
         static bool ExportSceneAndroidProduct()
         {
             if (!CheckEnvironment()) return false;
             return DoExport(ExporterConfig.REPlatform.Android
                 , false // sketch?
                 , false); // bundle?
         }

         [MenuItem("EZXR/导出为洞见Native资源/标准导出/Scene - Android", true, 100)]
         static bool ValidateExportSceneAndroidProduct()
         {
             return UserController.UserLogin;
         }

         // FOR AT
         public static bool ExportSceneAndroidProduct(string path, Texture2D shareLogo = null)
         {
             if (!CheckEnvironment()) return false;
             return DoExport(ExporterConfig.REPlatform.Android
                 , false // sketch?
                 , false // bundle?
                 , path
                 , shareLogo);
         }

         [MenuItem("EZXR/导出为洞见Native资源/标准导出/Bundle - iOS", false, 100)]
         public static bool ExportInsightBundleiOSProduct()
         {
             if (!CheckEnvironment()) return false;
             return DoExport(ExporterConfig.REPlatform.iOS
                 , false // sketch?
                 , true); // bundle?
         }

         [MenuItem("EZXR/导出为洞见Native资源/标准导出/Bundle - iOS", true, 100)]
         static bool ValidateExportInsightBundleiOSProduct()
         {
             return UserController.UserLogin;
         }

         [MenuItem("EZXR/导出为洞见Native资源/标准导出/Bundle - Android", false, 100)]
         public static bool ExportInsightBundleAndroidProduct()
         {
             if (!CheckEnvironment()) return false;
             return DoExport(ExporterConfig.REPlatform.Android
                 , false // sketch?
                 , true); // bundle?
         }

         [MenuItem("EZXR/导出为洞见Native资源/标准导出/Bundle - Android", true, 100)]
         static bool ValidateExportInsightBundleAndroidProduct()
         {
             return UserController.UserLogin;
         }

        public static bool ExportInsightBundleiOSProduct(out string bundlePath)
        {
            bundlePath = "";
            if (!CheckEnvironment()) return false;
            return DoExport(ExporterConfig.REPlatform.iOS
                , false // sketch?
                , true
                ,out bundlePath,false); // bundle?
        }

        public static bool ExportInsightBundleAndroidProduct(out string bundlePath)
        {
            bundlePath = "";
            if (!CheckEnvironment()) return false;
            return DoExport(ExporterConfig.REPlatform.Android
                , false // sketch?
                , true
                ,out bundlePath,false); // bundle?
        }

        public static bool DoExport(ExporterConfig.REPlatform platform
           , bool sketch
           , bool bundle
           , string forcedExportPath = null
           , Texture2D forcedShareLogo = null
           ,bool bRevealInFinder = true
           )
        {
            string bundlePath = "";
            return DoExport(platform
       , false // sketch?
       , true
       , out bundlePath, bRevealInFinder); // bundle?
        }

        public static bool DoExport(ExporterConfig.REPlatform platform
            , bool sketch
            , bool bundle
            ,out string bundlePath
            ,bool bRevealInFinder 
            , string forcedExportPath = null
            , Texture2D forcedShareLogo = null
            )
        {
            AlgExportSuccess = true;
            bundlePath = "";

            UserConfig.ExporterData exporterData = null;
            List<SceneAsset> prefabs;
            Texture2D shareLogo;
            List<TextAsset> JSLibrariesCustomAsset;
            UserConfig.ExporterConfigWindow.GetDataFromJson(out exporterData, out prefabs, out shareLogo, out JSLibrariesCustomAsset);

            //添加没有保存场景提示
            UnityEngine.SceneManagement.Scene currentScene = UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene();

            if (string.IsNullOrEmpty(currentScene.path))
            {
                EditorUtility.DisplayDialog("提示", "打包前请先保存场景", "OK");
                return false;
            }

            // 导出前先保存场景
            if (string.Empty != currentScene.name)
                UnityEditor.SceneManagement.EditorSceneManager.SaveScene(currentScene);

            // 确定导出的根目录。如果是bundle需要指定到Temp文件夹下
            // 算法配置保存在根目录的config子文件夹下
            // 资源文件保存在根目录的scene子文件夹下
            // 动态模型保存在根目录的model1，model2...子文件夹下
            // 如果是bundle，则scene和config打成一个zip，model各自打成zip
            string parentPath;
            if (null != forcedExportPath)
                parentPath = forcedExportPath;
            else
                parentPath = bundle ? Path.Combine(ExporterConfig.TEMPORARY_PATH, "exporter/") : (platform == ExporterConfig.REPlatform.iOS ? ExporterConfig.IOSDestinationPath : ExporterConfig.AndroidDestinationPath);

            Directory.CreateDirectory(parentPath);

            // 为导出模式生成相应的config文件
            ExporterConfig config = new ExporterConfig(platform, sketch);

            if (bundle)
                UtilityFileSystem.DirectoryDeleteRF(parentPath);

            UserConfig.ExportSceneData sceneData = new UserConfig.ExportSceneData();
            sceneData.platform = platform;
            sceneData.config = config;
            sceneData.shareLogo = forcedShareLogo != null ? forcedShareLogo : shareLogo;
            sceneData.prefabs = prefabs;
            sceneData.JSLibrariesName = exporterData.JSLibrariesName;
            sceneData.JSLibrariesCustomAsset = JSLibrariesCustomAsset;
            sceneData.JSLibrariesCustomStatus = exporterData.JSLibrariesCustomStatus;
            sceneData.sketch = sketch;
            sceneData.bundle = bundle;
            sceneData.parentPath = parentPath;
            sceneData.bundle_path = Path.Combine(ExporterConfig.BundleDestinationPath,
                    UnityEngine.SceneManagement.SceneManager.GetActiveScene().name + '_' + platform.ToString() + '_' + (sketch ? "SDK_Sketch" : "SDK_Product"));

            // -------首先导出config和scene-----
            string arConfigPath = Path.Combine(parentPath, "config/");
            // config.root_path = Path.Combine(parentPath, "scene/");
            //update by wy 不需要scene 目录
            config.root_path = parentPath;
            // 导出AR config文件，注意先删除原有文件夹   (1.7及以下版本的算法导出)
            // UserConfig.AlgConfigWindow.ExportConfig(arConfigPath, UtilityFileSystem.DirectoryDeleteRF);
            //update by wy
            //  UserConfig.AlgSessionConfigExporter.ExportConfig(arConfigPath, UtilityFileSystem.DirectoryDeleteRF);

            // 修改场景
            GameObject[] realityScenes = GameObject.FindGameObjectsWithTag(ConfigGlobal.TAG_REALITYSCENE);
            if (realityScenes != null && realityScenes.Length > 0)
            {
                foreach (var realityScene in realityScenes)
                {
                    MeshRenderer meshRenderer = realityScene.GetComponent<MeshRenderer>();
                    if (meshRenderer != null)
                    {
                        Material realitySceneMaterial = meshRenderer.sharedMaterial;
                        realitySceneMaterial.SetFloat("_Alpha", 0);
                       // realitySceneMaterial.SetFloat("_ShadowIntensity", 0);
                    }
                }
            }
           
            GameObject[] rootObjects = currentScene.GetRootGameObjects();
            for (int i = 0; i < rootObjects.Length; i++)
            {
                if (rootObjects[i].name == "World") rootObjects[i].SetActive(false);
                if (rootObjects[i].name == "toolman") rootObjects[i].SetActive(false);
            }

            // 导出场景
            bool ret =  AlgExportSuccess && DoExportScene(sceneData,out bundlePath,bRevealInFinder);

            // 还原场景
            rootObjects = currentScene.GetRootGameObjects();
            for (int i = 0; i < rootObjects.Length; i++)
            {
                if(rootObjects[i].name == "World") rootObjects[i].SetActive(true);
                if (rootObjects[i].name == "toolman") rootObjects[i].SetActive(true);
            }

            realityScenes = GameObject.FindGameObjectsWithTag(ConfigGlobal.TAG_REALITYSCENE);
            if (realityScenes != null && realityScenes.Length > 0)
            {
                foreach (var realityScene in realityScenes)
                {
                    MeshRenderer meshRenderer = realityScene.GetComponent<MeshRenderer>();
                    if (meshRenderer != null)
                    {
                        Material realitySceneMaterial = meshRenderer.sharedMaterial;
                        realitySceneMaterial.SetFloat("_Alpha", 1);
                      //  realitySceneMaterial.SetFloat("_ShadowIntensity", 1);
                    }
                }
            }

            // 返回导出结果
            return ret;
        }

        // 场景打包
        static bool DoExportScene(UserConfig.ExportSceneData sceneData,out string bundlePath,bool bRevealInFinder)
        {
            Debug.Log("开始导出场景...");
            bool success = true;
            bundlePath = "";

            // export main scene，注意先删除原有文件夹
            UtilityFileSystem.DirectoryDeleteRF(sceneData.config.root_path);

            var exportSceneSuc = ExporterScene.ExportScene(sceneData.config);
            if (!exportSceneSuc)
            {
                return false;
            }
            ExporterUtility.PostProcess(sceneData.config);

            // 导出 sharelogo，放在root_path下
            if (null != sceneData.shareLogo)
            {
                string shareLogoPath = sceneData.config.root_path + "ShareLogo/";
                if (!Directory.Exists(shareLogoPath))
                    Directory.CreateDirectory(shareLogoPath);
                UtilityFileSystem.CopyFile(AssetDatabase.GetAssetPath(sceneData.shareLogo), shareLogoPath + "ShareLogo.png", true);
            }

            // 找出所有的require引用
            List<string> requires = new List<string>();
            foreach (var scriptPath in ExporterScene.scriptPathList)
            {
                if (string.Empty != scriptPath && File.Exists(scriptPath))
                {
                    var textAsset = (TextAsset)AssetDatabase.LoadAssetAtPath(scriptPath, typeof(TextAsset));
                    if (null != textAsset)
                    {
                        Regex regex = new Regex(@"require\s*\(\s*[""|'']\s*([^""'']+)\s*[""|'']\s*\)");
                        foreach (Match match in regex.Matches(textAsset.text))
                            requires.Add(regex.Replace(match.Value, @"$1"));
                    }
                }
            }

            // 导出默认JS资源库，放在root_path下
            string JSLibraryPath = sceneData.config.root_path + "JSLibrary/";

            List<string> always = new List<string>();
            if (null != sceneData.JSLibrariesName)
                foreach (var name in sceneData.JSLibrariesName) always.Add(name);

            foreach (string path in Directory.GetFiles(RenderEngine.ExporterConfig.JS_LIBRARY_PATH))
                if (System.IO.Path.GetExtension(path).ToLower().Equals(".js"))
                {
                    var name = System.IO.Path.GetFileName(path);
                    var nameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(path);
                    if (always.Contains(name) || requires.Contains(nameWithoutExtension))
                    {
                        if (!Directory.Exists(JSLibraryPath))
                            Directory.CreateDirectory(JSLibraryPath);
                        UtilityFileSystem.CopyFile(path, Path.Combine(JSLibraryPath, nameWithoutExtension + ".js"), true);
                    }
                }

            // 导出自定义JS资源库，放在root_path下
            if (null != sceneData.JSLibrariesCustomAsset)
            {
                for (int i = 0; i < sceneData.JSLibrariesCustomAsset.Count; i++)
                {
                    if (null != sceneData.JSLibrariesCustomAsset[i])
                    {
                        var path = AssetDatabase.GetAssetPath(sceneData.JSLibrariesCustomAsset[i]);
                        var name = System.IO.Path.GetFileName(path);
                        var nameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(path);
                        if ((null != sceneData.JSLibrariesCustomStatus && sceneData.JSLibrariesCustomStatus[i]) || requires.Contains(nameWithoutExtension))
                        {
                            if (!Directory.Exists(JSLibraryPath))
                                Directory.CreateDirectory(JSLibraryPath);
                            UtilityFileSystem.CopyFile(path, Path.Combine(JSLibraryPath, nameWithoutExtension + ".js"), true);
                        }
                    }
                }

            }

            if (Directory.Exists(sceneData.bundle_path))
                UtilityFileSystem.DirectoryDeleteRF(sceneData.bundle_path);

            Directory.CreateDirectory(sceneData.bundle_path);
            // 打ZIP包
            if (sceneData.bundle)
            {
                string bundle_file
                    = Path.Combine(sceneData.bundle_path, "scene_"
                    + UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
                    + System.DateTime.Now.ToString("yyMMddHHmmss")
                    + ExporterConfig.INSIGHT_BUNDLE_EXTENSION);
                ExporterUtility.CompressScene(sceneData.parentPath, bundle_file);

                //add by wy
                bundlePath = bundle_file;
                // 打好zip包后清空文件夹
                UtilityFileSystem.DirectoryDeleteRF(sceneData.parentPath);
            }
            // --------------------------------

            // ------------导出model------------
            if (sceneData.prefabs != null && sceneData.prefabs.Count > 0)
            {
                string current_path = UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene().path;
                foreach (UnityEditor.SceneAsset scene in sceneData.prefabs)
                {
                    if (null == scene)
                    {
                        continue;
                    }

                    // string sceneName = "model" + (count++).ToString();
                    string sceneName = scene.name;
                    // if (!bundle)
                    // 	config.root_path = Path.Combine(parentPath, sceneName + '/');
                    // else
                    // 	config.root_path = parentPath;
                    if (sceneData.bundle)
                        sceneData.config.root_path = sceneData.parentPath;

                    // load scene
                    string prefab_path_name_ext = sceneData.config.asset_path.GetPath(scene);
                    sceneData.config.scene_path = prefab_path_name_ext;

                    UnityEngine.Debug.Log("Export prefab: " + prefab_path_name_ext);
                    UnityEditor.SceneManagement.EditorSceneManager.OpenScene(prefab_path_name_ext, UnityEditor.SceneManagement.OpenSceneMode.Single);
                    success = success && ExporterScene.ExportScene(sceneData.config);

                    if (sceneData.bundle)
                    {
                        string bundle_file
                            = Path.Combine(sceneData.bundle_path, sceneName
                            + ExporterConfig.INSIGHT_BUNDLE_EXTENSION);
                        ExporterUtility.CompressScene(sceneData.parentPath, bundle_file);

                        //add by wy
                        bundlePath = bundle_file;
                        // 打好zip包后清空文件夹
                        UtilityFileSystem.DirectoryDeleteRF(sceneData.parentPath);
                    }
                }
                // return to the main scene
                if (string.Empty != current_path)
                    UnityEditor.SceneManagement.EditorSceneManager.OpenScene(current_path, UnityEditor.SceneManagement.OpenSceneMode.Single);
            }
            // --------------------------------

            if (success)
            {
                Debug.Log("场景资源导出成功！");
                // update by wy
                if (bRevealInFinder)
                {
                    if (RenderEngine.ExporterConfig.OpenTargetFolder)
                        EditorUtility.RevealInFinder(System.IO.Path.GetFullPath(sceneData.bundle ? sceneData.bundle_path : sceneData.parentPath));
                }
            }
            else
                Debug.Log("场景资源导出失败");

            return success;
        }

        // [MenuItem("EZXR/导出为洞见客户端资源/Bundle - iOS", false, 101)]
        // static void ExportUnityBundleiOSProduct()
        // {
        //     ExporterSceneTool.CreateSceneiOS();
        // }

        // [MenuItem("EZXR/导出为洞见客户端资源/Bundle - Android", false, 101)]
        // static void ExportUnityBundleAndroidProduct()
        // {
        //     ExporterSceneTool.CreateSceneAndroid();

        // }

        [MenuItem("EZXR/导出设置", false, 103)]
        static void ExportedConfig()
        {
            UserConfig.ExporterConfigWindow.ConfigDialog();
        }

        [MenuItem("EZXR/导出设置", true, 103)]
        static bool ValidateExportedConfig()
        {
            return UserController.UserLogin;
        }

        // [MenuItem("EZXR/算法设置", false, 300)]
        // static void EZXRConfig()
        // {
        // 	UserConfig.AlgConfigWindow.ConfigDialog();
        // }

        [MenuItem("EZXR/清空Shader缓存", false, 300)]
        static void ClearShaderCache()
        {
            if (EditorUtility.DisplayDialog(RenderEngine.ExporterConfig.TITLE, "确定清理吗？清理后Shader信息在导出时会重新生成", "确定", "取消"))
            {
                ShaderGenerator.ClearCache();
                EditorUtility.DisplayDialog(RenderEngine.ExporterConfig.TITLE, "清理成功", "确定");
            }

        }

        [MenuItem("EZXR/清空Shader缓存", true, 300)]
        static bool ValidateClearShaderCache()
        {
            return UserController.UserLogin;
        }

        [MenuItem("EZXR/帮助文档", false, 400)]
        static void HelpDocument()
        {
            Application.OpenURL("https://yixian-ezxr.feishu.cn/wiki/wikcnfwFcPlRdNAU1r0g1vlzhXd");
        }

        [MenuItem("EZXR/帮助文档", true, 400)]
        static bool ValidateHelpDocument()
        {
            return UserController.UserLogin;
        }

        [MenuItem("EZXR/关于", false, 500)]
        static void ShowInfo()
        {
            EditorUtility.DisplayDialog(RenderEngine.ExporterConfig.TITLE, "版本 v1.6.0", "确定");
        }

        [MenuItem("EZXR/关于", true, 500)]
        static bool ValidateShowInfo()
        {
            return UserController.UserLogin;
        }


        //[MenuItem("Exporter/Export Illumination Bundle", false, 2)]
        // static void ExportIllumination()
        // {
        // 	if (null == RenderSettings.skybox)
        // 	{
        // 		Debug.LogError("Cound not find a skybox texture, please assign one in the Lighting Panel.");
        // 	}
        // 	else
        // 	{
        // 		ExporterConfig config = new ExporterConfig(ExporterConfig.REPlatform.iOS);

        // 		config.cubemap_name = RenderSettings.skybox.name;

        // 		config.root_path = ExporterConfig.DEFAULT_COMPRESSED_FILE_PATH + config.cubemap_name;

        // 		UtilityFileSystem.DirectoryDeleteRF( config.root_path );
        // 		ExporterResource.ExportIllumination(config);
        // 	}
        // }

        static bool CheckEnvironment()
        {
#if UNITY_EDITOR
            if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.iOS && EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android)
            {
                if (EditorUtility.DisplayDialog(RenderEngine.ExporterConfig.TITLE,
                "资源导出工具需要切换到移动目标平台才能继续，是否切换？", "否", "是"))
                {
                    return false;
                }
                else
                {
                    int index = EditorUtility.DisplayDialogComplex(RenderEngine.ExporterConfig.TITLE,
                    "切换到iOS还是Android？（二者没有区别，可任选其一）", "iOS", "取消", "Android");

                    if (0 == index)
                        if (!EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.iOS, BuildTarget.iOS))
                        {
                            EditorUtility.DisplayDialog(ExporterConfig.TITLE, "请先下载iOS平台资源包", "OK");
                            return false;
                        }
                        else
                            Debug.Log("已成功切换到iOS平台。");
                    else if (2 == index)
                        if (!EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android))
                        {
                            EditorUtility.DisplayDialog(ExporterConfig.TITLE, "请先下载Android平台资源包", "OK");
                            return false;
                        }
                        else
                            Debug.Log("已成功切换到Android平台。");
                    else
                        return false;
                }
            }

            UnityEditor.Rendering.TierSettings currentSettings = UnityEditor.Rendering.EditorGraphicsSettings.GetTierSettings(BuildTargetGroup.Standalone, UnityEngine.Rendering.GraphicsTier.Tier1);
            UnityEditor.Rendering.TierSettings desiredSettings;
            if (!IsDesiredSettings(currentSettings, out desiredSettings))
                if (EditorUtility.DisplayDialog(ExporterConfig.TITLE,
                    "需要改变 Tier settings 才能继续，是否自动设置？", "Later", "Yes"))
                    return false;
                else
                {
                    UnityEditor.Rendering.EditorGraphicsSettings.SetTierSettings(BuildTargetGroup.Standalone, UnityEngine.Rendering.GraphicsTier.Tier1, desiredSettings);
                    Debug.Log("Tier settings 已被成功设置，详见Edit->Project Settings->Graphics");
                }

            return true;
#else
				return false;
#endif
        }

        private static bool IsDesiredSettings(UnityEditor.Rendering.TierSettings currentSettings, out UnityEditor.Rendering.TierSettings desiredSettings)
        {
            desiredSettings = new UnityEditor.Rendering.TierSettings();
            desiredSettings.standardShaderQuality = UnityEditor.Rendering.ShaderQuality.Medium;
            desiredSettings.hdrMode = UnityEngine.Rendering.CameraHDRMode.FP16;
            desiredSettings.renderingPath = RenderingPath.Forward;
            desiredSettings.realtimeGICPUUsage = UnityEngine.Rendering.RealtimeGICPUUsage.Low;
            //		desiredSettings.reflectionProbeBlending = true;
            desiredSettings.reflectionProbeBoxProjection = true;

            if (currentSettings.standardShaderQuality != desiredSettings.standardShaderQuality) return false;
            if (currentSettings.hdrMode != desiredSettings.hdrMode) return false;
            if (currentSettings.reflectionProbeBoxProjection != desiredSettings.reflectionProbeBoxProjection) return false;
            if (currentSettings.reflectionProbeBlending != desiredSettings.reflectionProbeBlending) return false;
            if (currentSettings.hdr != desiredSettings.hdr) return false;
            if (currentSettings.detailNormalMap != desiredSettings.detailNormalMap) return false;
            if (currentSettings.cascadedShadowMaps != desiredSettings.cascadedShadowMaps) return false;
#if !UNITY_5_6
            if (currentSettings.prefer32BitShadowMaps != desiredSettings.prefer32BitShadowMaps) return false;
            if (currentSettings.enableLPPV != desiredSettings.enableLPPV) return false;
#endif
            if (currentSettings.semitransparentShadows != desiredSettings.semitransparentShadows) return false;
            if (currentSettings.renderingPath != desiredSettings.renderingPath) return false;
            if (currentSettings.realtimeGICPUUsage != desiredSettings.realtimeGICPUUsage) return false;

            return true;
        }

        static void CreateReverseState(UnityEditor.Animations.AnimatorStateMachine machine)
        {
            foreach (var state in machine.states)
            {
                // is reversed state?
                if (state.state.speed < 0)
                    continue;

                // generate reversed state name
                string reversed_name = ExporterConfig.GenReversedStateName(state.state.name);

                // find the state's reversed state
                bool found = false;
                foreach (var s in machine.states)
                {
                    if (reversed_name.Equals(s.state.name))
                        found = true;
                }
                if (found)
                    continue;

                // create reversed state
                var reversed_state = machine.AddState(reversed_name);
                reversed_state.motion = state.state.motion;
                reversed_state.speed = -state.state.speed;
                reversed_state.speedParameter = state.state.speedParameter;
                reversed_state.speedParameterActive = state.state.speedParameterActive;
                reversed_state.mirror = state.state.mirror;
                reversed_state.mirrorParameter = state.state.mirrorParameter;
                reversed_state.mirrorParameterActive = state.state.mirrorParameterActive;
                reversed_state.cycleOffset = state.state.cycleOffset;
                reversed_state.cycleOffsetParameter = state.state.cycleOffsetParameter;
                reversed_state.cycleOffsetParameterActive = state.state.cycleOffsetParameterActive;
                reversed_state.iKOnFeet = state.state.iKOnFeet;
                reversed_state.writeDefaultValues = state.state.writeDefaultValues;
            }

            foreach (var subsm in machine.stateMachines)
            {
                CreateReverseState(subsm.stateMachine);
            }
        }


    }
}
#endif