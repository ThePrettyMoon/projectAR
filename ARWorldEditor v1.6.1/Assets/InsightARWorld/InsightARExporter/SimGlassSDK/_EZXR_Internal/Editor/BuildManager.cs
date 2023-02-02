using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class BuildManager : EditorWindow
{
    /// <summary>
    /// 每个目标眼镜对应的aar的部分路径
    /// </summary>
    static Dictionary<string, string[]> exportAARPaths = new Dictionary<string, string[]>()
    {
        {"Sunny",new string[]{ "ezxrGlasslib-sunny-" } },
        {"SIM/Z1C",new string[]{ "ezxrGlasslib-sim-" } },
        {"SIM/Z1C(CS)",new string[]{ "ezglassserverlib-sim-release" } },
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


    [MenuItem("ARSDK/Build(Internal Use)/Sunny", false, 10)]
    public static void Build_Sunny()
    {
        Build("Sunny");
    }
    [MenuItem("ARSDK/Build(Internal Use)/Sunny", true, 10)]
    static bool ValidateBuild_Sunny()
    {
        return CheckAARExists("Sunny");
    }

    [MenuItem("ARSDK/Build(Internal Use)/SIM/Z1C", false, 11)]
    public static void Build_SIM()
    {
        Build("SIM/Z1C");
    }
    [MenuItem("ARSDK/Build(Internal Use)/SIM/Z1C", true, 11)]
    static bool ValidateBuild_SIM()
    {
        return CheckAARExists("SIM/Z1C");
    }

    [MenuItem("ARSDK/Build(Internal Use)/SIM/Z1C(CS)", false, 11)]
    public static void Build_SIMCS()
    {
        Build("SIM/Z1C(CS)");
    }
    [MenuItem("ARSDK/Build(Internal Use)/SIM/Z1C(CS)", true, 11)]
    static bool ValidateBuild_SIMCS()
    {
        return CheckAARExists("SIM/Z1C(CS)");
    }

    [MenuItem("ARSDK/Build(Internal Use)/SIM/001", false, 12)]
    public static void Build_SIM001()
    {
        Build("SIM/001");
    }
    [MenuItem("ARSDK/Build(Internal Use)/SIM/001", true, 12)]
    static bool ValidateBuild_SIM001()
    {
        return CheckAARExists("SIM/001");
    }

    [MenuItem("ARSDK/Build(Internal Use)/Rokid/Airpro/Box", false, 13)]
    public static void Build_RokidAirproBox()
    {
        Build("Rokid/AirPro/Box");
    }
    [MenuItem("ARSDK/Build(Internal Use)/Rokid/Airpro/Box", true, 13)]
    static bool ValidateBuild_RokidAirproBox()
    {
        return CheckAARExists("Rokid/AirPro/Box");
    }

    [MenuItem("ARSDK/Build(Internal Use)/Rokid/Airpro/Phone", false, 13)]
    public static void Build_RokidAirproPhone()
    {
        Build("Rokid/AirPro/Phone");
    }
    [MenuItem("ARSDK/Build(Internal Use)/Rokid/Airpro/Phone", true, 13)]
    static bool ValidateBuild_RokidAirproPhone()
    {
        return CheckAARExists("Rokid/AirPro/Phone");
    }

    [MenuItem("ARSDK/Build(Internal Use)/GOER", false, 10)]
    public static void Build_GOER()
    {
        Build("GOER");
    }
    [MenuItem("ARSDK/Build(Internal Use)/GOER", true, 10)]
    static bool ValidateBuild_GOER()
    {
        return CheckAARExists("GOER");
    }


    static bool CheckAARExists(string glassType)
    {
        string path = Path.Combine(Application.dataPath, "EZXRGlassSDK/Plugins/Android/libs");
        string[] aarPaths = Directory.GetFiles(path, "*.aar");
        foreach (string aarPath in aarPaths)
        {
            if (aarPath.Contains(exportAARPaths[glassType][0]))
            {
                return true;
            }
        }
        return false;
    }

    static void Build(string glassType, bool showBuildSettingsWindow = true)
    {
        SwitchTarget(glassType);

        if (showBuildSettingsWindow)
        {
            GetWindow<BuildPlayerWindow>(false, "Build Settings");
        }
    }

    /// <summary>
    /// 切换目标眼镜平台的配置
    /// </summary>
    /// <param name="glassType"></param>
    public static void SwitchTarget(string glassType)
    {
        SetAARPlatform(glassType);
        SetGradleTemplate(glassType);
        SetAndroidManifest(glassType);
        SetScriptingDefineSymbols(glassType);
        SelectTargetAPILevel();
    }

    static void SetAARPlatform(string glassType)
    {
        string aarDirPath = Path.Combine(Application.dataPath, "EZXRGlassSDK/Plugins/Android/libs");
        //把所有aar的Android平台选项先取消掉，然后按照需要的平台重新勾选
        string[] aarPaths = Directory.GetFiles(aarDirPath, "*.aar");
        foreach (string aarPath in aarPaths)
        {
            string relativeAARPath = aarPath.Substring(aarPath.IndexOf("Assets"));
            AssetImporter assetImporter = AssetImporter.GetAtPath(relativeAARPath);
            if (assetImporter != null)
            {
                ((PluginImporter)assetImporter).SetCompatibleWithPlatform(BuildTarget.Android, false);
                assetImporter.SaveAndReimport();
            }
        }

        //设置要导出的aar的目标平台为Android
        foreach (string path in exportAARPaths[glassType])
        {
            foreach (string aarPath in aarPaths)
            {
                if (aarPath.Contains(path))
                {
                    string relativeAARPath = aarPath.Substring(aarPath.IndexOf("Assets"));
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
    }

    static void SetGradleTemplate(string glassType)
    {
        string rootPluginsDirPath = Path.Combine(Application.dataPath, "Plugins/Android");
        string[] files = Directory.GetFiles(rootPluginsDirPath);

        if (glassType == "Rokid/AirPro/Phone")
        {
            foreach (string file in files)
            {
                if (file.Contains("baseProjectTemplate.gradle.DISABLED"))
                {
                    File.Move(file, file.Replace(".DISABLED", ""));
                }
                if (file.Contains("mainTemplate.gradle.DISABLED"))
                {
                    File.Move(file, file.Replace(".DISABLED", ""));
                }
            }
            AssetDatabase.Refresh();
        }
        else
        {
            foreach (string file in files)
            {
                if (file.Contains("baseProjectTemplate.gradle") && !file.Contains("baseProjectTemplate.gradle.DISABLED"))
                {
                    File.Move(file, file.Replace(".gradle", ".gradle.DISABLED"));
                }
                if (file.Contains("mainTemplate.gradle") && !file.Contains("mainTemplate.gradle.DISABLED"))
                {
                    File.Move(file, file.Replace(".gradle", ".gradle.DISABLED"));
                }
            }
            AssetDatabase.Refresh();
        }
    }

    static void SetAndroidManifest(string glassType)
    {
        string rootPluginsDirPath = Path.Combine(Application.dataPath, "Plugins/Android");
        string[] files = Directory.GetFiles(rootPluginsDirPath);

        if (glassType == "SIM/Z1C(CS)")
        {
            foreach (string file in files)
            {
                if (file.Contains("AndroidManifest.xml.DISABLED"))
                {
                    File.Move(file, file.Replace(".DISABLED", ""));
                }
            }
        }
        else
        {
            foreach (string file in files)
            {
                if (file.Contains("AndroidManifest.xml") && !file.Contains("AndroidManifest.xml.DISABLED"))
                {
                    File.Move(file, file.Replace(".xml", ".xml.DISABLED"));
                }
            }
        }
        AssetDatabase.Refresh();
    }

    static void SetScriptingDefineSymbols(string glassType)
    {
        if (glassType == "SIM/Z1C(CS)")
        {
            //向ScriptingDefineSymbols添加CS
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

    /// <summary>
    /// 手动选择编译用的TargetAPILevel
    /// </summary>
    static void SelectTargetAPILevel()
    {
        if (EditorUtility.DisplayDialog("Choose Target API Level", "Please choose Target API Level:", "API Level 27", "API Level 28"))
        {
            PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel27;
            PlayerSettings.Android.targetSdkVersion = AndroidSdkVersions.AndroidApiLevel27;
        }
        else
        {
            PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel28;
            PlayerSettings.Android.targetSdkVersion = AndroidSdkVersions.AndroidApiLevel28;
        }
        AssetDatabase.SaveAssets();
    }
}
