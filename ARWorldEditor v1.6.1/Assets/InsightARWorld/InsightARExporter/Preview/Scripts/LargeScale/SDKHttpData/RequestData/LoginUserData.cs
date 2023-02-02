using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

/// <summary>
/// 用户登陆数据
/// </summary>
[Serializable]
public class LoginUserData 
{
    private string appKey;
    private string appSecret;
    private string bundleId;
    private int platform;

    public LoginUserData( string userName,string password,string packageName, int platformId)
    {
        appKey = userName;
        appSecret = password;
        bundleId = packageName;
        platform = platformId;
    }

    [JsonIgnore]
    public string AppKey
    {
        get
        {
            return appKey;
        }
        set
        {
            appKey = value;
        }
    }

    [JsonIgnore]
    public string AppSecret
    {
        get
        {
            return appSecret;
        }
        set
        {
            appSecret = value;
        }
    }

    [JsonIgnore]
    public string BundleId
    {
        get
        {
            return bundleId;
        }
        set
        {
            bundleId = value;
        }
    }
    [JsonIgnore]
    public int Platform
    {
        get
        {
            return platform;
        }
        set
        {
            platform = value;
        }
    }
}
