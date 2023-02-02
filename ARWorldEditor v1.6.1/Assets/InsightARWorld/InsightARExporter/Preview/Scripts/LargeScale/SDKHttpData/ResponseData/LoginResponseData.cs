using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

/// <summary>
/// 登陆response model
/// </summary>
[Serializable]
public class LoginResponseData
{
    public string token;
    public int groupId;

    [JsonIgnore]
    public string Token
    {
        get
        {
            return token;
        }
    }

    [JsonIgnore]
    public int GroupId
    {
        get
        {
            return groupId;
        }
    }
}
