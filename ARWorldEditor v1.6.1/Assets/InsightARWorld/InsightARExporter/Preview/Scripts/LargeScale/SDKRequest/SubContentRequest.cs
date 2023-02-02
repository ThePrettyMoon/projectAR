using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using EZXR.NET;

/// <summary>
/// 子内容查询
/// </summary>
public class SubContentRequest : BaseInsightRequest
{
    public SubContentRequest(SubContentRequestData reqparam,string appKey, string appSecret, string token) : base(true)
    {
        long time = TimeUtility.GetTimeStampMilli();
        long timestamp = time + InsightConfigManager.Instance.GetTimetampDelta();
        string nonce = RandomUtility.Random();

        AddBody("cid", reqparam.CId);
        AddBody("client", reqparam.Client);

        AddBody("timestamp", timestamp);
        AddBody("nonce", nonce);
        AddBody("token", token);
        //sha256("content.querySubContent"|appKey|appSecret|cid|nonce|timestamp|token)
        string signature = "content.querySubContent|" + appKey + "|" + appSecret + "|" + reqparam.CId + "|"+ nonce + "|" + timestamp + "|" + token;
        string hashSha256 = EncodeUtility.Sha256(signature).ToLower();
        AddBody("sign", hashSha256);

    }

    public override string GetApi()
    {
        return "/api/content/querySubContent";
    }

    public override string GetMethod()
    {
        return HttpMethod.POST;
    }

    public override Type GetModel()
    {
        return typeof(SubContentResponseData);
    }

}

