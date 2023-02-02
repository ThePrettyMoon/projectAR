using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

/// <summary>
/// 获取动态so（匿名）（仅安卓）
/// </summary>
[Serializable]
public class SDKResourcesResponseData
{
    public AW_BuildType sdkBuildType;
    public List<AW_SdkResource> resources;

    [JsonIgnore]
    public AW_BuildType SDKBuildType
    {
        get
        {
            return sdkBuildType;
        }
    }

    [JsonIgnore]
    public List<AW_SdkResource> Resources
    {
        get
        {
            return resources;
        }
    }
    public override string ToString()
    {
        return "SDKResourcesResponseData" + " sdkBuildType = " + sdkBuildType
            + " resources = " + resources.Count;
    }
}
