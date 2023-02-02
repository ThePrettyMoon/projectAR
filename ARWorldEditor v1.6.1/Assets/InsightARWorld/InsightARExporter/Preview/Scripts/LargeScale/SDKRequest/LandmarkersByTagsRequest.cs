using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using EZXR.NET;

/// <summary>
/// 主内容查询
/// </summary>
public class LandmarkersByTagsRequest : BaseInsightRequest
{
    public LandmarkersByTagsRequest(LandmarkersByTagsRequestData reqparam,string appKey, string appSecret, string token) : base(true)
    {
        long time = TimeUtility.GetTimeStampMilli();
        long timestamp = time + InsightConfigManager.Instance.GetTimetampDelta();
        string nonce = RandomUtility.Random();

        AddBody("tagIds", reqparam.TagIds);
        AddBody("client", reqparam.Client);

        AddBody("timestamp", timestamp);
        AddBody("nonce", nonce);
        AddBody("token", token);
        //签名：sha256("content.queryLandmarkersByTags"|appKey|appSecret|nonce|tagIds|timestamp|token)
        string signature = "content.queryLandmarkersByTags|" + appKey + "|" + appSecret + "|"+ nonce + "|" + reqparam.TagIds + "|" + timestamp + "|" + token;
        string hashSha256 = EncodeUtility.Sha256(signature).ToLower();
        AddBody("sign", hashSha256);

    }

    public override string GetApi()
    {
        return "/api/content/queryLandmarkersByTags";
    }

    public override string GetMethod()
    {
        return HttpMethod.POST;
    }

    public override Type GetModel()
    {
        return typeof(LandmarkersByTagsResponseData);
    }

}

