using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using EZXR.NET;


public class LandmarkersNearbyRequest : BaseInsightRequest
{
    public LandmarkersNearbyRequest(LandmarkersNearbyRequestData reqparam, string appKey, string appSecret, string token) :base( true)
    {

        long time = TimeUtility.GetTimeStampMilli();
        long requestT = time + InsightConfigManager.Instance.GetTimetampDelta();

        AddBody("latitude", reqparam.latitude);
        AddBody("longitude", reqparam.longitude);
        AddBody("coordType", reqparam.coordType);
        AddBody("client", reqparam.client);

        AddBody("token", token);
        AddBody("timestamp", requestT);
        string nonce = RandomUtility.Random();
        AddBody("nonce", nonce);
        long timestamp = time + InsightConfigManager.Instance.GetTimetampDelta();
        //签名：sha256("content.queryLandmarkersNearby"|appKey|appSecret|coordType|latitude|longitude|nonce|timestamp|token)
        string signature = "content.queryLandmarkersNearby|"
            + appKey + "|" + appSecret + "|" + reqparam.coordType
            + "|" + reqparam.latitude + "|" + reqparam.longitude
            + "|" + nonce + "|" + timestamp + "|" + token;
        string hashSha256 = EncodeUtility.Sha256(signature).ToLower();
        AddBody("sign", hashSha256);
    }

    public override string GetApi()
    {
        return "/api/content/queryLandmarkersNearby";
    }

    public override string GetMethod()
    {
        return HttpMethod.POST;
    }

    public override Type GetModel()
    {
        return typeof(LandmarkersNearbyResponseData);
    }
}

