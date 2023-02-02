using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InsightAR.Internal;

/// <summary>
/// lua 和unity通信事件
/// </summary>
public class GlobalEvent 
{
    private const string TAG = "GlobalEvent";
    /// <summary>
    ///  手机震动
    /// </summary>
    public static void Vibrate()
    {
        InsightDebug.Log(TAG, "Vibrate " );
        InsightAPPNative.Vibrate();
    }

    /// <summary>
    /// 是否打开音视频界面
    /// </summary>
    /// <param name="visible"></param>
    public static void StartCaptureUIVisible(int visible)
    {
        InsightDebug.Log(TAG, "Start Capture UI Visible " + visible);
        InsightAPPNative.SetCaputureUIVisible(visible);
    }

    /// <summary>
    /// 切换到3dof算法
    /// </summary>
    public static void Switch3Dof()
    {
        InsightDebug.Log(TAG, "Switch3Dof");
        //仅iOS 支持切到3dof模式
#if UNITY_IOS
        InsightARManager.Instance.GetARAttach().StartAttachAR("3dofnoVR","");
#endif
    }

}
