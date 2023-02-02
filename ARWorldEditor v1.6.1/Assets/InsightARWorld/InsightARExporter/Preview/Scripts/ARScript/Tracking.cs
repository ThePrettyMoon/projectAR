using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InsightAR.Internal;

public class Tracking
{
    public static int quadCount
    {
        get
        {
            return 0;
        }
    }

    public static int reason
    {
        get
        {
            return (int)InsightARManager.Instance.ARReason;
        }
    }

    public static int status
    {
        get
        {
            return (int)InsightARManager.Instance.ARState;
        }
    }

    public static  int type
    {
        get
        {
            return 0;
        }
    }

    //   type    算法类型，type=0表示在线光照恢复算法，type=1表示离线光照恢复算法
            
    public static bool  EstimateIllumination(int  type)
    {
        return false;
    }

    public static Vector3 QuadGetCenter(string name)
    {
        List<InsightARMarkerAnchor> anchors = InsightARManager.Instance.GetInsightARMarkers();
        if (anchors == null || anchors.Count == 0) return Vector3.zero ;
        for(int i= 0; i < anchors.Count; i++)
        {
            if(name == anchors[i].identifier)
            {
                return anchors[i].center;
            }
        }
        return Vector3.zero;
    }

    /// <summary>
    /// 获得名称 
    /// </summary>
    /// <returns>The get name.</returns>
    /// <param name="index">Index.</param>
    public static string QuadGetName(int index)
    {
        List<InsightARMarkerAnchor> anchors = InsightARManager.Instance.GetInsightARMarkers();
        if (anchors == null || anchors.Count == 0) return string.Empty;
        for (int i = 0; i < anchors.Count; i++)
        {
            if (index == i)
            {
                return anchors[i].identifier;
            }
        }
        return string.Empty;
    }

    public static Quaternion QuadGetRotation(string name)
    {
        List<InsightARMarkerAnchor> anchors = InsightARManager.Instance.GetInsightARMarkers();
        if (anchors == null || anchors.Count == 0) return Quaternion.identity;
        for (int i = 0; i < anchors.Count; i++)
        {
            if (name == anchors[i].identifier)
            {
                return anchors[i].rotation;
            }
        }
        return Quaternion.identity;
    }

    public static Vector3 QuadGetScale(string name)
    {
        List<InsightARMarkerAnchor> anchors = InsightARManager.Instance.GetInsightARMarkers();
        if (anchors == null || anchors.Count == 0) return Vector3.zero;
        for (int i = 0; i < anchors.Count; i++)
        {
            if (name == anchors[i].identifier)
            {
                return anchors[i].extent;
            }
        }
        return Vector3.zero;
    }

    public static bool QuadGetValid(string name)
    {
        List<InsightARMarkerAnchor> anchors = InsightARManager.Instance.GetInsightARMarkers();
        if (anchors == null || anchors.Count == 0) return false;
        for (int i = 0; i < anchors.Count; i++)
        {
            if (name == anchors[i].identifier )
            {
                return anchors[i].isValid == 1;
            }
        }
        return false;
    }

    /// <summary>
    /// int x ,int y 单位是像素 
    /// </summary>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    public   static TrackingResult  Raycasting(int  x, int   y)
    { 
        TrackingResult trackResult = new TrackingResult(Vector3.zero,false);
        return trackResult;
    }

    // face
    public static string faceResultJsonString
    {
        get
        {
            return string.Empty;
        }
    }

    //gesture
    public static string gestureResultJsonString
    {
        get
        {
            return string.Empty;
        }
    }

    //body
    public static string bodyResultJsonString
    {
        get
        {
            return string.Empty;
        }
    }

 
    //返回body mask 纹理
    public static Texture2D bodyMaskTexture
    {
        get
        {
            return null;
        }
    }

    /// <summary>
    /// 返回识别结果
    /// </summary>
    /// <param name="trackIndex"></param>
    /// <returns></returns>
    public static string GetResultString(int trackIndex)
    {
        return InsightARManager.Instance.GetResultString(trackIndex);
    }

    public static string GetTrackingResult(int type)
    {
        return "";
    }

    //cloud location
    public static int cloudLocationStatus {
        get
        {
            return (int)InsightARManager.Instance.CloudLocStatus;
        }

    }
    public static string cloudLocationReason {

        get
        {
            return InsightARManager.Instance.CloudLocReason;
        }
    }

    public static long cloudLocationTotalCount
    {

        get
        {
            return InsightARManager.Instance.CloudLocTotalCount;
        }
    }

    public static void RequestOnceLocNative() {
        InsightARNative.iarRequestOnceCloudLocNative();
        InsightDebug.Log("Tracking", "RequestOnceCloudLocNative");
    }
}

