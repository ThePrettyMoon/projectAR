#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using System.Threading;

public class DuktapePreview
{    
    // Scene加载完成之前，同步初始化Duktape
    [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void OnRuntimeInitializeBeforeSceneLoad()
    {
        DukTapeVMManager.Instance.Startup();
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    // Scene加载完成之后，初始场景内Javascript
    [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void OnRuntimeInitializeAfterSceneLoad()
    {
        ParseJavaScript();
    }

    // 退出Play时，ShutDown。
    private static void OnPlayModeStateChanged(PlayModeStateChange mode)
    {
        //ExitingPlayMode
        if (mode == PlayModeStateChange.ExitingPlayMode)
        {
            DukTapeVMManager.Instance.ShutDown();
        }
    }

    private  static void ParseJavaScript()
    {
        var list = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
        GameObject obj = null;
        //找到一个字节点最少的根节点
        foreach (var item in list)
        {
            if (item.transform.childCount==0)
            {
                obj = item;
                break;
            }
        }
        if (obj == null)
        {
            obj = list[0];
        }
        ARWorldEditor.SceneHierarchyUtility.SetExpandedRecursive(obj, true);

        //获取当前场景所有根节点
        foreach (GameObject rootObj in list)
        {
            ScriptRunner[] scriptRunners = rootObj.GetComponentsInChildren<ScriptRunner>(true);
         
            if (scriptRunners != null && scriptRunners.Length > 0)
            {
                for (int i = 0; i < scriptRunners.Length; i++)
                {
                    var sc = scriptRunners[i];
                    string root = Application.dataPath.Replace("/Assets", "");
                    string relativePath = AssetDatabase.GetAssetPath(sc.ScriptSource);
                    //Debug.LogError("root:"+ root + " relativePath:"+ relativePath);
                    sc.ParseScript(root, relativePath);
                }
            }
        }
        //Debug.Log("ParseJavaScript ok");
    }
}
#endif