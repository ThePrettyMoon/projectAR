using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using EZXR.NET;

/// <summary>
/// 云端重定位
/// </summary>
public class CloudRequest : BaseInsightRequest
{
    private string mDomain = "http://59.111.148.60:8087";
    private string mApi = "/api/alg/cloud/load";

    public CloudRequest(CloudRequestData reqparam, long time, string appKey, string appSecret) : base(true)
    {
        long requestT = time + InsightConfigManager.Instance.GetTimetampDelta();
        string nonce = RandomUtility.Random();

        var tk = InsightConfigManager.Instance.GetToken();

        AddBody("alg", reqparam.alg);

        AddBody("token", tk);
        AddBody("timestamp", requestT);
        AddBody("nonce", nonce);
        //sha256("cloud.reloc"|appKey|appSecret|nonce|timestamp|token)
        string signature = "cloud.reloc|"+appKey + "|" + appSecret + "|" + nonce + "|" + requestT + "|" + tk;
        string hashSha256 = EncodeUtility.Sha256(signature).ToLower();
        AddBody("sign", hashSha256);
    }

    /// <summary>
    ///  需要单独设置
    /// </summary>
    /// <returns></returns>
    public override string GetDomain()
    {
         return mDomain;
    }

    public void SetDomain(string domain)
    {
        mDomain = domain;
    }

    public override string GetApi()
    {
        return mApi;
    }

    public void SetApi(string api)
    {
        mApi = api;
    }

    public override string GetMethod()
    {
        return HttpMethod.POST;
    }

    public override Type GetModel()
    {
        return typeof(CloudResponseData);
    }

    /// <summary>
    /// parse url
    /// </summary>
    /// <param name="url"></param>
    public void ParseUrl(string url)
    {
        Uri uri = new Uri(url);

        //set domain
        string domain = uri.Scheme + "://" + uri.Authority;
        SetDomain(domain);

        //set api
        string apiPath = uri.AbsolutePath;
        SetApi(apiPath);

        string query = uri.Query;
        // 解析参数
        if (!string.IsNullOrEmpty(query))
        {
            //移除 "?"
            string queryString = query.Replace("?", "");
            string[] pairs = queryString.Split('&');
            foreach (string pair in pairs)
            {
                string[] paramvalue = pair.Split('=');
                AddQuery(paramvalue[0], paramvalue[1]);
            }
        }
    }
}

