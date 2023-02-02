using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

/// <summary>
/// 查询子内容快照
/// </summary>
[Serializable]
public class SubContentSnapshotResponseData
{
    public SubArProduct content;

    [JsonIgnore]
    public SubArProduct Content
    {
        get
        {
            return content;
        }
    }

    public override string ToString()
    {
        return "SubContentSnapshotResponseData" + " content = " + content.Cid;
    }
}
