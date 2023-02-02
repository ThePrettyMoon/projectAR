using System;
using System.Collections;
using System.Collections.Generic;
using ARWorldEditor;
using Newtonsoft.Json;
using UnityEngine;

namespace ARWorldEditor
{
    [Serializable]
    public class GetMapResourcesResponseData : BaseResponseData
    {
        public MapResourcesResultData result;
    }

    [Serializable]
    public class MapResourcesResultData : BaseDbData
    {
        public string name;
        public long gmtModified; //地图最新的更新时间，用于本地判断地图资源是否需要更新
        public List<MapResourcesData> resourceList; // 地图资源列表
        //public List<double> origin;
        //public List<double> matrix;

        [JsonIgnore]
        public ProductState state;

        [JsonIgnore]
        public long mapId;  //记录地图id


        public MapResourcesResultData Clone()
        {
            MapResourcesResultData mapResource = new MapResourcesResultData();
            mapResource.name = name;
            mapResource.gmtModified = gmtModified;
            if (resourceList != null && resourceList.Count > 0)
            {
                List<MapResourcesData> list = new List<MapResourcesData>();
                for (int i = 0; i < resourceList.Count; i++)
                {

                    MapResourcesData mapData = new MapResourcesData();
                    mapData.type = resourceList[i].type;
                    mapData.gmtModified = resourceList[i].gmtModified;
                    mapData.resourceUrl = resourceList[i].resourceUrl;
                    list.Add(mapData);
                }
                mapResource.resourceList = list;
            }

            //if (origin != null && origin.Count > 0)
            //{
            //    List<double> originList = new List<double>();
            //    originList.AddRange(origin);
            //    mapResource.origin = originList;
            //}

            //if (matrix != null && matrix.Count > 0)
            //{
            //    List<double> matrixList = new List<double>();
            //    matrixList.AddRange(matrix);
            //    mapResource.matrix = matrixList;
            //}

            mapResource.mapId = mapId;
            return mapResource;
        }
    }

    [Serializable]
    public class MapResourcesData
    {
        public string type; //资源类型，3-三维高精地图点云模型,4-三维内容高模,5-三维内容低模,6-二维显示地图数据源,7-三维路径地图,10-动线pose序列,11-二维显示地图,12-动线图片序列
        public string resourceUrl;   //资源nosurl
        public string gmtModified;   //资源更新时间
    }
}