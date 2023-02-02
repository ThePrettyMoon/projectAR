using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using EZXR.NET;

/// <summary>
/// 主内容查询列表
/// </summary>
public class LandmarkersRequest : BaseInsightRequest
{
    public LandmarkersRequest(LandmarkersRequestData reqparam,string appKey, string appSecret, string token) : base(true)
    {
        long time = TimeUtility.GetTimeStampMilli();
        long timestamp = time + InsightConfigManager.Instance.GetTimetampDelta();
        string nonce = RandomUtility.Random();

        AddBody("latitude", reqparam.Latitude);
        AddBody("longitude", reqparam.Longitude);
        AddBody("coordType", reqparam.CoordType);
        AddBody("maxDistance", reqparam.MaxDistance);
        AddBody("tagIds", reqparam.TagIds);
        AddBody("client", reqparam.Client);

        AddBody("timestamp", timestamp);
        AddBody("nonce", nonce);
        AddBody("token", token);
        //签名：sha256("content.queryLandmarkers"|appKey|appSecret|coordType|latitude|longitude|maxDistance|nonce|tagIds|timestamp|token)
        string signature = "content.queryLandmarkers|" + appKey + "|" + appSecret 
            + "|" + reqparam.CoordType + "|" + reqparam.Latitude + "|" + reqparam.Longitude
            + "|" + reqparam.MaxDistance + "|" + nonce + "|" + reqparam.TagIds + "|" + timestamp + "|" + token;
        string hashSha256 = EncodeUtility.Sha256(signature).ToLower();
        AddBody("sign", hashSha256);

    }

    public override string GetApi()
    {
        return "/api/content/queryLandmarkers";
    }

    public override string GetMethod()
    {
        return HttpMethod.POST;
    }

    public override Type GetModel()
    {
        return typeof(LandmarkersResponseData);
    }

}

