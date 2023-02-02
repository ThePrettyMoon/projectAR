using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class ExportSDK : EditorWindow
{
    public ARSDKConfig config;
    string curPlatform;
    string version;

    /// <summary>
    /// 是否导出CS架构的SDK
    /// </summary>
    bool clientServerMode;
    bool clientServerMode_LastTime;

    /// <summary>
    /// 空间追踪
    /// </summary>
    bool spatialTracking;
    /// <summary>
    /// 空间定位
    /// </summary>
    bool spatialPositioning;
    /// <summary>
    /// 手部追踪
    /// </summary>
    bool handTracking;
    /// <summary>
    /// 图像检测
    /// </summary>
    bool imageDetection;
    /// <summary>
    /// 物体检测
    /// </summary>
    bool objectDetection;

    /// <summary>
    /// 投屏功能
    /// </summary>
    bool miracast;

    /// <summary>
    /// 所有AAR所在的目录路径
    /// </summary>
    string exportAARPath;

    /// <summary>
    /// 每个目标眼镜对应的aar的部分路径，用于从exportAARPath中找到aar
    /// </summary>
    Dictionary<string, string[]> exportAARPaths;

    /// <summary>
    /// （Standalone模式下）每个目标眼镜对应的aar的部分路径，用于从exportAARPath中找到aar
    /// </summary>
    Dictionary<string, string[]> exportAARPaths_Standalone = new Dictionary<string, string[]>()
    {
        {"Sunny",new string[]{ "ezxrGlasslib-sunny-" } },
        {"SIM/Z1C",new string[]{ "ezxrGlasslib-sim-" } },
        {"SIM/001",new string[]{ "ezxrGlasslib-sim_001-" } },
        //{"Rokid/Xcraft",new string[]{ "Assets/EZXRGlassSDK/Plugins/Android/libs/ezglasslib-rokid_xcraft-release.aar" } },
        {"Rokid/AirPro/Box",new string[]{ "ezxrGlasslib-rokid_airpro-" } },
        {"Rokid/AirPro/Phone",new string[]
            {
                "ezxrGlasslib-rokid_airpro_phone-",
                "Assets/Plugins/Android/baseProjectTemplate.gradle",
                "Assets/Plugins/Android/mainTemplate.gradle"
            }
        },
        {"GOER",new string[]{ "ezxrGlasslib-goertek-" } },
    };

    /// <summary>
    /// （ClientServer模式下）每个目标眼镜对应的aar的部分路径，用于从exportAARPath中找到aar
    /// </summary>
    Dictionary<string, string[]> exportAARPaths_ClientServer = new Dictionary<string, string[]>()
    {
        {"SIM/Z1C",new string[]{ "ezglassserverlib-sim-" } },
    };

    /// <summary>
    /// 每个目标眼镜独有导出文件的路径
    /// </summary>
    Dictionary<string, string[]> exportExclusiveFilePaths = new Dictionary<string, string[]>()
    {
        {"Rokid/AirPro/Phone",new string[]
            {
                "Assets/Plugins/Android/baseProjectTemplate.gradle",
                "Assets/Plugins/Android/mainTemplate.gradle"
            }
        },
    };

    // Add menu to the Window menu
    [MenuItem("ARSDK/ExportSDK(Internal Use) &e", false, 30)]
    static void ShowWindow()
    {
        // Get existing open window or if none, make a new one:
        ExportSDK window = (ExportSDK)EditorWindow.GetWindow(typeof(ExportSDK));
        window.minSize = new Vector2(600, 300);
        window.Show();
    }

    void Init()
    {
        //取第一个眼镜平台作为当前平台
        if (clientServerMode)
        {
            exportAARPaths = exportAARPaths_ClientServer;
        }
        else
        {
            exportAARPaths = exportAARPaths_Standalone;
        }

        foreach (string key in exportAARPaths.Keys)
        {
            curPlatform = key;
            break;
        }
    }

    void OnEnable()
    {
        Init();

        exportAARPath = Path.Combine(Application.dataPath, "EZXRGlassSDK/Plugins/Android/libs");

        clientServerMode = config.clientServerMode;

        if (!string.IsNullOrEmpty(config.curPlatform))
        {
            curPlatform = config.curPlatform;
        }

        version = config.version;
        spatialTracking = config.spatialTracking;
        spatialPositioning = config.spatialPositioning;
        handTracking = config.handTracking;
        imageDetection = config.imageDetection;
        objectDetection = config.objectDetection;
    }

    void OnGUI()
    {
        EditorGUILayout.Foldout(true, "SDK类型");
        EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
        clientServerMode = !EditorGUILayout.ToggleLeft("Standalone", !clientServerMode, GUILayout.Width(150));
        clientServerMode = EditorGUILayout.ToggleLeft("C/S", clientServerMode, GUILayout.Width(150));
        if (clientServerMode != clientServerMode_LastTime)
        {
            clientServerMode_LastTime = clientServerMode;
            Init();
            Debug.Log("hhe");
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();

        EditorGUILayout.Foldout(true, "目标平台");
        if (GUILayout.Button(curPlatform))
        {
            GenericMenu menu = new GenericMenu();

            foreach (string key in exportAARPaths.Keys)
            {
                AddMenuItemForPlatform(menu, key);
            }

            menu.ShowAsContext();
        }
        EditorGUILayout.Space();

        EditorGUILayout.Foldout(true, "核心功能");
        EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
        spatialTracking = EditorGUILayout.ToggleLeft("Spatial Tracking", spatialTracking, GUILayout.Width(150));
        spatialPositioning = EditorGUILayout.ToggleLeft("Spatial Positioning", spatialPositioning, GUILayout.Width(150));
        handTracking = EditorGUILayout.ToggleLeft("Hand Tracking", handTracking, GUILayout.Width(150));
        imageDetection = EditorGUILayout.ToggleLeft("Image Detection", imageDetection, GUILayout.Width(150));
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();

        EditorGUILayout.Foldout(true, "附加功能");
        EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
        miracast = EditorGUILayout.ToggleLeft("MiraCast", miracast, GUILayout.Width(150));
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();

        EditorGUILayout.Foldout(true, "版本号");
        EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
        EditorGUILayout.LabelField("请输入版本号：");
        version = EditorGUILayout.TextField(version);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();

        if (GUILayout.Button("Export"))
        {
            string tarDirectory = EditorUtility.OpenFolderPanel("请选择目标目录：", "", "");
            if (!string.IsNullOrEmpty(tarDirectory))
            {
                //所有sdk都应该包含的导出部分
                List<string> paths = new List<string>()
                {
                    "Assets/Plugins/Android/LauncherManifest.xml",
                    "Assets/EZXRGlassSDK/Editor",
                    "Assets/EZXRGlassSDK/Core/Shared",
                    "Assets/EZXRGlassSDK/Core/Helpers",
                };

                //设置要导出的aar的目标平台为Android
                string[] aarPaths = Directory.GetFiles(exportAARPath, "*.aar");
                foreach (string path in exportAARPaths[curPlatform])
                {
                    foreach (string aarPath in aarPaths)
                    {
                        if (aarPath.Contains(path))
                        {
                            string relativeAARPath = aarPath.Substring(aarPath.IndexOf("Assets"));
                            paths.Add(relativeAARPath);
                            AssetImporter assetImporter = AssetImporter.GetAtPath(relativeAARPath);
                            if (assetImporter != null)
                            {
                                ((PluginImporter)assetImporter).SetCompatibleWithPlatform(BuildTarget.Android, true);
                                assetImporter.SaveAndReimport();
                            }
                            break;
                        }
                    }
                }

                //切换到对应眼镜平台的配置后再进行导出操作
                BuildManager.SwitchTarget(curPlatform);

                if (clientServerMode)
                {
                    paths.Add("Assets/Plugins/Android/AndroidManifest.xml");
                }

                //如果要导出“6dof功能”则在导出路径中加入这些
                if (spatialTracking)
                {
                    paths.Add("Assets/EZXRGlassSDK/Core/SixDof");
                    paths.Add("Assets/EZXRGlassSDK/Demos/SpatialTracking");
                    paths.Add("Assets/EZXRGlassSDK/Demos/PlaneDetection");
                    //paths.Add("Assets/EZXRGlassSDK/Core/SpatialMesh");
                    //paths.Add("Assets/EZXRGlassSDK/Demos/SpatialMesh");
                }

                //如果要导出“空间定位”则在导出路径中加入这些
                if (spatialPositioning)
                {
                    paths.Add("Assets/EZXRGlassSDK/Core/SpatialPositioning");
                    paths.Add("Assets/EZXRGlassSDK/Demos/SpatialPositioning");
                }

                //如果要导出“手部追踪功能”则在导出路径中加入这些
                if (handTracking)
                {
                    paths.Add("Assets/EZXRGlassSDK/Core/HandTracking");
                    paths.Add("Assets/EZXRGlassSDK/Demos/HandTracking");
                }

                //如果要导出“图像追踪功能”则在导出路径中加入这些
                if (imageDetection)
                {
                    paths.Add("Assets/EZXRGlassSDK/Core/ImageDetection");
                    paths.Add("Assets/StreamingAssets");
                    paths.Add("Assets/EZXRGlassSDK/Demos/ImageDetection");
                }

                //如果要导出“MiraCast功能”则在导出路径中加入这些
                if (miracast)
                {
                    paths.Add("Assets/EZXRGlassSDK/Core/MiraCast");
                    paths.Add("Assets/EZXRGlassSDK/Demos/MiraCast");
                    paths.Add("Assets/EZXRGlassSDK/Plugins/Android/NatCorder.aar");
                    paths.Add("Assets/EZXRGlassSDK/Plugins/Android/NatRender.aar");
                }

                if (exportExclusiveFilePaths.ContainsKey(curPlatform))
                {
                    paths.AddRange(exportExclusiveFilePaths[curPlatform]);
                }
                string tarFilePath = Path.Combine(tarDirectory, "EZXR_ARGlass_SDK_Unity_" + version + "_For" + curPlatform.Replace('/', '_') + ".unitypackage");
                AssetDatabase.ExportPackage(paths.ToArray(), tarFilePath, ExportPackageOptions.Recurse | ExportPackageOptions.Interactive);

                config.clientServerMode = clientServerMode;
                config.curPlatform = curPlatform;
                config.version = version;
                config.spatialTracking = spatialTracking;
                config.spatialPositioning = spatialPositioning;
                config.handTracking = handTracking;
                config.imageDetection = imageDetection;
                EditorUtility.SetDirty(config);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }
    }

    void AddMenuItemForPlatform(GenericMenu menu, string platformName)
    {
        menu.AddItem(new GUIContent(platformName), platformName.Equals(curPlatform), OnPlatformSelected, platformName);
    }

    void OnPlatformSelected(object platformName)
    {
        curPlatform = (string)platformName;
    }
}