using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;


[Serializable]
public abstract class BaseRequestData
{
    //客户端版本号
    public string versionno = HttpUtility.VERSION_NO;
    // 请求协议号
    public string protocolno = HttpUtility.PROTOCOL_NO;
    // 网络apn
    public string apn;
    public string osid = HttpUtility.OPERATESYSTEM_ID;
    public string secret;
    public string brand = SystemInfo.deviceName;
    public string model = SystemInfo.deviceModel;
    //windows osversion 置空处理
    /*#if UNITY_EDITOR
        public string osversion = "";
    #else
        public string osversion = SystemInfo.operatingSystem;
    #endif*/
    public string osversion = "";

    [JsonIgnore]
    public string ProtocolNo
    {
        get
        {
            return protocolno;
        }
        set
        {
            protocolno = value;
        }
    }

    [JsonIgnore]
    public string Apn
    {
        get
        {
            return apn;
        }
        set
        {
            apn = value;
        }
    }

    [JsonIgnore]
    public string OsId
    {
        get
        {
            return osid;
        }
        set
        {
            osid = value;
        }
    }

    [JsonIgnore]
    public string Secret
    {
        get
        {
            return secret;
        }
        set
        {
            secret = value;
        }
    }

    [JsonIgnore]
    public string Brand
    {
        get
        {
            return brand;
        }
        set
        {
            brand = value;
        }
    }

    [JsonIgnore]
    public string Model
    {
        get
        {
            return model;
        }
        set
        {
            model = value;
        }
    }

    [JsonIgnore]
    public string OsVersion
    {
        get
        {
            return osversion;
        }
        set
        {
            osversion = value;
        }
    }

    public abstract void ResetTimeStamp(long requestT);
}
