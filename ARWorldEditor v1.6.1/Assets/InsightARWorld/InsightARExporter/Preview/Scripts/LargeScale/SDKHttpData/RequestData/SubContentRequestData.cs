using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

/// <summary>
/// 子内容查询数据
/// </summary>
[Serializable]
public class SubContentRequestData
{
    private int cid;
    private AW_ClientInfo client;

    public SubContentRequestData(int contentId, AW_ClientInfo clientInfo)
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
