using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

/// <summary>
/// 存储缓存文件
/// </summary>
[Serializable]
public class InsightArCache
{
    //存储arproduct
    public List<CacheNormalEventData> normalEvents;

    /// <summary>
    /// 构造函数
    /// </summary>
    public InsightArCache()
    {
        normalEvents = new List<CacheNormalEventData>();
    }

    /// <summary>
    /// 返回所有normal events
    /// </summary>
    /// <returns></returns>
    public List<CacheNormalEventData> GetNormalEvents()
    {
        return normalEvents;
    }

    /// <summary>
    /// add or update data
    /// </summary>
    /// <param name="dbData"></param>
    public void AddOrUpdate(BaseDbData dbData)
    {
        if (dbData is ArProduct)
        {
            ArProduct arProduct = (ArProduct)dbData;
            CacheNormalEventData normalData = normalEvents.Find(p => p.cid == arProduct.Cid);
            if (normalData == null)
            {
                normalData = new CacheNormalEventData();
                normalEvents.Add(normalData);
            }
            normalData.ObtainContentValues(arProduct);
        }
    }

    /// <summary>
    /// query normal event
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public T Query<T>(string id) where T : BaseDbData
    {
        Type checkType = typeof(T);
        string typeName = checkType.Name;
        if (typeName.Equals("ArProduct"))
        {
            CacheNormalEventData normalEventData = normalEvents.Find(p => p.cid.ToString() == id);
            if (normalEventData == null) return null;
            return normalEventData.ObtainObject() as T;
        }
        return null;
    }

    /// <summary>
    /// delete 
    /// </summary>
    /// <param name="dbData"></param>
    public void Delete(BaseDbData dbData)
    {
        if (dbData is ArProduct)
        {
            ArProduct arProduct = (ArProduct)dbData;
            CacheNormalEventData normalData = normalEvents.Find(p => p.cid == arProduct.Cid);
            if (normalData != null)
                normalEvents.Remove(normalData);
        }
    }
}
