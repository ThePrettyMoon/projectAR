using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

/// <summary>
/// 地图上点的描述信息
/// </summary>
public struct MapPoint
{
    /// <summary>
    /// 楼层
    /// </summary>
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
    public string floorLevel;

    /// <summary>
    /// 经纬度,[0]经度,[1]纬度
    /// </summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public double[] geographicCoords;
    /// <summary>
    /// 当前点的三维空间坐标
    /// </summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public float[] realSpaceCoords;
    /// <summary>
    /// 2D地图上的朝向，yaw角
    /// </summary>
    public float direction2DYaw;
    /// <summary>
    /// 三维空间中朝向，四元数
    /// </summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public float[] rotation;
}

/// <summary>
/// 地图上线描述信息
/// </summary>
public struct MapLineString
{
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
    public MapPoint mapPoints;
}

/// <summary>
/// 地图上多边形的描述信息
/// </summary>
public struct MapPolygon
{
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
    public MapLineString lineStringPoints;
}


/// <summary>
/// 地图信息
/// </summary>
public struct MapInfo
{
    /// <summary>
    /// 对应的线上的map的链接地址
    /// </summary>
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
    public string mapboxUrl;
    /// <summary>
    /// 标记符
    /// </summary>
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
    public string identitifier;

}

/// <summary>
/// 地图POI节点描述
/// </summary>
public struct MapPoIInfo
{
    /// <summary>
    /// 地图Id
    /// </summary>
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
    public string identifierMap;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
    public string osmIdentifier;

    [JsonProperty(PropertyName = "mapPoint")]
    public MapPoint pointInfo;

    public MapPoiProperty properties;
}

/// <summary>
/// 和算法保持一致
/// </summary>
public class MapPoiInfos
{
    public int poiSum;
    public List<MapPoIInfo> mapPoILists;
}

/// <summary>
/// 属性
/// </summary>
public struct MapPoiProperty
{
    public string direction;

    public string x_anchor;

    public string x_content_type;

    public string type;
    public string height;
    public string level;
    public string name;

    public string x_name_radius;

    public string x_content_id;

    public string x_popular;

    public string x_content_radius;

    public string x_preview_content_id;

    public string x_preview_content_radius;

    public string x_preview_content_type;

    public string x_content_alg_mode;

    public string id;
}

///导航动作
public enum TurnAction
{
    /// 经纬度,[0]纬度,[1]经度
    UNKNOW = 0,
    /// 直行
    STRAIGHT = 1,
    /// 左转
    LEFT = 2,
    /// 右转
    RIGHT = 3,
    /// 上楼梯
    UPSTAIRS = 4,
    /// 下楼梯
    DOWNSTAIRS = 5,
    /// 达到目的
    REACH = 6
}

/// <summary>
/// 导航提示
/// </summary>
public struct TurnInfo
{
    /// 转弯动作
    public TurnAction turnAction;
    //转弯角度（弧度）
    public float angle;
    /// 动作距离
    public float distance;
}

public struct SplitPath
{
    /// 路径点的数量
    public int mapPointsSum;
    /// 路径点
    public List<MapPoint> mapPoints;
}

/// <summary>
/// 单楼层路径
/// </summary>
public struct FloorPath
{
    public string floorLevel;
    public int splitSum;
    public List<SplitPath> splitPaths;
}

public class MapNavigationPath
{
    public string identifierBuilding;
    public List<FloorPath> floorPath;
}

public class MapNavigationTurnInfo
{
    //turn action
    public int turnAction;
    public float angle;
    public float distance;
}

public class MapNavigationInfo
{
    public MapNavigationTurnInfo turnInfoJsonBuf;
    public MapNavigationPath navPathJsonBuf;
}






