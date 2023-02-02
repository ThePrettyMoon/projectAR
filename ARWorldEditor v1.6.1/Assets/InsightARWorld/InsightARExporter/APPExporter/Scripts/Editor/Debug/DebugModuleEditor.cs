using ARSession;
using ARWorldEditor;
using InsightARWorldEditor.Debug;
using UnityEditor;
using UnityEngine;

public class DebugModuleEditor:Editor
{

    public const string debugkey = "DebugModuleEditor";
    public const string openPath = "EZXR/打包时加入调试插件/打开";
    public const string closePath = "EZXR/打包时加入调试插件/关闭";
    public const string debugModuleName = "debugModule__";

    private static bool openToggle = false;
    private static bool closeToggle = false;


    private static MyContentsResultData myContent;
    static DebugModuleEditor()
    {
        //UnityEngine.Debug.Log("DebugModuleEditor");
        myContent = ExportSceneWindow.LoadMyContent();
        if (myContent != null)
        {
            openToggle = IsTargetCidSettingSaved(myContent.id);
            closeToggle = !openToggle;
            //UnityEngine.Debug.Log("isOPen:" + openToggle);
        }
        else
        {
            openToggle = false;
            closeToggle =false;
        }
        //UnityEngine.Debug.Log("myContent = null:" + (myContent == null));
    }
    //缓存
    public static bool IsTargetCidSettingSaved(long cid)
    {
        string key = debugkey + cid.ToString();
        return EditorPrefs.GetBool(key);
    }
    public static void SetTargetCidSetting(long cid,bool value)
    {
        string key = debugkey + cid.ToString();
        EditorPrefs.SetBool(key,value);
    }



    [MenuItem(openPath, true ,300)]
    static bool OpenDynamicDebug_Check()
    {
        Menu.SetChecked(openPath, openToggle);
        myContent = ExportSceneWindow.LoadMyContent();
        return UserController.UserLogin&&( myContent != null);
    }
    [MenuItem(closePath,true, 299)]
    static bool CloseDynamicDebug_Check()
    {
        Menu.SetChecked(closePath, closeToggle);
        myContent = ExportSceneWindow.LoadMyContent();
        return UserController.UserLogin && (myContent != null);
    }

    [MenuItem(openPath, false, 300)]
    static void OpenDynamicDebug()
    {
        openToggle = true;
        closeToggle = false;
        myContent = ExportSceneWindow.LoadMyContent();
        SetTargetCidSetting(myContent.id, true);
        AddDebugModule();
    }
    [MenuItem(closePath, false, 299)]
    static void CloseDynamicDebug()
    {
        openToggle = false;
        closeToggle = true;
        myContent = ExportSceneWindow.LoadMyContent();
        SetTargetCidSetting(myContent.id, false);
        CloseDebugModule();
    }

    public static void AddDebugModule()
    {
        var obj = GameObject.Find(debugModuleName);
        if (obj == null)
        {
            obj = new GameObject(debugModuleName);
            obj.hideFlags = HideFlags.HideInHierarchy;
            obj.AddComponent<InitRuntimeDebug>();
        }
        else
        {
            if (obj.GetComponent<InitRuntimeDebug>() == null)
            {
                obj.AddComponent<InitRuntimeDebug>();
            }
        }
    }

    public static void CloseDebugModule()
    {
        var obj = GameObject.Find(debugModuleName);
        if (obj != null)
        {
            DestroyImmediate(obj);
        }
    }
}
