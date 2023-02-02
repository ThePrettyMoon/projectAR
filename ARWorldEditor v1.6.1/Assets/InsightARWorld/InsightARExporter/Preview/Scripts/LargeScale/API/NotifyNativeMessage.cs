using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

/// <summary>
/// 处理通知native消息
/// </summary>
public class NotifyNativeMessage
{
    #region params
    private const string TAG = "NotifyNativeMessage";
    #endregion

    #region custom functions

    /// <summary>
    /// load map data
    /// </summary>
    /// <param name="floorIndex"></param>
    public static void LoadGeoMapData()
    {

#if UNITY_IOS
        GeoLevelsData geoLevelsData = InsightConfigManager.Instance.GetGeoLevelsData();
        GeoData geoJsonData = new GeoData{
            geoFilePath = ContentResPaths.Instance.GetGeoRoot(),
            geoFileContent = geoLevelsData
        };
        string geoData = JsonUtil.Serialize(geoJsonData);
        InsightDebug.Log("LOADMAP", geoData);
        LoadMapData(geoData);
#else
        GeoLevelsData levelsIndex = InsightConfigManager.Instance.GetGeoLevelsData();
        List<GeoData> allGeoData = new List<GeoData>();
        for (int i = 0; i < levelsIndex.levels.Count; i++)
        {
            GeoData geoJsonData = new GeoData
            {
                floor = levelsIndex.levels[i].refFloor,
                floorName = levelsIndex.levels[i].name,
                geoFilePath = levelsIndex.levels[i].filePath
            };
            allGeoData.Add(geoJsonData);
        }
        string geoData = JsonUtil.Serialize(allGeoData);
        InsightDebug.Log("LOADMAP", geoData);
        LoadMapData(geoData);
#endif


    }

    /// <summary>
    /// load map data
    /// </summary>
    /// <param name="data"></param>
    /// <param name="floor"></param>
    public static void LoadMapData(string data,int floor = 1)
    {
        InsightAPPNative.LoadMapData(data, floor);
    }

    /// <summary>
    /// 设置poilist
    /// </summary>
    public static void SetPoiList(string jsonStr)
    {
        InsightAPPNative.SetPoiList(jsonStr);
    }

    /// <summary>
    /// 是否显示地图
    /// </summary>
    /// <param name="visible"></param>
    public static void SetMapVisibility(int visible)
    {
        InsightAPPNative.SetMapUIVisibility(visible);
    }

    /// <summary>
    /// make toast
    /// </summary>
    /// <param name="text"></param>
    /// <param name="during"></param>
    public static void MakeToast(string text,int during = 3)
    {
        InsightDebug.Log(TAG, "make toast " + text);
        InsightAPPNative.MakeToast(text, during);
    }

#endregion


}
