using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "ARSDKConfig", menuName = "ARSDKConfig", order = 1)]
public class ARSDKConfig : ScriptableObject
{
    /// <summary>
    /// C/S架构
    /// </summary>
    public bool clientServerMode;
    /// <summary>
    /// 当前眼镜平台
    /// </summary>
    public string curPlatform;
    /// <summary>
    /// SDK version
    /// </summary>
    public string version;
    /// <summary>
    /// 空间追踪
    /// </summary>
    public bool spatialTracking;
    /// <summary>
    /// 空间定位
    /// </summary>
    public bool spatialPositioning;
    /// <summary>
    /// 手部追踪
    /// </summary>
    public bool handTracking;
    /// <summary>
    /// 图像检测
    /// </summary>
    public bool imageDetection;
    /// <summary>
    /// 物体检测
    /// </summary>
    public bool objectDetection;
}
