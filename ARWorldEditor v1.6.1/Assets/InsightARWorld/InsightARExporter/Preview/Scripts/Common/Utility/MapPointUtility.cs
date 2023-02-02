using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InsightAR.Internal;

namespace Dongjian.LargeScale
{
    public static class MapPointUtility
    {
        /// <summary>
        /// copy poi data
        /// </summary>
        /// <param name="mapPoiInfo"></param>
        /// <returns></returns>
        public static MapPoIInfo CopyPoiInfo(U3DMapPoIInfo mapPoiInfo)
        {
            MapPoIInfo poiInfo = new MapPoIInfo();
            poiInfo.osmIdentifier = mapPoiInfo.osmIdentifier;
            poiInfo.identifierMap = mapPoiInfo.identifierMap;
            poiInfo.pointInfo = CopyPosInfo(mapPoiInfo.GetPointInfo());
            return poiInfo;
        }

        /// <summary>
        /// copy map point
        /// </summary>
        /// <param name="mapPoint"></param>
        /// <returns></returns>
        public static MapPoint CopyPosInfo(U3DMapPoint mapPoint)
        {
            MapPoint pos = new MapPoint();
            pos.direction2DYaw = mapPoint.Direction;
            pos.geographicCoords = new double[2] { mapPoint.Longitude, mapPoint.Latitude };
            pos.realSpaceCoords = new float[3] {mapPoint.Position.x,mapPoint.Position.y,mapPoint.Position.z};
            pos.rotation = new float[4] {mapPoint.Rotation.x,mapPoint.Rotation.y,mapPoint.Rotation.z,mapPoint.Rotation.w};
            return pos;
        }

        /// <summary>
        /// 返回mappoint
        /// </summary>
        /// <returns></returns>
        public static MapPoint TransformPositionToMapPoint(Transform trans)
        {
            MapPoint mapPoint = new MapPoint();

            if (trans == null) return mapPoint;
            mapPoint.realSpaceCoords = new float[3] { trans.position.x,trans.position.y,trans.position.z };
            mapPoint.rotation = new float[4] {trans.rotation.x,trans.rotation.y,trans.rotation.z,trans.rotation.w };
            mapPoint.floorLevel = "1";
            return mapPoint;
        }
    }
}
