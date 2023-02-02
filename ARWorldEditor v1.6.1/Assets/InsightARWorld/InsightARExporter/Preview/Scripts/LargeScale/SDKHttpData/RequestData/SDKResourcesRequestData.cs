using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

/// <summary>
/// 获取动态so（匿名）（仅安卓）
/// </summary>
[Serializable]
public class SDKResourcesRequestData
{
    private string abi;
    private AW_ClientInfo client;

    public SDKResourcesRequestData( string _abi, AW_ClientInfo _client)
    {
        abi = _abi;
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

    [JsonIgnore]
    public string Abi
    {
        get
        {
            return abi;
        }
        set
        {
            abi = value;
        }
    }
}
