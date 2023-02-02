using System;
using System.Collections.Generic;
using ARWorldEditor;

namespace ARWorldEditor
{
    [Serializable]
    public class InsightARCache
    {
        public List<CacheMapResources> cacheMapList;

        public InsightARCache()
        {
            cacheMapList = new List<CacheMapResources>();
        }

        public void AddOrUpdate(BaseDbData dbData)
        {
            if (dbData is MapResourcesResultData)
            {
                MapResourcesResultData mapResource = (MapResourcesResultData)dbData;
                CacheMapResources cacheMapResource = cacheMapList.Find(p => p.mapId == mapResource.mapId);
                if (cacheMapResource == null)
                {
                    cacheMapResource = new CacheMapResources();
                    cacheMapList.Add(cacheMapResource);
                }
                cacheMapResource.ObtainContentValues(mapResource);
            }
        }

        public T Query<T>(string id) where T : BaseDbData
        {
            Type checkType = typeof(T);
            string typeName = checkType.Name;
            if (typeName.Equals("MapResourcesResultData"))
            {
                CacheMapResources cacheMapResource = cacheMapList.Find(p => p.mapId.ToString() == id);
                if (cacheMapResource == null) return null;
                return cacheMapResource.ObtainObject() as T;
            }
            return null;
        }

        public void Delete(BaseDbData dbData)
        {
            if (dbData is MapResourcesResultData)
            {
                MapResourcesResultData mapResource = (MapResourcesResultData)dbData;
                CacheMapResources cacheMapResource = cacheMapList.Find(p => p.mapId == mapResource.mapId);
                if (cacheMapResource != null)
                    cacheMapList.Remove(cacheMapResource);
            }
        }
    }
}
