using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

/// <summary>
/// 查询主内容列表
/// </summary>
[Serializable]
public class LandmarkersRequestData
{
    /*
   "latitude": "JEKGyQjFTM",
   "longitude": "ZDIUkaIqzC",
   "coordType": 1,
   "maxDistance": 46372,
   "tagIds": "1,2",
   "client": {
       "sdkVersion": "1.7.1",
       "platform": 3,
       "brand": "iPhone",
       "model": "iPhone10,3",
       "osVersion": "12.1.2",
       "xnwa": "wifi"
   }
    */
    private long latitude;
    private long longitude;
    private int coordType;
    private double maxDistance;
    private string tagIds;
    private AW_ClientInfo client;
   
    public LandmarkersRequestData( long _latitude, long _longitude, int _coordType, double _maxDistance, string _tagIds, AW_ClientInfo clientInfo)
    {
        latitude = _latitude;
        longitude = _longitude;
        coordType = _coordType;
        maxDistance = _maxDistance;
        tagIds = _tagIds;
        client = clientInfo;
    }

    [JsonIgnore]
    public long Latitude
    {
        get
        {
            return latitude;
        }
        set
        {
            latitude = value;
        }
    }

    [JsonIgnore]
    public long Longitude
    {
        get
        {
            return longitude;
        }
        set
        {
            longitude = value;
        }
    }

    [JsonIgnore]
    public int CoordType
    {
        get
        {
            return coordType;
        }
        set
        {
            coordType = value;
        }
    }

    [JsonIgnore]
    public double MaxDistance
    {
        get
        {
            return maxDistance;
        }
        set
        {
            maxDistance = value;
        }
    }

    [JsonIgnore]
    public string TagIds
    {
        get
        {
            return tagIds;
        }
        set
        {
            tagIds = value;
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
