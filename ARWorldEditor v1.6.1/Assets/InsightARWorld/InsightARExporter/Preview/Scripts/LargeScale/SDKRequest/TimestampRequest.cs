using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZXR.NET;
using System;

/// <summary>
/// 获取服务端时间戳
/// </summary>
public class TimestampRequest :BaseInsightRequest
{
    public TimestampRequest() : base(true)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override string GetApi()
    {
        return "/api/sys/getTimestamp";
    }

    public override string GetMethod()
    {
        return HttpMethod.GET;
    }

    public override Type GetModel()
    {
        return typeof(TimestampResponseData);
    }
}
