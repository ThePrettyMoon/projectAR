using System.Collections.Generic;
using ARWorldEditor;

namespace ARWorldEditor
{
    public class CacheMapResources : CacheBaseData
    {
        public ProductState state;
        public string downloadPath;
        public long mapId;
        public string name;
        public long gmtModified; //地图最新的更新时间，用于本地判断地图资源是否需要更新
        public string resourceList; // 地图资源列表

        public void ObtainContentValues(MapResourcesResultData mapResource)
        {
            this.state = mapResource.state;
            this.mapId = mapResource.mapId;
            this.name = mapResource.name;
            this.gmtModified = mapResource.gmtModified;
            this.resourceList = JsonUtil.Serialize(mapResource.resourceList);
            this.downloadPath = mapResource.DownloadPath;
        }

        public MapResourcesResultData ObtainObject()
        {
            MapResourcesResultData mapResource = new MapResourcesResultData();
            mapResource.mapId = this.mapId;
            mapResource.state = this.state;
            mapResource.gmtModified = this.gmtModified;
            mapResource.name = this.name;
            mapResource.resourceList = JsonUtil.Deserialization<List<MapResourcesData>>(this.resourceList);
            mapResource.DownloadPath = this.downloadPath;
            return mapResource;
        }
    }
}

