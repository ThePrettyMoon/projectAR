using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

/// <summary>
/// 根据GPS获取附近建筑请求模型
/// </summary>
[Serializable]
public class LandmarkersNearbyRequestData 
{
    public double latitude;
    public double longitude;
    public int coordType;
    public AW_ClientInfo client;

    [JsonIgnore]
    public double Latitude
    {
        set
        {
            latitude = value;
        }
    }

    [JsonIgnore]
    public double Longitude
    {
        set
        {
            longitude = value;
        }
    }

    [JsonIgnore]
    public int CoordiType
    {
        set
        {
            coordType = value;
        }
    }

    [JsonIgnore]
    public AW_ClientInfo ClientInfo
    {
        set
        {
            client = value;
        }
    }
}
