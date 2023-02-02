using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

/// <summary>
/// 请求代理
/// </summary>
[Serializable]
public class ProxyResponseData
{
    public string responseText;
    public int statusCode;

    [JsonIgnore]
    public string ResponseText
    {
        get
        {
            return responseText;
        }
    }

    [JsonIgnore]
    public int StatusCode
    {
        get
        {
            return statusCode;
        }
    }

    public override string ToString()
    {
        return "ProxyResponseData" + " responseText = " + responseText + " statusCode = " + statusCode;
    }
}
