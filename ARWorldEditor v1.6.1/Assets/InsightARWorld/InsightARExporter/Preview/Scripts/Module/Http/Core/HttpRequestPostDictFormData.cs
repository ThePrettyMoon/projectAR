using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZXR.NET;
using UnityEngine.Networking;
using System.Text;

/// <summary>
/// 封装request post form data
/// </summary>
public class HttpRequestPostDictFormData : HttpRequestCreateBase
{
    private Dictionary<string,string> dictFormdata;

    public static HttpRequestPostDictFormData Post(string _uri,Dictionary<string,string> _formData)
    {
        HttpRequestPostDictFormData requestPostFormData = new HttpRequestPostDictFormData();
        requestPostFormData.uri = _uri;
        requestPostFormData.dictFormdata = _formData;
        return requestPostFormData;
    }

    public override  void CreateWebRequest()
    {
        unityWebRequest = GenerateWebRequest();
    }

    private UnityWebRequest GenerateWebRequest()
    {
        return UnityWebRequest.Post(uri, dictFormdata);
    }
}
