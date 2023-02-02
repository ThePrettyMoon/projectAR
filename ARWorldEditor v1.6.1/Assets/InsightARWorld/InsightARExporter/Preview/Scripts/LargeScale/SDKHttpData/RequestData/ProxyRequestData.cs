using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

/// <summary>
/// 请求代理
/// </summary>
[Serializable]
public class ProxyRequestData
{
    private string requestId;
    private string requestUrlParms;
    private string requestHeaders;
    private string requestBody;
    private string requestExtensions;


    public ProxyRequestData( string _requestId, string _requestUrlParms, string _requestHeaders, string _requestBody, string _requestExtensions)
    {
        requestId = _requestId;
        requestUrlParms = _requestUrlParms;
        requestHeaders = _requestHeaders;
        requestBody = _requestBody;
        requestExtensions = _requestExtensions;
    }

    [JsonIgnore]
    public string RequestId
    {
        get
        {
            return requestId;
        }
        set
        {
            requestId = value;
        }
    }

    [JsonIgnore]
    public string RequestUrlParms
    {
        get
        {
            return requestUrlParms;
        }
        set
        {
            requestUrlParms = value;
        }
    }

    [JsonIgnore]
    public string RequestHeaders
    {
        get
        {
            return requestHeaders;
        }
        set
        {
            requestHeaders = value;
        }
    }

    [JsonIgnore]
    public string RequestBody
    {
        get
        {
            return requestBody;
        }
        set
        {
            requestBody = value;
        }
    }


    [JsonIgnore]
    public string RequestExtensions
    {
        get
        {
            return requestExtensions;
        }
        set
        {
            requestExtensions = value;
        }
    }
}
