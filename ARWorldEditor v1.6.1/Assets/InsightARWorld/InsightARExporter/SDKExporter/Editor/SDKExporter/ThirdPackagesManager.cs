#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager.Requests;
using UnityEditor.PackageManager;
using UnityEngine;
using System.IO;
using ARWorldEditor;

[InitializeOnLoad]
public class ThirdPackagesManager
{
    private const string savePath = "InsightARWorld/InsightARExporter/ThirdComponent/";


    private const string MenuItemRoot = "EZXR/插件扩展/";

    private const string BoltPackageName = "bolt_1.4.13";
    private const string BoltPath = MenuItemRoot + BoltPackageName;

    private const string FimpossibleCreationsPackageName = "fimpossible_creations_2.0.0";
    private const string FimpossibleCreationsPath = MenuItemRoot + FimpossibleCreationsPackageName;

    private const string FlockBoxPackageName = "flockbox_1.7";
    private const string FlockBoxPath = MenuItemRoot + FlockBoxPackageName;

    private const string PolyBrushPackageName = "polybrush_1.0.2";
    private const string PolyBrushPath = MenuItemRoot + PolyBrushPackageName;

    private const string PostProcessingPackageName = "postprocessing_2.3.0";
    private const string PostProcessingPath = MenuItemRoot + PostProcessingPackageName;

    private const string SoundVisualizerPackageName = "soundvisualizer-ColorWaves_2.0";
    private const string SoundVisualizerPath = MenuItemRoot + SoundVisualizerPackageName;

    private const string SoxAnimationPackageName = "soxanimationtoolkit_1.102";
    private const string SoxAnimationPath = MenuItemRoot + SoxAnimationPackageName;

    private const string SpinePackageName = "spine_3.8";
    private const string SpinePath = MenuItemRoot + SpinePackageName;

    private const string TimeLinePackageName = "timeline_1.2.17";
    private const string TimeLinePath = MenuItemRoot + TimeLinePackageName;

    /// <summary>
    /// 维护的插件包
    /// </summary>
    public static Dictionary<string, bool> TargetList { get; set; } = new Dictionary<string, bool>()
    {
        [BoltPackageName] = false,
        [FimpossibleCreationsPackageName] = false,
        [FlockBoxPackageName] = false,
        [PolyBrushPackageName] = false,
        [PostProcessingPackageName] = false,
        [SoundVisualizerPackageName] = false,
        [SoxAnimationPackageName] = false,
        [SpinePackageName] = false,
        [TimeLinePackageName] = false,
    };


    static ThirdPackagesManager()
    {
        CheckFolder();
        EditorApplication.projectChanged += EditorApplication_projectChanged;
    }

    static void CheckFolder()
    {
        string thirdComponentPath = Path.Combine(Application.dataPath, savePath);

        Dictionary<string, bool> change = new Dictionary<string, bool>();
        //检查目录是否有文件夹
        foreach (var item in TargetList)
        {
            string path = Path.Combine(thirdComponentPath, item.Key);
            bool exist = System.IO.Directory.Exists(path);
            //Debug.LogError(item.Key +"-exit:"+exist);
            change[item.Key] = exist;
        }
        foreach (var item in change)
        {
            TargetList[item.Key] = item.Value;
        }
    }

    private static void EditorApplication_projectChanged()
    {
        CheckFolder();
    }




    #region Bolt
    [MenuItem(BoltPath, true, 900)]
    static bool BoltCheck()
    {
        Menu.SetChecked(BoltPath, TargetList[BoltPackageName]);
        return true;
    }
    [MenuItem(BoltPath, false, 900)]
    static void Bolt()
    {
        bool flag = Menu.GetChecked(BoltPath);
        TargetList[BoltPackageName] = !TargetList[BoltPackageName];
        InstallOrDelete(BoltPackageName, TargetList[BoltPackageName]);
        Menu.SetChecked(BoltPath, TargetList[BoltPackageName]);
    }
    #endregion

    #region Fimpossible_Creations
    [MenuItem(FimpossibleCreationsPath, true, 901)]
    static bool Fimpossible_CreationsCheck()
    {
        Menu.SetChecked(FimpossibleCreationsPath, TargetList[FimpossibleCreationsPackageName]);
        return true;
    }
    [MenuItem(FimpossibleCreationsPath, false, 901)]
    static void Fimpossible_Creations()
    {
        bool flag = Menu.GetChecked(FimpossibleCreationsPath);
        TargetList[FimpossibleCreationsPackageName] = !TargetList[FimpossibleCreationsPackageName];
        InstallOrDelete(FimpossibleCreationsPackageName, TargetList[FimpossibleCreationsPackageName]);
        Menu.SetChecked(FimpossibleCreationsPath, TargetList[FimpossibleCreationsPackageName]);
    }
    #endregion

    #region FlockBox
    [MenuItem(FlockBoxPath, true, 902)]
    static bool FlockBoxCheck()
    {
        Menu.SetChecked(FlockBoxPath, TargetList[FlockBoxPackageName]);
        return true;
    }
    [MenuItem(FlockBoxPath, false, 902)]
    static void FlockBox()
    {
        bool flag = Menu.GetChecked(FlockBoxPath);
        TargetList[FlockBoxPackageName] = !TargetList[FlockBoxPackageName];
        InstallOrDelete(FlockBoxPackageName, TargetList[FlockBoxPackageName]);
        Menu.SetChecked(FlockBoxPath, TargetList[FlockBoxPackageName]);
    }
    #endregion

    #region PolyBrush
    [MenuItem(PolyBrushPath, true, 903)]
    static bool PolyBrushCheck()
    {
        Menu.SetChecked(PolyBrushPath, TargetList[PolyBrushPackageName]);
        return true;
    }
    [MenuItem(PolyBrushPath, false, 903)]
    static void PolyBrush()
    {
        bool flag = Menu.GetChecked(PolyBrushPath);
        TargetList[PolyBrushPackageName] = !TargetList[PolyBrushPackageName];
        InstallOrDelete(PolyBrushPackageName, TargetList[PolyBrushPackageName]);
        Menu.SetChecked(PolyBrushPath, TargetList[PolyBrushPackageName]);
    }
    #endregion

    #region PostProcessing
    [MenuItem(PostProcessingPath, true, 904)]
    static bool PostProcessingCheck()
    {
        Menu.SetChecked(PostProcessingPath, TargetList[PostProcessingPackageName]);
        return true;
    }
    [MenuItem(PostProcessingPath, false, 904)]
    static void PostProcessing()
    {
        bool flag = Menu.GetChecked(PostProcessingPath);
        TargetList[PostProcessingPackageName] = !TargetList[PostProcessingPackageName];
        InstallOrDelete(PostProcessingPackageName, TargetList[PostProcessingPackageName]);
        Menu.SetChecked(PostProcessingPath, TargetList[PostProcessingPackageName]);
    }
    #endregion

    #region SoundVisualizer
    [MenuItem(SoundVisualizerPath, true, 905)]
    static bool SoundVisualizerCheck()
    {
        Menu.SetChecked(SoundVisualizerPath, TargetList[SoundVisualizerPackageName]);
        return true;
    }
    [MenuItem(SoundVisualizerPath, false, 905)]
    static void SoundVisualizer()
    {
        bool flag = Menu.GetChecked(SoundVisualizerPath);
        TargetList[SoundVisualizerPackageName] = !TargetList[SoundVisualizerPackageName];
        InstallOrDelete(SoundVisualizerPackageName, TargetList[SoundVisualizerPackageName]);
        Menu.SetChecked(SoundVisualizerPath, TargetList[SoundVisualizerPackageName]);
    }
    #endregion

    #region SoxAnimation
    [MenuItem(SoxAnimationPath, true, 906)]
    static bool SoxAnimationCheck()
    {
        Menu.SetChecked(SoxAnimationPath, TargetList[SoxAnimationPackageName]);
        return true;
    }
    [MenuItem(SoxAnimationPath, false, 906)]
    static void SoxAnimation()
    {
        bool flag = Menu.GetChecked(SoxAnimationPath);
        TargetList[SoxAnimationPackageName] = !TargetList[SoxAnimationPackageName];
        InstallOrDelete(SoxAnimationPackageName, TargetList[SoxAnimationPackageName]);
        Menu.SetChecked(SoxAnimationPath, TargetList[SoxAnimationPackageName]);
    }
    #endregion

    #region Spine
    [MenuItem(SpinePath, true, 906)]
    static bool SpineCheck()
    {
        Menu.SetChecked(SpinePath, TargetList[SpinePackageName]);
        return true;
    }
    [MenuItem(SpinePath, false, 906)]
    static void Spine()
    {
        bool flag = Menu.GetChecked(SpinePath);
        TargetList[SpinePackageName] = !TargetList[SpinePackageName];
        InstallOrDelete(SpinePackageName, TargetList[SpinePackageName]);
        Menu.SetChecked(SpinePath, TargetList[SpinePackageName]);
    }
    #endregion

    #region TimeLine
    [MenuItem(TimeLinePath, true, 906)]
    static bool TimeLineCheck()
    {
        Menu.SetChecked(TimeLinePath, TargetList[TimeLinePackageName]);
        return true;
    }
    [MenuItem(TimeLinePath, false, 906)]
    static void TimeLine()
    {
        bool flag = Menu.GetChecked(TimeLinePath);
        TargetList[TimeLinePackageName] = !TargetList[TimeLinePackageName];
        InstallOrDelete(TimeLinePackageName, TargetList[TimeLinePackageName]);
        Menu.SetChecked(TimeLinePath, TargetList[TimeLinePackageName]);
    }
    #endregion

    #region Zip

    static void InstallOrDelete(string targetPackageName,bool isInstall)
    {
        string targetPath = Path.Combine(Application.dataPath, savePath, targetPackageName);

        if (Directory.Exists(targetPath)) Directory.Delete(targetPath,true);
        //删除
        if (!isInstall)
        {
            AssetDatabase.Refresh();
            return;
        }

        Directory.CreateDirectory(targetPath);
        string filePath = targetPath + ".zip";

        //解压
        //if (!Directory.Exists(filePath))
        //{
        //    Debug.LogError("未找到插件："+ targetPackageName+"，请联系平台客服");
        //    return;
        //}


        ARWorldEditor.ZipUtility.Unzip(filePath, targetPath);
        AssetDatabase.Refresh();

    }

    #endregion

}

#endif