using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using EZXR.NET;

/// <summary>
/// 登陆request
/// </summary>
public class ProxyRequest : BaseInsightRequest
{
    public ProxyRequest(ProxyRequestData reqparam, string appKey, string appSecret, string token) : base(true)
    {
        long time = TimeUtility.GetTimeStampMilli();
        long requestT = time + InsightConfigManager.Instance.GetTimetampDelta();
        string nonce = RandomUtility.Random();

        AddBody("requestBody", reqparam.RequestBody);
        AddBody("requestHeaders", reqparam.RequestHeaders);
        AddBody("requestId", reqparam.RequestId);
        AddBody("requestUrlParams", reqparam.RequestUrlParms);
        AddBody("requestExtensions", reqparam.RequestExtensions);
        

        AddBody("timestamp", requestT);
        AddBody("nonce", nonce);
        //sha256("proxy"|appKey|appSecret|nonce|requestBody|requestHeaders|requestId|requestUrlParams|timestamp|token)
        string signature = "proxy|" + appKey + "|" + appSecret 
            + "|"+ reqparam.RequestBody + "|" + reqparam.RequestHeaders + "|" + reqparam.RequestId + "|" + reqparam.RequestUrlParms 
            + "|" + nonce + "|"  + requestT + "|" + token;
        string hashSha256 = EncodeUtility.Sha256(signature).ToLower();
        AddBody("sign", hashSha256);

    }

    public override string GetApi()
    {
        return "/api/proxy";
    }

    public override string GetMethod()
    {
        return HttpMethod.POST;
    }

    public override Type GetModel()
    {
        return typeof(ProxyResponseData);
    }

}

