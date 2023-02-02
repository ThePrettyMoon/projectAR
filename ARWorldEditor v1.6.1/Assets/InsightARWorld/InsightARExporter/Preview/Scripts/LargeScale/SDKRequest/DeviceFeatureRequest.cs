using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using EZXR.NET;

/// <summary>
/// 登陆request
/// </summary>
public class DeviceFeatureRequest : BaseInsightRequest
{
    public DeviceFeatureRequest(AW_ClientInfo reqparam) : base(true)
    {
        AddBody("client", reqparam);
    }

    public override string GetApi()
    {
        return "/api/sys/getDeviceFeature";
    }

    public override string GetMethod()
    {
        return HttpMethod.POST;
    }

    public override Type GetModel()
    {
        return typeof(DeviceFeatureResponseData);
    }

}

