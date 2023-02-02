using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InsightAR.Internal;
using Newtonsoft.Json.Linq;
using System;

/// <summary>
/// 存储当前地图所有信息
/// </summary>
public class GameMapData : Singleton<GameMapData>
{
    const string TAG = "GameMapData";

    //导航经过的路径
    private MapNavRoute mMapNavRoute;

    //当前位置
    private MapPoint mMapPoint;

    //当前选择的poi位置
    private MapPoIInfo mMapPoiInfo;

    //POI 列表
    private List<MapPoIInfo> mMapPoiList;

    public GameMapData()
    {
        mMapNavRoute = new MapNavRoute();
    }


    public void SetMapPoint(MapPoint mapPoint)
    {
        mMapPoint = mapPoint;
    }

    public void SetMapPoiPoint(MapPoIInfo mapPoIInfo)
    {
        mMapPoiInfo = mapPoIInfo;
    }

    public void SetMapPoiPoint(string geoId)
    {
        mMapPoiInfo = GetMapPoiInfoByGeoId(geoId);
        //添加楼层信息,不然算法崩溃
        mMapPoiInfo.pointInfo.floorLevel = "1";
    }

    public MapPoIInfo GetMapPoIInfo()
    {
        return mMapPoiInfo;
    }

    /// <summary>
    /// 导航路径
    /// </summary>
    /// <returns></returns>
    public MapNavRoute GetMapNavRoute()
    {
        return mMapNavRoute;
    }

    public MapPoint GetMapPoint()
    {
        return mMapPoint;
    }


    /// <summary>
    /// 返回Poi列表
    /// </summary>
    /// <returns></returns>
    public List<MapPoIInfo> GetMapPoiList()
    {
        return mMapPoiList;
    }

    /// <summary>
    /// set poi list
    /// </summary>
    /// <param name="poiInfos"></param>
    public void SetMapPoiList(MapPoIInfo[] poiInfos)
    {
        mMapPoiList = new List<MapPoIInfo>();
        mMapPoiList.AddRange(poiInfos);
    }

    public void SetMapRoute(MapNavRoute mapNavRoute)
    {
        mMapNavRoute = mapNavRoute;
    }

    /// <summary>
    /// get mappoiinfo by geo id
    /// </summary>
    /// <param name="geoId"></param>
    /// <returns></returns>
    public MapPoIInfo GetMapPoiInfoByGeoId(string geoId)
    {
        if (mMapPoiList == null || mMapPoiList.Count == 0) return new MapPoIInfo();
        for (int i = 0; i < mMapPoiList.Count; i++)
        {
            MapPoIInfo mapPoIInfo = mMapPoiList[i];
            string id = mapPoIInfo.properties.id;
            if (id == geoId) return mapPoIInfo;
        }
        return new MapPoIInfo();
    }
}

    /// <summary>
    /// 导航路径
    /// </summary>
    public class MapNavRoute
    {
        // 导航起始位置
        private U3DMapPoint startNavLocation;
        // 结束位置
        private U3DMapPoIInfo endNavLocaition;
        // 经过位置
        private List<U3DMapPoint> pathPoints;
        
        /// <summary>
        /// 起始位置
        /// </summary>
        /// <returns></returns>
        public U3DMapPoint GetStarNavPoint()
        {
            return startNavLocation;
        }

        /// <summary>
        /// 终点poi位置
        /// </summary>
        /// <returns></returns>
        public U3DMapPoIInfo GetEndNavPoi()
        {
            return endNavLocaition;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<U3DMapPoint> GetNavPathPoints()
        {
            return pathPoints;
        }

        public void SetNavPathPoints(List<U3DMapPoint> mapPoints)
        {
            pathPoints = mapPoints;
        }

        public MapNavRoute()
        {
            if (startNavLocation == null) startNavLocation = new U3DMapPoint();
            if (endNavLocaition == null) endNavLocaition = new U3DMapPoIInfo();
            if (pathPoints == null) pathPoints = new List<U3DMapPoint>();
        }
    }

    /// <summary>
    /// 普通点的信息
    /// </summary>
    public class U3DMapPoint
    {
        //纬度坐标
        private double latitude;
        //经度坐标
        private double longitude;
        //世界坐标
        private Vector3 realSpaceCoords;
        //朝向
        private float direction2D_yaw;
        // 三维空间四元数
        private Quaternion rotation;

        /// <summary>
        /// latitude
        /// </summary>
        public double Latitude
        {
            get
            {
                return latitude;
            }
        }

        /// <summary>
        /// longitude
        /// </summary>
        public double Longitude
        {
            get
            {
                return longitude;
            }
        }

        /// <summary>
        /// position
        /// </summary>
        public Vector3 Position
        {
            get
            {
                return realSpaceCoords;
            }
        }

        /// <summary>
        /// rotation
        /// </summary>
        public Quaternion Rotation
        {
            get
            {
                return rotation;
            }
        }

        /// <summary>
        /// direction
        /// </summary>
        public float Direction
        {
            get
            {
                return direction2D_yaw;
            }
        }

        /// <summary>
        /// 数据拷贝
        /// </summary>
        /// <param name="point"></param>
        public void Clone(U3DMapPoint point)
        {
            latitude = point.latitude;
            longitude = point.longitude;
            realSpaceCoords = point.realSpaceCoords;
            direction2D_yaw = point.direction2D_yaw;
            rotation = point.rotation;
        }

        /// <summary>
        /// 拷贝mappoint数据
        /// </summary>
        /// <param name="point"></param>
        public void Copy(MapPoint point)
        {
            longitude = point.geographicCoords[0];
            latitude = point.geographicCoords[1];
            realSpaceCoords = new Vector3(point.realSpaceCoords[0], point.realSpaceCoords[1], point.realSpaceCoords[2]);
            direction2D_yaw = point.direction2DYaw;
            rotation = new Quaternion(point.rotation[0], point.rotation[1], point.rotation[2], point.rotation[3]);
        }
    }

    /// <summary>
    /// POI配置的语义属性
    /// </summary>
    public class U3DMapPoiProperty
    {
        const string TAG = "U3DMapPoiProperty";
        private string id; //geojson id
        private string type;
        private string name;
        private float R3;
        private bool anchor;
        private float bearing;
        private float height;
        private int content_id;
        private string content_type;
        private float R1;
        private int preview_content_id;
        private string preview_content_type;
        private float R2;

        public string ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        public string Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public float Radius3
        {
            get
            {
                return R3;
            }
            set
            {
                R3 = value;
            }
        }

        public bool Anchor
        {
            get
            {
                return anchor;
            }
            set
            {
                anchor = value;
            }
        }

        public float Bearing
        {
            get
            {
                return bearing;
            }
            set
            {
                bearing = value;
            }
        }

        public float Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
            }
        }

        public int Content_Id
        {
            get
            {
                return content_id;
            }set{
                content_id = value;
            }
        }

        public string Content_Type
        {
            get
            {
                return content_type;
            }
            set
            {
                content_type = value;
            }
        }

        public float Radius1
        {
            get
            {
                return R1;
            }
            set
            {
                R1 = value;
            }
        }

        public int Preview_Content_Id
        {
            get
            {
                return preview_content_id;
            }
            set
            {
                preview_content_id = value;
            }
        }

        public string Preview_Content_Type
        {
            get
            {
                return preview_content_type;
            }
            set
            {
                preview_content_type = value;
            }
        }

        public float Radius2
        {
            get
            {
                return R2;
            }
            set
            {
                R2 = value;
            }
        }
        

        /// <summary>
        /// 生成字符串
        /// </summary>
        public static string JsonToString(U3DMapPoiProperty property)
        {
            JObject jObject = new JObject();
            jObject.Add("id", property.id);
            jObject.Add("type", property.type);
            jObject.Add("name", property.name);
            jObject.Add("anchor", property.anchor?"true":"false");
            jObject.Add("name-radius", property.R3.ToString());
            jObject.Add("bearing", property.bearing.ToString()) ;
            jObject.Add("height", property.height.ToString());
            jObject.Add("content-id", property.content_id.ToString());
            jObject.Add("content-type", property.content_type);
            jObject.Add("content-radius", property.R1.ToString());
            jObject.Add("preview-content-id", property.preview_content_id.ToString());
            jObject.Add("preview-content-type", property.preview_content_type.ToString());
            jObject.Add("preview-content-radius", property.R2.ToString());
            return jObject.ToString();
        }

        /// <summary>
        /// property解析数据
        /// </summary>
        /// <param name="str"></param>
        public void Parse(string str)
        {
            try
            {
                JObject jObject = JObject.Parse(str);
                if (jObject != null)
                {
                    JToken idToken = jObject.SelectToken("id");
                    id = JObjectUtility.ParseJObjectString(idToken);

                    JToken typeToken = jObject.SelectToken("type");
                    type = JObjectUtility.ParseJObjectString(typeToken);

                    JToken nameToken = jObject.SelectToken("name");
                    name = JObjectUtility.ParseJObjectString(nameToken);

                    JToken anchorToken = jObject.SelectToken("anchor");
                    anchor = JObjectUtility.ParseJObjectBool(anchorToken);

                    JToken R3Token = jObject.SelectToken("name-radius");
                    R3 = JObjectUtility.ParseJObjectFloat(R3Token);

                    JToken bearingToken = jObject.SelectToken("bearing");
                    bearing = JObjectUtility.ParseJObjectFloat(bearingToken);

                    JToken heightToken = jObject.SelectToken("height");
                    height = JObjectUtility.ParseJObjectFloat(heightToken);

                    JToken contentIdToken = jObject.SelectToken("content-id");
                    content_id = JObjectUtility.ParseJObjectInt(contentIdToken);

                    JToken contentTypeToken = jObject.SelectToken("content-type");
                    content_type = JObjectUtility.ParseJObjectString(contentTypeToken);

                    JToken R1Token = jObject.SelectToken("content-radius");
                    R1 = JObjectUtility.ParseJObjectFloat(R1Token);

                    JToken previewContentIdToken = jObject.SelectToken("preview-content-id");
                    preview_content_id = JObjectUtility.ParseJObjectInt(previewContentIdToken);

                    JToken previewContentTypeToken = jObject.SelectToken("preview-content-type");
                    preview_content_type = JObjectUtility.ParseJObjectString(previewContentTypeToken);

                    JToken R2Token = jObject.SelectToken("preview-content-radius");
                    R2 = JObjectUtility.ParseJObjectFloat(R2Token);
                }
            }catch(Exception e)
            {
                InsightDebug.LogError(TAG, "Throw New Exception" + e.ToString());
            }    
        }

        /// <summary>
        /// clone
        /// </summary>
        /// <param name="property"></param>
        public void Clone(U3DMapPoiProperty property)
        {
            type = property.type;
            name = property.name;
            anchor = property.anchor;
            R3 = property.R3;
            bearing = property.bearing;
            height = property.height;
            content_id = property.content_id;
            content_type = property.content_type;
            R1 = property.R1;
            preview_content_id = property.preview_content_id;
            preview_content_type = property.preview_content_type;
            R2 = property.R2;
        }
    }

/// <summary>
/// 存储地图的POI信息,包含语义信息
/// </summary>
public class U3DMapPoIInfo
{
    /// <summary>
    /// 地图Id
    /// </summary>
    public string identifierMap;
    /// <summary>
    /// 当前poi的id,OSM id
    /// </summary>
    public string osmIdentifier;
    /// <summary>
    /// 点位置描述
    /// </summary>
    private U3DMapPoint pointInfo;

    /// <summary>
    /// 当前poi的属性描述
    /// </summary>
    private U3DMapPoiProperty properties;

    public string IdentifierMap
    {
        get
        {
            return identifierMap;
        }
    }

    public string OsmIdentifier
    {
        get
        {
            return osmIdentifier;
        }
    }

    public U3DMapPoint GetPointInfo()
    {
        return pointInfo;
    }

    public U3DMapPoiProperty GetProperties()
    {
        return properties;
    }

    /// <summary>
    /// 数据拷贝
    /// </summary>
    /// <param name="mapPoIInfo"></param>
    public void Copy(MapPoIInfo mapPoIInfo)
    {
        identifierMap = mapPoIInfo.identifierMap;
        osmIdentifier = mapPoIInfo.osmIdentifier;
        if (pointInfo == null) pointInfo = new U3DMapPoint();
        pointInfo.Copy(mapPoIInfo.pointInfo);
    }

    /// <summary>
    /// 数据拷贝
    /// </summary>
    /// <param name="mapPoIInfo"></param>
    public void Clone(U3DMapPoIInfo mapPoIInfo)
    {
        identifierMap = mapPoIInfo.identifierMap;
        osmIdentifier = mapPoIInfo.osmIdentifier;
        if (pointInfo == null) pointInfo = new U3DMapPoint();
        pointInfo.Clone(mapPoIInfo.pointInfo);
        properties = mapPoIInfo.properties;
    }
}

/// <summary>
/// 地图信息
/// </summary>
public class U3DMapInfo
{
    /// <summary>
    /// 对应的线上的map的链接地址
    /// </summary>
    public string mapboxUrl;
    /// <summary>
    /// 标记符
    /// </summary>
    public string identitifier;

    public void Copy(MapInfo mapInfo)
    {
        mapboxUrl = mapInfo.mapboxUrl;
        identitifier = mapInfo.identitifier;
    }
}
