using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

/// <summary>
/// 查询子内容快照
/// </summary>
[Serializable]
public class SubContentSnapshotRequestData
{
    private int cid;
    private int sid;
    private AW_ClientInfo client;

    public SubContentSnapshotRequestData(int contentId, int sceneId, AW_ClientInfo clientInfo)
    {
        cid = contentId;
        sid = sceneId;
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

    public int SId
    {
        get
        {
            return sid;
        }
        set
        {
            sid = value;
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
