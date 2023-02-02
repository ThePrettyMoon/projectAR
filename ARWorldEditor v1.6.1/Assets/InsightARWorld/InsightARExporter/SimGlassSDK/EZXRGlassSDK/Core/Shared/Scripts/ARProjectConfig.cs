using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "ARProjectConfig", menuName = "ARProjectConfig", order = 2)]
public class ARProjectConfig : ScriptableObject
{
    /// <summary>
    /// 空间追踪
    /// </summary>
    public bool spatialTracking;
    /// <summary>
    /// 多人空间追踪（云锚点）
    /// </summary>
    public bool spatialTrackingMultiPlayer;
    /// <summary>
    /// 空间定位
    /// </summary>
    public bool spatialPositioning;
    /// <summary>
    /// 手部追踪
    /// </summary>
    public bool handTracking;
    /// <summary>
    /// 平面检测
    /// </summary>
    public bool planeDetection;
    /// <summary>
    /// 图像检测
    /// </summary>
    public bool imageDetection;
    ///// <summary>
    ///// 物体检测
    ///// </summary>
    //public bool objectDetection;
    /// <summary>
    /// 空间Mesh
    /// </summary>
    public bool spatialMesh;
    /// <summary>
    /// 投屏
    /// </summary>
    public bool miraCast;
}
