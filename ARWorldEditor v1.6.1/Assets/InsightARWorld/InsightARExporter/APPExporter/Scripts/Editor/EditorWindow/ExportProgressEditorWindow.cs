#if UNITY_EDITOR
using ARWorldEditor;
using System.Text;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

[InitializeOnLoad]
public class ExportProgressEditorWindow : EditorWindow
{
    #region params
    static ExportProgressEditorWindow _instance;
    private GUISkin skin;
    public static float Progress { get; set; }
    public static string CurrentProgressActionInfo { get; set; }
    public static StringBuilder LogInfo { get; set; }
    [TextArea(3, 10)]
    private static string saveLogInfo = "";
    public static string SaveLogInfo 
    {
        get {
            string value = "";
            if (EditorPrefs.HasKey("ExportProgressEditorWindow-SaveLogInfo"))
            {
                value = EditorPrefs.GetString("ExportProgressEditorWindow-SaveLogInfo");
            }
            saveLogInfo = value;
            return value;
        }
        set
        {
            EditorPrefs.SetString("ExportProgressEditorWindow-SaveLogInfo", value);
            saveLogInfo = value;
        }
    }

    public const string ContentStartPublish = "提示：场景打包开始...";//0f
    public const string ContentBuildFinish = "提示：场景打包完毕...";//0.7f
    public const string ContentReadyToUpload = "提示:正在上传内容，请稍等...";//0.7f;
    public const string ContentFinishUpload = "提示：完成内容上传...";//0.9f
    public const string ContentPublishSuccess = "提示：内容发布成功...";//1f
    public const string ContentPublishFail = "Error：内容发布失败,";//1f
    public const string ContentTurnToFolder = "提示：跳转打包文件夹...";//1f
    public const string SimGlassBuildStart = "SimGlass Start Build";
    public const string SimGlassFinishBuild = "SimGlass Finish Build";
    public const string AndroidBuildStart = "Android Start Build";
    public const string AndroidFinishBuild = "Android Finish Build";
    public const string IOSBuildStart = "IOS Start Build";
    public const string IOSFinishBuild = "IOS Finish Build";

    public const string Error_PackageIsNull = "提示：打包资源为空...";//1f
    public const string Error_SceneZipOver100MB = "Error：scene zip file can't exceed 100mb...";
    public const string Error_NeedSaveScene = "提示：打包前请先保存场景，保存后重新打包...";//0f
   
    #endregion

    // Start is called before the first frame update
    private void OnGUI()
    {
        DrawProgressPanel();
        _instance.position = new Rect(0, 0, 500, 500);
    }

    //[MenuItem("EZXR/进度 &q", false, 10000)]
    public static void ShowWindow()
    {
        if (_instance == null)
        {
            _instance = (ExportProgressEditorWindow)EditorWindow.GetWindow(typeof(ExportProgressEditorWindow), true, "Upload Progress");
            _instance.minSize = new Vector2(500f,500f);
            _instance.maxSize = new Vector2(500f,500f);
            //_instance.position = new Rect(0, 0, 500, 500);
        }
        LogInfo = new StringBuilder();
        Progress = 0f;
        CurrentProgressActionInfo = "";
        _instance.Show();
    }

    private void DrawProgressPanel()
    {
        EditorGUI.LabelField(new Rect(10,30, 55, 50),"发布进度：");
        EditorGUI.ProgressBar( new Rect(80, 30,400,50), Progress, CurrentProgressActionInfo);
        EditorGUI.LabelField(new Rect(10, 100, 55, 50), "进度详情：");
        EditorGUI.TextArea(new Rect(80, 100, 400, 300), saveLogInfo.ToString());
        if (GUI.Button(new Rect(80, 430, 400, 40),"关闭"))
        {
            Close();
        }
    }

    public static void SetCurrentProgressState(string currentProgressAction,float targetProgress)
    {
        if (_instance == null)
        {
            ExportProgressEditorWindow.ShowWindow();
        }
        LogInfo.Append("\n");
        string logInfo = currentProgressAction.ToString();
        LogInfo.Append(logInfo);
        SaveLogInfo = LogInfo.ToString();
        Progress = targetProgress;
        _instance.Focus();
        _instance.Repaint();
        _instance.Show();
    }
    public static void SetCurrentProgressState(BuildReport report, float targetProgress)
    {
        if (_instance == null)
        {
            ExportProgressEditorWindow.ShowWindow();
        }
        foreach (var item in report.steps)
        {
            foreach (var info in item.messages)
            {
                if (info.type == LogType.Error)
                {
                    string logType = info.type.ToString();
                    string content = info.content;
                    LogInfo.Append("\n");
                    LogInfo.Append(logType + ":\t" + content);
                }
            }
        }
        SaveLogInfo = LogInfo.ToString();
        Progress = targetProgress;
        _instance.Focus();
        _instance.Repaint();
        _instance.Show();
    }
    public static void CloseWindow()
    {
        _instance.Close();
    }
}
#endif