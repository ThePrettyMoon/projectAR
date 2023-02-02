using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

/// <summary>
/// 设备配置
/// </summary>
[Serializable]
public class DeviceFeatureData
{
    private AW_ClientInfo aW_ClientInfo;

    public DeviceFeatureData(AW_ClientInfo awClientInfo)
    {
        aW_ClientInfo = awClientInfo;
    }

    [JsonIgnore]
    public AW_ClientInfo ClientInfo
    {
        get
        {
            return aW_ClientInfo;
        }
        set
        {
            aW_ClientInfo = value;
        }
    }
}
