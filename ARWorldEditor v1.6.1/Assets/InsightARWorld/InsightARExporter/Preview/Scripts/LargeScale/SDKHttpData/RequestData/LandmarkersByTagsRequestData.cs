using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

/// <summary>
/// 查询主内容列表（根据标签查询）
/// </summary>
[Serializable]
public class LandmarkersByTagsRequestData
{
    private string tagIds;
    private AW_ClientInfo client;

    public LandmarkersByTagsRequestData(string contentTags, AW_ClientInfo clientInfo)
    {
        tagIds = contentTags;
        client = clientInfo;
    }

    [JsonIgnore]
    public string TagIds
    {
        get
        {
            return tagIds;
        }
        set
        {
            tagIds = value;
        }
    }

    [JsonIgnore]
    public AW_ClientInfo Client
    {
        get
        {
            return client;
        }
        set
        {
            client = value;
        }
    }
}
