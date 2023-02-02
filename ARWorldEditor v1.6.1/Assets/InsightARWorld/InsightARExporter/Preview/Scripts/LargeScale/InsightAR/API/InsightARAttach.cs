using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InsightAR.Internal;

public enum InsightAttachType
{
    ATTACH_TYPE_NONE = 0,
    ATTACH_TYPE_NO_CHANGE= 1, //没有变化，采用默认算法
    ATTACH_TYPE_ADD = 2,  //算法叠加，只有anchor 或者recognition 等回调
    ATTACH_TYPE_CHANGE=3, //原算法被挂起，切换到新的算法
}

/// <summary>
/// 处理算法叠加
/// </summary>
public class InsightARAttach
{
    private const string TAG = "InsightARAttach";

    /// <summary>
    /// 开始叠加ar算法
    /// </summary>
    /// <param name="configPath"></param>
    /// <param name="assetPath"></param>
    /// <returns></returns>
    public InsightAttachType StartAttachAR(string configPath, string mapAssetPath)
    {
        return (InsightAttachType)InsightARNative.iarlsStartAttachedAR(configPath, mapAssetPath);
    }

    /// <summary>
    /// 停止叠加ar
    /// </summary>
    public void StopAttachAR()
    {
        InsightARNative.iarlsStopAttachedAR();
    }
}
