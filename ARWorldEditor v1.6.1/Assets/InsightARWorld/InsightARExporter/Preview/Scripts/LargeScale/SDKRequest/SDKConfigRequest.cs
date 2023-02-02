using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZXR.NET;
using System;

/// <summary>
/// 获取SDK配置
/// </summary>
public class SDKConfigRequest : BaseInsightRequest
{
    public SDKConfigRequest( SDKConfigRequestData reqparam, string appKey, string appSecret, string token) : base(true)
    {
        long time = TimeUtility.GetTimeStampMilli();
        long timestamp = time + InsightConfigManager.Instance.GetTimetampDelta();
        string nonce = RandomUtility.Random();

        AddBody("token", token);
        AddBody("client", reqparam.Client);
        AddBody("timestamp", timestamp);
        AddBody("nonce", nonce);
        //sha256("sys.getSdkConfig"|appKey|appSecret|nonce|timestamp|token)
        string signature = "proxy|" + appKey + "|" + appSecret + "|" + nonce + "|" + timestamp + "|" + token;
        string hashSha256 = EncodeUtility.Sha256(signature).ToLower();
        AddBody("sign", hashSha256);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override string GetApi()
    {
        return "/api/sys/getSdkConfig";
    }

    public override string GetMethod()
    {
        return HttpMethod.POST;
    }

    public override Type GetModel()
    {
        return typeof(SDKConfigResponseData);
    }
}
