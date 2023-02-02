using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZXR.NET;
using System;

/// <summary>
/// 获取动态so（匿名）（仅安卓）
/// </summary>
public class SDKResourcesRequest : BaseInsightRequest
{
    public SDKResourcesRequest(SDKResourcesRequestData reqparam) : base(true)
    {
        AddBody("abi", reqparam.Abi);
        AddBody("client", reqparam.Client);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override string GetApi()
    {
        return "/api/sys/getSdkResources";
    }

    public override string GetMethod()
    {
        return HttpMethod.POST;
    }

    public override Type GetModel()
    {
        return typeof(SDKResourcesResponseData);
    }
}
