using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InsightAR.Internal;
using Dongjian.LargeScale;


public class NavigationUtility
{
    #region params
    private const float MINJUDGEDISTANCE = 2.0f;
   private const float MINTURNDEGREE = 60.0f;
    #endregion

    #region custom functions

    /// <summary>
    /// 从一系列点返回分割的点
    /// </summary>
    /// <param name="pointInfos"></param>
    /// <param name="distance">间隔</param>
    /// <param name="adjustHeight">调低高度</param>
    /// <returns></returns>
    public static List<Vector3> GetPositionsFromList(List<U3DMapPoint> pointInfos, float distance = 2.0f, float adjustHeight = -1.0f)
    {
        List<Vector3> points = new List<Vector3>();
        Vector3 startPosition = pointInfos[0].Position;
        for (int i = 1; i < pointInfos.Count; i++)
        {
            Vector3 endPosition = pointInfos[i].Position;
            while (Vector3.Distance(startPosition, endPosition) > distance)
            {
                Vector3 direction = (endPosition - startPosition).normalized;
                Vector3 worldPosition = startPosition + distance * direction + Vector3.up * adjustHeight;
                points.Add(worldPosition);
                startPosition = worldPosition;
            }
        }
        return points;
    }

    /// <summary>
    /// 创建POI假数据
    /// </summary>
    /// <returns></returns>
    public static string CreateMapPoinInfoData()
    {
        List<MapPoIInfo> poiInfos = new List<MapPoIInfo>();
        MapPoIInfo mapPoIInfo = new MapPoIInfo();
        mapPoIInfo.osmIdentifier = "-100";
        mapPoIInfo.identifierMap = "22";
        MapPoiProperty mapPoiProperty = new MapPoiProperty();
        mapPoiProperty.id = "7a8e8b149de4bccb1855e0f11c7fc3d1";
        mapPoiProperty.x_name_radius = "5";
        mapPoiProperty.x_preview_content_radius = "3";
        mapPoiProperty.x_content_radius = "2";
        mapPoiProperty.type = "POI";
        mapPoiProperty.name = "2D default 算法";
        mapPoiProperty.x_content_alg_mode = "unchange";
        mapPoiProperty.x_content_id = "3721";
        mapPoIInfo.properties = mapPoiProperty;
        
        mapPoIInfo.pointInfo = new MapPoint();
        mapPoIInfo.pointInfo.geographicCoords = new double[2] { 120.0, 30.0 };
        mapPoIInfo.pointInfo.realSpaceCoords = new float[3] { 10, 0, 10.0f };
        mapPoIInfo.pointInfo.rotation = new float[4] { 0, 0, 0, 1 };
        mapPoIInfo.pointInfo.floorLevel = "1";
        poiInfos.Add(mapPoIInfo);
        MapPoIInfo mapPoIInfo1 = new MapPoIInfo();
        mapPoIInfo1.osmIdentifier = "-101";
        mapPoIInfo1.identifierMap = "23";

        MapPoiProperty mapPoiProperty1 = new MapPoiProperty();
        mapPoiProperty1.id = "3180d070426dda3ca01909722a9a49d3";
        mapPoiProperty1.x_name_radius = "5";
        mapPoiProperty1.x_preview_content_radius = "3";
        mapPoiProperty1.x_content_radius = "2";
        mapPoiProperty1.type = "POI";
        mapPoiProperty1.name = "2D 识别 算法";
        mapPoiProperty1.x_content_alg_mode = "overlay";
        mapPoiProperty1.x_content_id = "3723";
        mapPoIInfo1.properties = mapPoiProperty1;
        mapPoIInfo1.pointInfo = new MapPoint();
        mapPoIInfo1.pointInfo.geographicCoords = new double[2] { 120.0, 30.0 };
        mapPoIInfo1.pointInfo.realSpaceCoords = new float[3] { 10.0f,0.0f, 10.0f };
        mapPoIInfo1.pointInfo.rotation = new float[4] { 0, 0, 0, 1 };
        mapPoIInfo1.pointInfo.floorLevel = "1";
        poiInfos.Add(mapPoIInfo1);
        MapPoIInfo mapPoIInfo2 = new MapPoIInfo();
        mapPoIInfo2.osmIdentifier = "-102";
        mapPoIInfo2.identifierMap = "24";
        MapPoiProperty mapPoiProperty2 = new MapPoiProperty();
        mapPoiProperty2.id = "352550754ff9a808858e2a174649aa61";
        mapPoiProperty2.x_name_radius = "5";
        mapPoiProperty2.x_preview_content_radius = "3";
        mapPoiProperty2.x_content_radius = "2";
        mapPoiProperty2.type = "POI";
        mapPoiProperty2.name = "2D跟踪算法";
        mapPoiProperty2.x_content_alg_mode = "swap";
        mapPoiProperty2.x_content_id = "3722";
        mapPoIInfo2.properties = mapPoiProperty2;
        mapPoIInfo2.pointInfo = new MapPoint();
        mapPoIInfo2.pointInfo.geographicCoords = new double[2] { 120.0, 30.0 };
        mapPoIInfo2.pointInfo.realSpaceCoords = new float[3] { 0.0f, 0.0f, 0.0f };
        mapPoIInfo2.pointInfo.rotation = new float[4] { 0, 0, 0, 1 };
        mapPoIInfo2.pointInfo.floorLevel = "1";
        poiInfos.Add(mapPoIInfo2);

        MapPoIInfo mapPoIInfo3 = new MapPoIInfo();
        mapPoIInfo3.osmIdentifier = "-103";
        mapPoIInfo3.identifierMap = "25";
        MapPoiProperty mapPoiProperty3 = new MapPoiProperty();
        mapPoiProperty3.id = "0af0058fcc558f1f2e6b3df391d97936";
        mapPoiProperty3.x_name_radius = "5";
        mapPoiProperty3.x_preview_content_radius = "3";
        mapPoiProperty3.x_content_radius = "2";
        mapPoiProperty3.type = "POI";
        mapPoiProperty3.name = "照片墙";
        mapPoiProperty3.x_content_alg_mode = "unsupport";
        mapPoiProperty3.x_content_id = "3722";
        mapPoIInfo3.properties = mapPoiProperty3;
        mapPoIInfo3.pointInfo = new MapPoint();
        mapPoIInfo3.pointInfo.geographicCoords = new double[2] { 120.0, 30.0 };
        mapPoIInfo3.pointInfo.realSpaceCoords = new float[3] { 10.0f, 0, 10.35f };
        mapPoIInfo3.pointInfo.rotation = new float[4] { 0, 0, 0, 1 };
        mapPoIInfo3.pointInfo.floorLevel = "1";
        poiInfos.Add(mapPoIInfo3);

        MapPoIInfo mapPoIInfo6 = new MapPoIInfo();
        mapPoIInfo6.osmIdentifier = "-104";
        mapPoIInfo6.identifierMap = "25";
        MapPoiProperty mapPoiProperty6 = new MapPoiProperty();
        mapPoiProperty6.id = "8c277030e1681d2e0b208457b8248cf3";
        mapPoiProperty6.direction = "86.4";
        mapPoiProperty6.x_name_radius = "10";
        mapPoiProperty6.type = "POI";
        mapPoiProperty6.name = "人体体验区域";
        mapPoiProperty6.x_name_radius = "10";
        mapPoiProperty6.x_content_radius = "3";
        mapPoiProperty6.x_preview_content_radius = "5";
        mapPoiProperty6.x_preview_content_id = "12";
        mapPoiProperty6.x_content_type = "body";
        mapPoiProperty6.x_preview_content_type = "ar_product";
        mapPoiProperty6.x_anchor = "yes";
        mapPoiProperty6.height = "1.88";
        mapPoIInfo6.properties = mapPoiProperty6;

        mapPoIInfo6.pointInfo = new MapPoint();
        mapPoIInfo6.pointInfo.geographicCoords = new double[2] { 120.0, 30.0 };
        mapPoIInfo6.pointInfo.realSpaceCoords = new float[3] { 10.0f, 0.5f, 5.0f };
        mapPoIInfo6.pointInfo.rotation = new float[4] { 0, 0, 0, 1 };
        mapPoIInfo6.pointInfo.floorLevel = "1";
        poiInfos.Add(mapPoIInfo6);

        MapPoiInfos mapPoiInfos = new MapPoiInfos();
        mapPoiInfos.mapPoILists = poiInfos;
        mapPoiInfos.poiSum = poiInfos.Count;
        return JsonUtil.Serialize(mapPoiInfos);
    }

    /// <summary>
    /// 创建导航数据
    /// </summary>
    public static string CreateNavPathData()
    {
        MapNavigationTurnInfo mapTurnInfo = new MapNavigationTurnInfo();
        mapTurnInfo.angle = 1000;
        mapTurnInfo.distance = 1.5f;
        mapTurnInfo.turnAction =Random.Range(1,6);

        List<MapPoint> list = new List<MapPoint>();
        MapPoint point = new MapPoint();
        point.floorLevel = "1";
        point.geographicCoords = new double[2] { 120.0, 30.0 };
        point.realSpaceCoords = new float[3] { 0, 0, 0 };
        point.rotation = new float[4] { 0, 0, 0, 1 };
        list.Add(point);
        MapPoint point1 = new MapPoint();
        point1.floorLevel = "1";
        point1.geographicCoords = new double[2] { 120.0, 30.0 };
        point1.realSpaceCoords = new float[3] { Random.Range(0, 20), 0, Random.Range(-5, 5) };
        point1.rotation = new float[4] { 0, 0, 0, 1 };
        list.Add(point1);

        SplitPath splitPath = new SplitPath();
        splitPath.mapPoints = list;
        splitPath.mapPointsSum = list.Count;

        List<MapPoint> list1 = new List<MapPoint>();
        MapPoint point2 = new MapPoint();
        point2.floorLevel = "1";
        point2.geographicCoords = new double[2] { 120.0, 30.0 };
        point2.realSpaceCoords = new float[3] { 10, 0, -20 };
        point2.rotation = new float[4] { 0, 0, 0, 1 };
        list1.Add(point2);
        MapPoint point3 = new MapPoint();
        point3.floorLevel = "1";
        point3.geographicCoords = new double[2] { 120.0, 30.0 };
        point3.realSpaceCoords = new float[3] { -5, 0, 5 };
        point3.rotation = new float[4] { 0, 0, 0, 1 };
        list1.Add(point3);


        SplitPath splitPath1 = new SplitPath();
        splitPath1.mapPoints = list1;
        splitPath1.mapPointsSum = list1.Count;

        List<MapPoint> list2 = new List<MapPoint>();
        MapPoint point4 = new MapPoint();
        point4.floorLevel = "1";
        point4.geographicCoords = new double[2] { 120.0, 30.0 };
        point4.realSpaceCoords = new float[3] { Random.Range(-10, 10), 0, Random.Range(-5, 5) };
        point4.rotation = new float[4] { 0, 0, 0, 1 };
        list2.Add(point4);

        SplitPath splitPath2 = new SplitPath();
        splitPath2.mapPoints = list2;
        splitPath2.mapPointsSum = list2.Count;

        FloorPath floorPath = new FloorPath();
        List<SplitPath> splitPaths = new List<SplitPath>();
        splitPaths.Add(splitPath);
        splitPaths.Add(splitPath1);
        splitPaths.Add(splitPath2);

        floorPath.splitPaths = splitPaths;
        floorPath.floorLevel = "1";
        floorPath.splitSum = splitPaths.Count;

        List<FloorPath> floorPaths = new List<FloorPath>();
        floorPaths.Add(floorPath);

        MapNavigationPath mapNavigationPath = new MapNavigationPath();
        mapNavigationPath.floorPath = floorPaths;
        mapNavigationPath.identifierBuilding = "C6";

        MapNavigationInfo mapNavigationInfo = new MapNavigationInfo();
        mapNavigationInfo.navPathJsonBuf = mapNavigationPath;
        mapNavigationInfo.turnInfoJsonBuf = mapTurnInfo;

        return JsonUtil.Serialize(mapNavigationInfo);
    }

    /// <summary>
    /// create map point
    /// </summary>
    /// <returns></returns>
    public static MapPoint CreateMapPoint()
    {
        MapPoint point = new MapPoint();
        point.geographicCoords = new double[2] { 120.0, 30.0 };
        point.realSpaceCoords = new float[3] { 3, 5, 8 };
        point.direction2DYaw = 0.0f;
        point.rotation = new float[4] { 0, 0, 0, 1 };
        point.floorLevel = "1";
        return point;
    }

    /// <summary>
    /// create map data
    /// </summary>
    /// <returns></returns>
    public static MapInfo CreateMapInfo()
    {
        MapInfo mapInfo = new MapInfo();
        mapInfo.identitifier = "";
        mapInfo.mapboxUrl = "";
        return mapInfo;
    }
    #endregion
}
