using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

/// <summary>
/// 主内容列表
/// </summary>
[Serializable]
public class LandmarkersResponseData
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
        return "LandmarkersResponseData" + " content = " + contents.Count;
    }
}
