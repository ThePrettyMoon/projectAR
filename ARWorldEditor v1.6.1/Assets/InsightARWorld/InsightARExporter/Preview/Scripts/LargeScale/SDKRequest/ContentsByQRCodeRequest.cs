using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using EZXR.NET;

/// <summary>
/// 查询内容（扫描二维码）
/// </summary>
public class ContentsByQRCodeRequest : BaseInsightRequest
{
    public ContentsByQRCodeRequest(ContentsByQRCodeRequestData reqparam,string appKey, string appSecret, string token) : base(true)
    {
        long time = TimeUtility.GetTimeStampMilli();
        long timestamp = time + InsightConfigManager.Instance.GetTimetampDelta();
        string nonce = RandomUtility.Random();

        AddBody("qrCode", reqparam.QrCode);
        AddBody("client", reqparam.Client);

        AddBody("timestamp", timestamp);
        AddBody("nonce", nonce);
        AddBody("token", token);
        //签名：sha256("content.queryByQRCode"|appKey|appSecret|nonce|qrCode|timestamp|token)
        string signature = "content.queryByQRCode|" + appKey + "|" + appSecret + "|"+ nonce + "|" + reqparam.QrCode + "|" + timestamp + "|" + token;
        string hashSha256 = EncodeUtility.Sha256(signature).ToLower();
        AddBody("sign", hashSha256);

    }

    public override string GetApi()
    {
        return "/api/content/queryByQRCode";
    }

    public override string GetMethod()
    {
        return HttpMethod.POST;
    }

    public override Type GetModel()
    {
        return typeof(ContentsByQRCodeResponseData);
    }

}

