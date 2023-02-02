using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


/// <summary>
/// 初始 以及 主动点击 时进行重置操作
/// 1.packagemanager
/// 2.Layer
/// </summary>

public class ListenerManager
{
    private static bool Initialized
    {
        get
        {
            if (string.IsNullOrEmpty(EditorUserSettings.GetConfigValue("ListenerManager")))
            {
                EditorUserSettings.SetConfigValue("ListenerManager", "finish");
                //Debug.LogError("add");
                return false;
            }
            else
            {

                return true;
            }
          
        }
    }
    
    [UnityEditor.Callbacks.DidReloadScripts]
    static void InitializeOnLoad()
    {
        if (Initialized)
            return;
        Initialize();
    }
    //[MenuItem("EZXR/重置", false, 2000)]
    static void Initialize()
    {
        LayerChangeListener.SetLayerAndTag();
    }
}
