using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

[InitializeOnLoad]
public class IssueFix : EditorWindow
{
    static ARSDKConfig config;
    static Texture2D logo;
    static bool allFixed;
    /// <summary>
    /// 目标系统平台，必须是Android
    /// </summary>
    static bool targetPlatform;
    /// <summary>
    /// SDK是否为C/S架构，如果是的话需要向ScriptingDefineSymbols添加CS符号，否则移除
    /// </summary>
    static bool clientServerMode;
    /// <summary>
    /// GraphicsAPI必须是OpenGLES3或OpenGLES2，否则为false
    /// </summary>
    static bool graphicsAPIOK;
    /// <summary>
    /// Android最小支持sdk版本，大于等于26为true
    /// </summary>
    static bool minimum_API_Level;
    /// <summary>
    /// 编译此项目用到的sdk版本
    /// </summary>
    static bool target_API_Level;
    /// <summary>
    /// 是Mono还是IL2CPP，IL2CPP为true
    /// </summary>
    static bool scripting_Backend;
    /// <summary>
    /// 目标平台为ARM64为true
    /// </summary>
    static bool target_Architectures;
    /// <summary>
    /// 允许unsafe为true
    /// </summary>
    static bool allow_Unsafe_Code;
    /// <summary>
    /// 禁用MultiThreadedRendering为true
    /// </summary>
    static bool disableMultiThreadedRendering;

    static IssueFix()
    {
        AssetDatabase.importPackageCompleted -= AssetDatabase_importPackageCompleted;
        AssetDatabase.importPackageCompleted += AssetDatabase_importPackageCompleted;
    }

    private void OnEnable()
    {
        Init();
        FixTags();
    }

    private static void AssetDatabase_importPackageCompleted(string packageName)
    {
        if (packageName.Contains("EZXR_ARGlass_SDK"))
        {
            string filePath = "EZXR_CICD.cfg";
            if (File.Exists(filePath))
            {
                FixAllIssues();
                File.WriteAllText(filePath, "AllFixed");
            }
            else
            {
                ShowWindow();
            }
        }
    }

    [MenuItem("ARSDK/Project IssueFix", false, 50)]
    public static void ShowWindow()
    {
        EditorWindow window = EditorWindow.GetWindow(typeof(IssueFix), false, "Project IssueFix");
        window.minSize = new Vector2(300, 300);
    }

    /// <summary>
    /// 进行所有代码之前必须先执行的初始化操作，取到ARSDK的配置文件
    /// </summary>
    static void Init()
    {
        string filePath = "Assets/EZXRGlassSDK/Editor/ARSDKConfig.asset";
        if ((AssetDatabase.LoadAssetAtPath(filePath, typeof(ARSDKConfig)) as ARSDKConfig) == null)
        {
            filePath = AssetDatabase.GUIDToAssetPath("2c84fdf00cdace44ba329395d9e29d08");
        }
        config = AssetDatabase.LoadAssetAtPath(filePath, typeof(ARSDKConfig)) as ARSDKConfig;
    }

    void OnGUI()
    {
        CheckAllIssues();

        if (logo == null)
        {
            logo = Resources.Load<Texture2D>("ezxr_logo");
        }

        GUIStyle label_Red = new GUIStyle(EditorStyles.label);
        label_Red.normal.textColor = Color.red;


        GUILayout.Button(logo, GUILayout.Height(200));

        EditorGUILayout.Separator();

        if (graphicsAPIOK && minimum_API_Level & scripting_Backend & target_Architectures & allow_Unsafe_Code && disableMultiThreadedRendering)
        {
            EditorGUILayout.HelpBox("Everything is OK!", MessageType.Info);
        }
        else
        {
            EditorGUILayout.HelpBox("For AR Glass Project, all issues below must be fixed, or your project cannot run on AR Glass devices!", MessageType.Warning);
        }

        EditorGUILayout.Separator();

        if (!targetPlatform)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Platform:");
            GUILayout.FlexibleSpace();
            GUILayout.Label("Should be 'Android'", label_Red);
            if (GUILayout.Button("Fix"))
            {
                FixTargetPlatform();
            }
            GUILayout.EndHorizontal();

            EditorGUILayout.Separator();
        }

        if (!clientServerMode)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("C/S:");
            GUILayout.FlexibleSpace();
            GUILayout.Label("Should be Fixed", label_Red);
            if (GUILayout.Button("Fix"))
            {
                FixClientServerMode();
            }
            GUILayout.EndHorizontal();

            EditorGUILayout.Separator();
        }

        if (!graphicsAPIOK)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("GraphicsAPI Status:");
            GUILayout.FlexibleSpace();
            GUILayout.Label("Should be 'OpenGLES3 or OpenGLES2'", label_Red);
            if (GUILayout.Button("Fix"))
            {
                FixGraphicsAPI();
            }
            GUILayout.EndHorizontal();

            EditorGUILayout.Separator();
        }

        if (!minimum_API_Level)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Minimum API Level:");
            GUILayout.FlexibleSpace();
            GUILayout.Label("Recommend be '26 or higher'", label_Red);
            if (GUILayout.Button("Fix"))
            {
                FixMinimumAPILevel();
            }
            GUILayout.EndHorizontal();

            EditorGUILayout.Separator();
        }

        if (!target_API_Level)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Target API Level: ");
            GUILayout.FlexibleSpace();
            GUILayout.Label("Recommend be '27'", label_Red);
            if (GUILayout.Button("Fix"))
            {
                FixTargetAPILevel();
            }
            GUILayout.EndHorizontal();

            EditorGUILayout.Separator();
        }

        if (!scripting_Backend)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Scripting Backend: ");
            GUILayout.FlexibleSpace();
            GUILayout.Label("Should be 'IL2CPP'", label_Red);
            if (GUILayout.Button("Fix"))
            {
                FixScriptingBackend();
            }
            GUILayout.EndHorizontal();

            EditorGUILayout.Separator();
        }

        if (!target_Architectures)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Target Architectures: ");
            GUILayout.FlexibleSpace();
            GUILayout.Label("Should be 'ARM64'", label_Red);
            if (GUILayout.Button("Fix"))
            {
                FixTargetArchitectures();
            }
            GUILayout.EndHorizontal();

            EditorGUILayout.Separator();
        }

        if (!allow_Unsafe_Code)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Allow Unsafe Code: ");
            GUILayout.FlexibleSpace();
            GUILayout.Label("Should be 'Checked'", label_Red);
            if (GUILayout.Button("Fix"))
            {
                FixAllowUnsafeCode();
            }
            GUILayout.EndHorizontal();

            EditorGUILayout.Separator();
        }

        if (!disableMultiThreadedRendering)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("MultiThreadedRendering: ");
            GUILayout.FlexibleSpace();
            GUILayout.Label("MultiThreadedRendering must be 'Disabled'", label_Red);
            if (GUILayout.Button("Fix"))
            {
                FixMultiThreadedRendering();
            }
            GUILayout.EndHorizontal();
        }
    }

    static void CheckAllIssues()
    {
        //目标系统平台必须是Android
        BuildTarget buildTarget = EditorUserBuildSettings.activeBuildTarget;
        if (buildTarget == BuildTarget.Android)
        {
            targetPlatform = true;
        }
        else
        {
            targetPlatform = false;
        }

        var symbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);
        clientServerMode = (config.clientServerMode == symbols.Contains("EZXRCS"));

        //GraphicsAPI必须是OpenGLES3或OpenGLES2，否则为false
        GraphicsDeviceType[] graphicsAPIs = PlayerSettings.GetGraphicsAPIs(BuildTarget.Android);
        foreach (GraphicsDeviceType item in graphicsAPIs)
        {
            if (item != GraphicsDeviceType.OpenGLES3 && item != GraphicsDeviceType.OpenGLES2)
            {
                graphicsAPIOK = false;
                break;
            }
            else
            {
                graphicsAPIOK = true;
            }
        }

        //Android最小支持sdk版本
        AndroidSdkVersions androidSdkVersions = PlayerSettings.Android.minSdkVersion;
        if ((int)androidSdkVersions >= 26)
        {
            minimum_API_Level = true;
        }
        else
        {
            minimum_API_Level = false;
        }

        //用于编译项目的sdk版本
        AndroidSdkVersions targetAndroidSdkVersions = PlayerSettings.Android.targetSdkVersion;
        if ((int)targetAndroidSdkVersions > 26 && (int)targetAndroidSdkVersions < 29)
        {
            target_API_Level = true;
        }
        else
        {
            target_API_Level = false;
        }

        //脚本backend
        if (PlayerSettings.GetScriptingBackend(BuildTargetGroup.Android) == ScriptingImplementation.IL2CPP)
        {
            scripting_Backend = true;
        }
        else
        {
            scripting_Backend = false;
        }

        //cpu架构
        if (PlayerSettings.Android.targetArchitectures == AndroidArchitecture.ARM64)
        {
            target_Architectures = true;
        }
        else
        {
            target_Architectures = false;
        }

        //是否允许unsafe代码
        if (PlayerSettings.allowUnsafeCode)
        {
            allow_Unsafe_Code = true;
        }
        else
        {
            allow_Unsafe_Code = false;
        }

        //是否禁用MultiThreadedRendering
        if (PlayerSettings.GetMobileMTRendering(BuildTargetGroup.Android))
        {
            disableMultiThreadedRendering = false;
        }
        else
        {
            disableMultiThreadedRendering = true;
        }

        allFixed = targetPlatform && clientServerMode && graphicsAPIOK && minimum_API_Level & scripting_Backend & target_Architectures & allow_Unsafe_Code && disableMultiThreadedRendering;

        //if (all)
        //{
        //    EditorApplication.update -= CheckAllIssuesForUpdate;
        //    updateChecking = false;
        //}
        //else
        //{
        //    if (!updateChecking)
        //    {
        //        EditorApplication.update = CheckAllIssuesForUpdate;
        //        updateChecking = true;
        //    }
        //}
    }

    static void FixTags()
    {
        string[] tags = new string[] { "SpacialObject", "SpacialUI", "SpacialHandler" };
        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAssetAtPath("ProjectSettings/TagManager.asset", typeof(Object)));
        SerializedProperty tagsProp = tagManager.FindProperty("tags");

        //Debug.Log("TagsPorp Size:" + tagsProp.arraySize);

        List<string> tagsExists = new List<string>();
        for (int i = 0; i < tagsProp.arraySize; i++)
        {
            tagsExists.Add(tagsProp.GetArrayElementAtIndex(i).stringValue);
        }

        foreach (string tag in tags)
        {
            if (!tagsExists.Contains(tag))
            {
                tagsExists.Add(tag);
            }
        }

        tagsProp.ClearArray();

        foreach (string tag in tagsExists)
        {
            tagsProp.InsertArrayElementAtIndex(Mathf.Clamp(0, tagsProp.arraySize - 1, 128));
            tagsProp.GetArrayElementAtIndex(tagsProp.arraySize - 1).stringValue = tag;
        }
        tagManager.ApplyModifiedProperties();

    }

    static void FixTargetPlatform()
    {
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);
    }

    static void FixClientServerMode()
    {
        if (config.clientServerMode)
        {
            //向ScriptingDefineSymbols添加EZXRCS
            var symbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);
            if (!symbols.Contains("EZXRCS"))
            {
                symbols += ";EZXRCS";
                PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, symbols);
            }
        }
        else
        {
            //去掉ScriptingDefineSymbols中的EZXRCS，避免移除package的时候会因为引用丢失而报错
            string symbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);
            if (symbols.Contains("EZXRCS;"))
            {
                symbols = symbols.Replace("EZXRCS;", "");
                PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, symbols);
            }
            else if (symbols.Contains("EZXRCS"))
            {
                symbols = symbols.Replace("EZXRCS", "");
                PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, symbols);
            }
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    static void FixGraphicsAPI()
    {
        PlayerSettings.SetGraphicsAPIs(BuildTarget.Android, new GraphicsDeviceType[] { GraphicsDeviceType.OpenGLES3, GraphicsDeviceType.OpenGLES2 });
    }
    static void FixMinimumAPILevel()
    {
        PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel26;
    }
    static void FixTargetAPILevel()
    {
        PlayerSettings.Android.targetSdkVersion = AndroidSdkVersions.AndroidApiLevel27;
    }
    static void FixScriptingBackend()
    {
        PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);
    }
    static void FixTargetArchitectures()
    {
        PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64;
    }
    static void FixAllowUnsafeCode()
    {
        PlayerSettings.allowUnsafeCode = true;
    }
    static void FixMultiThreadedRendering()
    {
        PlayerSettings.SetMobileMTRendering(BuildTargetGroup.Android, false);
    }

    public static void FixAllIssues()
    {
        Init();
        FixTags();
        FixTargetPlatform();
        FixClientServerMode();
        FixGraphicsAPI();
        FixMinimumAPILevel();
        FixTargetAPILevel();
        FixScriptingBackend();
        FixTargetArchitectures();
        FixAllowUnsafeCode();
        FixMultiThreadedRendering();
    }
}