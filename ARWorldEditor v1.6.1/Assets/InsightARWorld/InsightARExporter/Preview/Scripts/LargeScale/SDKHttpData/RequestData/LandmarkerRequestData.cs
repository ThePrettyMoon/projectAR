using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

/// <summary>
/// 查询主内容
/// </summary>
[Serializable]
public class LandmarkerRequestData
{
    private int cid;
    private AW_ClientInfo client;

    public LandmarkerRequestData(int contentId, AW_ClientInfo clientInfo)
    {
        cid = contentId;
        client = clientInfo;
    }

    [JsonIgnore]
    public int CId
    {
        get
        {
            return cid;
        }
        set
        {
            cid = value;
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
