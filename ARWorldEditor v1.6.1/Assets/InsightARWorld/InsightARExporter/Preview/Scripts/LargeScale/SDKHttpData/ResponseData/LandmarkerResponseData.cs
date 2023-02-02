using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

/// <summary>
/// 主内容response model
/// </summary>
[Serializable]
public class LandmarkerResponseData
{
    public ArProduct content;

    [JsonIgnore]
    public ArProduct Content
    {
        get
        {
            return content;
        }
    }

    public override string ToString()
    {
        return "LandmarkerResponseData" + " content = " + content.Cid;
    }
}
