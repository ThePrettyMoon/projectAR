using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

/// <summary>
/// 查询主内容列表（根据标签查询）
/// </summary>
[Serializable]
public class LandmarkersByTagsResponseData
{
    public List<ArProduct> contents;

    [JsonIgnore]
    public List<ArProduct> Contents
    {
        get
        {
            return contents;
        }
    }

    public override string ToString()
    {
        return "LandmarkersByTagsResponseData" + " contents = " + contents.Count;
    }
}
