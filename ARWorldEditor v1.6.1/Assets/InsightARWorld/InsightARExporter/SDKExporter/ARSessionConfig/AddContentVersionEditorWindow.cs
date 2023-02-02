#if UNITY_EDITOR
using ARWorldEditor;
using Insight;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.VR;

public class AddContentVersionEditorWindow : EditorWindow
{
    #region params
    static AddContentVersionEditorWindow _instance;
    GUIStyle scenePopUpStyle;
    int selectIndex = 0;
    long contentID = 0;
    int engineType = 0;
    int deviceType = 1;
    Dictionary<string, GetAppSdkVersionsResonseResult> versionToSDKVersionDic = new Dictionary<string, GetAppSdkVersionsResonseResult>();
    List<string> versionList = new List<string>();
    #endregion
    [MenuItem("EZXR/新增版本 &e", false, 10000)]
    public static void ShowWindow(GetAppSdkVersionsResponseData data,long _contentID, int _engineType,int _deviceType)
    {
        if (_instance == null)
        {
            _instance = (AddContentVersionEditorWindow)EditorWindow.GetWindow(typeof(AddContentVersionEditorWindow), true, "添加版本");
            _instance.minSize = new UnityEngine.Vector2(500f, 200f);
            _instance.maxSize = new UnityEngine.Vector2(500f, 200f);
        }

        _instance.engineType = _engineType;
        _instance.contentID = _contentID;
        _instance.deviceType = _deviceType;
        _instance.versionToSDKVersionDic.Clear();
        _instance.versionList.Clear();
        foreach (var item in data.result)
        {
            _instance.versionToSDKVersionDic.Add(item.version, item);
            _instance.versionList.Add(item.version);
        }
        _instance.Show();
    }

    private void OnGUI()
    {
        if (scenePopUpStyle == null)
        {
            scenePopUpStyle = new GUIStyle(EditorStyles.popup);
            scenePopUpStyle.alignment = TextAnchor.MiddleLeft;
            scenePopUpStyle.fontSize = 14;
            scenePopUpStyle.fixedHeight = 60;
        }

        DrawVersionPanel();
    }
    private void DrawVersionPanel()
    {
        EditorGUILayout.BeginVertical();
        EditorGUILayout.Space(30);
        EditorGUILayout.PrefixLabel("请选择要添加的SDK版本号");
        selectIndex = EditorGUILayout.Popup(selectIndex, versionList.ToArray(), scenePopUpStyle, GUILayout.Height(scenePopUpStyle.fixedHeight));
        EditorGUILayout.EndVertical();
        EditorGUILayout.Space(30);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("取消", GUILayout.Height(40)))
        {
            Cancel();
        }
        if (GUILayout.Button("确定", GUILayout.Height(40)))
        {
            Confirm();
        }
        EditorGUILayout.EndHorizontal();

        Event e = Event.current;
        if (e.isKey)
        {
            Debug.LogError(e.type);
            if (e.type == EventType.KeyDown)
            {

                if (e.keyCode == KeyCode.Return || e.keyCode == KeyCode.Space)
                {
                    Confirm();
                }
            }
        }
    }
    private void Cancel()
    {
        Debug.Log("Cancel");
        Close();
    }
    private void Confirm()
    {
        Debug.Log("Confirm");
        CreateAppVersion();
    }
    private void CreateAppVersion()
    {
        var data = versionToSDKVersionDic[versionList[selectIndex]];
        ARWorldEditor.NetDataFetchManager.Instance.CreateContentVersion(contentID, engineType, data.sdkVersionId,deviceType, 
            new ARWorldEditor.OnOasisNetworkDataFetchCallback<ARWorldEditor.CreateContentVersionResponseData>(
                (ARWorldEditor.CreateContentVersionResponseData response) =>
                {
                    OnCreateContentVersionSuccess(response);
                },
                (string code, string msg) =>
                {
                    OnCreateContentVersionFail(code, msg);
                }));
    }
    private void OnCreateContentVersionSuccess(CreateContentVersionResponseData result )
    {
        Close();
        ARSceneCreator.ShowWindow();
    }
    private void OnCreateContentVersionFail(string code,string msg)
    {
        EditorUtility.DisplayDialog("Error", "创建内容失败。\n code = " + code + "\n msg = " + msg, "确认");
    }

}

#endif