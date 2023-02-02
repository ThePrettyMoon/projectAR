using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

/// <summary>
/// SDK配置
/// </summary>
[Serializable]
public class SDKConfigResponseData
{
    public bool logoVisible;
    public bool cloudLog;
    public long ctTime;
    public string ctUrl;

    [JsonIgnore]
    public bool LogoVisible
    {
        get
        {
            return logoVisible;
        }
    }

    [JsonIgnore]
    public bool CloudLog
    {
        get
        {
            return cloudLog;
        }
    }
    [JsonIgnore]
    public long CtTime
    {
        get
        {
            return ctTime;
        }
    }
    [JsonIgnore]
    public string CtUrl
    {
        get
        {
            return ctUrl;
        }
    }

    public override string ToString()
    {
        return "SDKConfigResponseData" + " logoVisible = " + logoVisible 
            + " cloudLog = " + cloudLog
            + " ctTime = " + ctTime
            + " ctUrl = " + ctUrl;
    }
}
