using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

/// <summary>
/// SDK配置
/// </summary>
[Serializable]
public class SDKConfigRequestData
{
    private AW_ClientInfo client;

    public SDKConfigRequestData( AW_ClientInfo _client)
    {
        client = _client;
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
