using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using EZXR.NET;

/// <summary>
/// 登陆request
/// </summary>
public class LoginRequest : BaseInsightRequest
{
    public LoginRequest(LoginUserData reqparam) : base(true)
    {
        long time = TimeUtility.GetTimeStampMilli();
        long requestT = time + InsightConfigManager.Instance.GetTimetampDelta();
        string nonce = RandomUtility.Random();

        AddBody("appKey", reqparam.AppKey);
        AddBody("platform", reqparam.Platform);
        AddBody("bundleId", reqparam.BundleId);

        AddBody("timestamp", requestT);
        AddBody("nonce", nonce);
        //sha256("user.login"|appKey|appSecret|bundleId|nonce|platform|timestamp)
        string signature = "user.login|" + reqparam.AppKey + "|" + reqparam.AppSecret +"|"+ reqparam.BundleId + "|" + nonce + "|" + reqparam.Platform + "|" + requestT;
        string hashSha256 = EncodeUtility.Sha256(signature).ToLower();
        AddBody("sign", hashSha256);

    }

    public override string GetApi()
    {
        return "/api/user/login";
    }

    public override string GetMethod()
    {
        return HttpMethod.POST;
    }

    public override Type GetModel()
    {
        return typeof(LoginResponseData);
    }

}

