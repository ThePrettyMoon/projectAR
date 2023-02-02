using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

/// <summary>
/// 子内容response model
/// </summary>
[Serializable]
public class SubContentResponseData
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
        return "SubContentResponseData" + " content = " + content.Cid;
    }
}
